using EmptyFlow.SciterAPI.Enums;
using EmptyFlow.SciterAPI.Structs;
using System.Runtime.InteropServices;
using System.Text;

namespace EmptyFlow.SciterAPI {

    public partial class SciterAPIHost {

        [DllImport ( SciterLoader.SciterLibrary, EntryPoint = "SciterAPI", CallingConvention = CallingConvention.Cdecl )]
        private static extern IntPtr SciterAPI ();

        private readonly Dictionary<nint, SciterEventHandler> m_eventHandlerMap = [];

        private readonly Dictionary<string, nint> m_eventHandlerUniqueMap = [];

        private IntPtr m_apiPointer = IntPtr.Zero;

        private string m_className = "";

        private Version m_version = new Version ( 0, 0, 0, 0 );

        private IntPtr m_mainWindow = IntPtr.Zero;

        SciterApiStruct m_basicApi;

        GraphicsApiStruct m_graphicsApi;

        bool m_graphicsApiLoaded = false;

        RequestApiStruct m_requestApi;

        private string VersionOfLibrary = "1.0.0.0";

        readonly SciterAPIGlobalCallbacks m_callbacks;

        public SciterAPIHost ( string pathToLibrary, bool enableGraphicsApi = false, bool enableRequestApi = false ) {
            SciterLoader.Initialize ( pathToLibrary );

            m_callbacks = new SciterAPIGlobalCallbacks ( this );
            InnerLoadAPI ();

            if ( enableGraphicsApi ) PrepareGraphicsApi ();
            if ( enableRequestApi ) PrepareRequestApi ();
        }

        public SciterAPIHost () {
            m_callbacks = new SciterAPIGlobalCallbacks ( this );
            InnerLoadAPI ();
        }

        private void InnerLoadAPI () {
            m_apiPointer = SciterAPI ();
            if ( m_apiPointer == IntPtr.Zero ) return;

            m_basicApi = Marshal.PtrToStructure<SciterApiStruct> ( m_apiPointer );

            var major = m_basicApi.SciterVersion ( 0 );
            var minor = m_basicApi.SciterVersion ( 1 );
            var build = m_basicApi.SciterVersion ( 2 );
            var revision = m_basicApi.SciterVersion ( 3 );
            var sciterVersion = new Version ( (int) major, (int) minor, (int) build, (int) revision );

            if ( major < 6 ) throw new Exception ( "Supported only Sciter version 6.0.0.0 or greather!" );

            m_className = Marshal.PtrToStringUni ( m_basicApi.SciterClassName () ) ?? "";
            Console.WriteLine ( $"Sciter class name: {m_className}" );
            Console.WriteLine ( $"Sciter version: {sciterVersion}" );
            Console.WriteLine ( $"SciterAPI version: {VersionOfLibrary}" );

            m_basicApi.SciterExec ( ApplicationCommand.SCITER_APP_INIT, IntPtr.Zero, IntPtr.Zero );
        }

        /// <summary>
        /// Please don't use these method, appropriate methods already called in constructor.
        /// These method stay for backward compatibility.
        /// </summary>
        [Obsolete]
        public void LoadAPI () {
        }

        public IntPtr MainWindow => m_mainWindow;

        public SciterApiStruct OriginalApi => m_basicApi;

        public SciterAPIGlobalCallbacks Callbacks => m_callbacks;

        public string ClassName => m_className;

        public Version Version => m_version;

        public GraphicsApiStruct Graphics => m_graphicsApi;

        public RequestApiStruct Request => m_requestApi;

        public const SciterRuntimeFeatures DefaultRuntimeFeatures = SciterRuntimeFeatures.ALLOW_EVAL | SciterRuntimeFeatures.ALLOW_FILE_IO | SciterRuntimeFeatures.ALLOW_SOCKET_IO | SciterRuntimeFeatures.ALLOW_SYSINFO;

        // delegate for windows was copied from example
        // not sure if it actually need
        private delegate IntPtr WindowDelegate ( IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, IntPtr pParam, IntPtr handled );
        public static IntPtr WindowsDelegateImplementaion ( IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, IntPtr pParam, IntPtr handled ) {
            return 0;
        }

        public void CreateMainWindow ( int width = 0, int height = 0, bool enableDebug = false, bool enableFeature = false ) {
            if ( enableDebug ) m_basicApi.SciterSetOption ( IntPtr.Zero, RtOptions.SCITER_SET_DEBUG_MODE, new IntPtr ( 1 ) );
            if ( enableFeature ) m_basicApi.SciterSetOption ( IntPtr.Zero, RtOptions.SCITER_SET_SCRIPT_RUNTIME_FEATURES, new IntPtr ( (int) DefaultRuntimeFeatures ) );

            var rectangePointer = nint.Zero;
            if ( width > 0 && height > 0 ) {
                var rectangle = new SciterRectangle ( 0, 0, width, height );
                rectangePointer = Marshal.AllocHGlobal ( Marshal.SizeOf<SciterRectangle> () );
                Marshal.StructureToPtr ( rectangle, rectangePointer, false );
            }
            var ptr = Marshal.GetFunctionPointerForDelegate<WindowDelegate> ( WindowsDelegateImplementaion );

            m_mainWindow = m_basicApi.SciterCreateWindow (
                WindowsFlags.Main | WindowsFlags.Resizeable | WindowsFlags.Titlebar | WindowsFlags.Controls,
                rectangePointer,
                RuntimeInformation.IsOSPlatform ( OSPlatform.Windows ) ? ptr : IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero
            );

            if ( rectangePointer != nint.Zero ) Marshal.FreeHGlobal ( rectangePointer );

            if ( enableDebug ) {
                m_basicApi.SciterSetupDebugOutput (
                    m_mainWindow,
                    1,
                    ( IntPtr param, uint subsystem, uint severity, IntPtr text_ptr, uint text_length ) => {
                        Console.WriteLine ( Marshal.PtrToStringUni ( text_ptr, (int) text_length ) );
                        return IntPtr.Zero;
                    }
                );
            }

            m_callbacks.RegisterCallback ();
        }

        public void LoadFile ( string htmlPath ) {
            if ( m_mainWindow == IntPtr.Zero ) return;

            m_basicApi.SciterLoadFile ( m_mainWindow, htmlPath );
        }

        public void LoadHtml ( string html ) {
            if ( m_mainWindow == IntPtr.Zero ) return;
            var bytes = Encoding.UTF8.GetBytes ( html );
            m_basicApi.SciterLoadHtml ( m_mainWindow, bytes, (uint) bytes.Length, "" );
        }

        public void PrepareGraphicsApi () {
            var pointer = m_basicApi.GetSciterGraphicsApi ();
            m_graphicsApi = Marshal.PtrToStructure<GraphicsApiStruct> ( pointer );
            m_graphicsApiLoaded = true;
        }

        public void PrepareRequestApi () {
            var pointer = m_basicApi.GetSciterRequestApi ();
            m_requestApi = Marshal.PtrToStructure<RequestApiStruct> ( pointer );
        }

        public int Process () {
            //activate window
            m_basicApi.SciterWindowExec ( m_mainWindow, WindowCommand.SCITER_WINDOW_ACTIVATE, 1, nint.Zero );

            //expand window
            m_basicApi.SciterWindowExec ( m_mainWindow, WindowCommand.SCITER_WINDOW_SET_STATE, 1, nint.Zero );

            // run loop for waiting close all windows
            var code = m_basicApi.SciterExec ( ApplicationCommand.SCITER_APP_LOOP, nint.Zero, nint.Zero );

            // remove event handlers
            if ( m_eventHandlerMap.Any () ) m_eventHandlerMap.Clear ();
            if ( m_eventHandlerUniqueMap.Any () ) m_eventHandlerUniqueMap.Clear ();

            // deinitialize engine
            m_basicApi.SciterExec ( ApplicationCommand.SCITER_APP_SHUTDOWN, nint.Zero, nint.Zero );

            return code;
        }

        public void CloseMainWindow () {
            if ( m_mainWindow == IntPtr.Zero ) return;

            m_basicApi.SciterWindowExec ( m_mainWindow, WindowCommand.SCITER_WINDOW_SET_STATE, (int) WindowState.SCITER_WINDOW_STATE_CLOSED, IntPtr.Zero );
        }

        public void CloseWindow ( IntPtr window ) {
            if ( window == IntPtr.Zero ) return;

            m_basicApi.SciterWindowExec ( window, WindowCommand.SCITER_WINDOW_SET_STATE, (int) WindowState.SCITER_WINDOW_STATE_CLOSED, IntPtr.Zero );
        }

        /// <summary>
        /// Get main window size and position.
        /// </summary>
        /// <param name="sizeMode">In which window size area will be result.</param>
        /// <param name="windowRelateMode">In which area relate to monitor or other window will be coordinates.</param>
        /// <param name="inPhisicalDevicePixels"> If true coordinates are in physical device pixels, if false in CSS pixels 1/96 of inch.</param>
        public SciterWindowInfo? GetMainWindowSizeAndPosition ( WindowSizeMode sizeMode, WindowRelateMode windowRelateMode, bool inPhisicalDevicePixels = false ) {
            var boxOf = sizeMode == WindowSizeMode.Border ? "border" : "client";
            var relTo = windowRelateMode switch {
                WindowRelateMode.Desktop => "desktop",
                WindowRelateMode.Monitor => "monitor",
                WindowRelateMode.Self => "self",
                _ => ""
            };
            var script = $"Window.this.box(\"xywh\",\"{boxOf}\", \"{relTo}\", {( inPhisicalDevicePixels ? "true" : "false" )})";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsArray && !result.IsArrayLike ) {
                    Console.WriteLine ( "GetMainWindowSizeAndPosition: box() return not array!" );
                    return null;
                }
                var count = GetArrayOrMapCount ( ref result );
                var size = new SciterWindowSize ( 0, 0 );
                var position = new SciterWindowPosition ( 0, 0 );
                for ( var i = 0; i < count; i++ ) {
                    var arrayItem = GetArrayItem ( ref result, i );
                    var value = (int) arrayItem.d;
                    if ( i == 0 ) position = position with { X = value };
                    if ( i == 1 ) position = position with { Y = value };
                    if ( i == 2 ) size = size with { Width = value };
                    if ( i == 3 ) size = size with { Height = value };
                }
                return new SciterWindowInfo ( size, position );
            }

            return null;
        }

    }

    public record SciterWindowInfo ( SciterWindowSize Size, SciterWindowPosition Position );

    public record SciterWindowSize ( int Width, int Height );

    public record SciterWindowPosition ( int X, int Y );

}

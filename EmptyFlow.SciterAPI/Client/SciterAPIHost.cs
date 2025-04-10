using EmptyFlow.SciterAPI.Enums;
using EmptyFlow.SciterAPI.Structs;
using System.Runtime.InteropServices;
using System.Text;

namespace EmptyFlow.SciterAPI {

    public class SciterAPIHost {

        [DllImport ( SciterLoader.SciterLibrary, EntryPoint = "SciterAPI" )]
        private static extern IntPtr SciterAPI ();

        private IntPtr m_apiPointer = IntPtr.Zero;

        private string m_className = "";

        private Version m_version = new Version ( 0, 0, 0, 0 );

        private IntPtr m_mainWindow = IntPtr.Zero;

        private List<SciterEventHandler> m_eventHandlers = new List<SciterEventHandler> ();

        SciterApiStruct m_basicApi;

        private string VersionOfLibrary = "1.0.0.0";

        readonly SciterAPIGlobalCallbacks m_callbacks;

        public SciterAPIHost () {
            m_callbacks = new SciterAPIGlobalCallbacks ( this );
        }

        public void LoadAPI () {
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

        public IntPtr MainWindow => m_mainWindow;

        public SciterApiStruct OriginalApi => m_basicApi;

        public SciterAPIGlobalCallbacks Callbacks => m_callbacks;

        public string ClassName => m_className;

        public Version Version => m_version;

        public const SciterRuntimeFeatures DefaultRuntimeFeatures = SciterRuntimeFeatures.ALLOW_EVAL | SciterRuntimeFeatures.ALLOW_FILE_IO | SciterRuntimeFeatures.ALLOW_SOCKET_IO | SciterRuntimeFeatures.ALLOW_SYSINFO;

        // delegate for windows was copied from example
        // not sure if it actually need
        private delegate IntPtr WindowDelegate ( IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, IntPtr pParam, IntPtr handled );
        public static IntPtr WindowsDelegateImplementaion ( IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, IntPtr pParam, IntPtr handled ) {
            return 0;
        }

        public void CreateMainWindow ( int width, int height, bool enableDebug = false, bool enableFeature = false ) {
            if ( enableDebug ) m_basicApi.SciterSetOption ( IntPtr.Zero, RtOptions.SCITER_SET_DEBUG_MODE, new IntPtr ( 1 ) );
            if ( enableFeature ) m_basicApi.SciterSetOption ( IntPtr.Zero, RtOptions.SCITER_SET_SCRIPT_RUNTIME_FEATURES, new IntPtr ( (int) DefaultRuntimeFeatures ) );

            var rectangle = new SciterRectangle ( 0, 0, width, height );

            var ptr = Marshal.GetFunctionPointerForDelegate<WindowDelegate> ( WindowsDelegateImplementaion );

            m_mainWindow = m_basicApi.SciterCreateWindow (
                WindowsFlags.Main | WindowsFlags.Resizeable | WindowsFlags.Titlebar | WindowsFlags.Controls,
                ref rectangle,
                RuntimeInformation.IsOSPlatform ( OSPlatform.Windows ) ? ptr : IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero
            );

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

        public int Process () {
            //activate window
            m_basicApi.SciterWindowExec ( m_mainWindow, WindowCommand.SCITER_WINDOW_ACTIVATE, 1, IntPtr.Zero );

            //expand window
            m_basicApi.SciterWindowExec ( m_mainWindow, WindowCommand.SCITER_WINDOW_SET_STATE, 1, IntPtr.Zero );

            // run loop for waiting close all windows
            var code = m_basicApi.SciterExec ( ApplicationCommand.SCITER_APP_LOOP, IntPtr.Zero, IntPtr.Zero );

            // remove event handlers
            if ( m_eventHandlers.Any () ) m_eventHandlers.Clear ();

            // deinitialize engine
            m_basicApi.SciterExec ( ApplicationCommand.SCITER_APP_SHUTDOWN, IntPtr.Zero, IntPtr.Zero );

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

        public IEnumerable<IntPtr> MakeCssSelector ( string cssSelector ) {
            IntPtr element;
            var domResult = m_basicApi.SciterGetRootElement ( m_mainWindow, out element );
            if ( domResult != DomResult.SCDOM_OK ) return Enumerable.Empty<IntPtr> ();

            var elements = new List<IntPtr> ();
            SciterElementCallback callback = ( IntPtr he, IntPtr param ) => {
                elements.Add ( he );
                return false;
            };
            m_basicApi.SciterSelectElementsW ( element, cssSelector, callback, 1 );

            return elements;
        }

        public string GetElementHtml ( IntPtr element, bool outer ) {
            var strings = new List<string> ();
            lpcbyteReceiver callback = ( IntPtr bytes, uint num_bytes, IntPtr param ) => {
                strings.Add ( Marshal.PtrToStringAnsi ( bytes, Convert.ToInt32 ( num_bytes ) ) );
            };
            m_basicApi.SciterGetElementHtmlCb ( element, outer, callback, 1 );

            return string.Join ( "", strings );
        }

        public string GetElementText ( IntPtr element ) {
            var strings = new List<string> ();
            lpcwstReceiver callback = ( IntPtr bytes, uint num_bytes, IntPtr param ) => {
                strings.Add ( Marshal.PtrToStringUni ( bytes, Convert.ToInt32 ( num_bytes ) ) );
            };
            m_basicApi.SciterGetElementTextCb ( element, callback, 1 );

            return string.Join ( "", strings );
        }

        public void SetElementText ( IntPtr element, string text ) {
            m_basicApi.SciterSetElementText ( element, text, (uint) text.Length );
        }

        public string GetElementAttribute ( IntPtr element, string name ) {
            var strings = new List<string> ();
            lpcwstReceiver callback = ( IntPtr bytes, uint num_bytes, IntPtr param ) => {
                strings.Add ( Marshal.PtrToStringUni ( bytes, Convert.ToInt32 ( num_bytes ) ) );
            };
            m_basicApi.SciterGetAttributeByNameCb ( element, name, callback, 1 );

            return string.Join ( "", strings );
        }

        public IEnumerable<string> GetElementAttributeNames ( IntPtr element ) {
            m_basicApi.SciterGetAttributeCount ( element, out var count );

            if ( count <= 0 ) return Enumerable.Empty<string> ();

            var result = new List<string> ();

            for ( uint i = 0; i < count; i++ ) {
                var receiver = new LPCStrReceiverCallback ();
                m_basicApi.SciterGetNthAttributeNameCb ( element, i, receiver.Callback, 1 );

                result.Add ( receiver.Result.ToString () );
            }

            return result;
        }

        public bool GetElementHasAttribute ( IntPtr element, string name, CaseInsensitiveMode caseSensitiveMode = CaseInsensitiveMode.CaseInsensitive ) {
            m_basicApi.SciterGetAttributeCount ( element, out var count );

            if ( count <= 0 ) return false;

            for ( uint i = 0; i < count; i++ ) {
                var receiver = new LPCStrReceiverCallback ();
                m_basicApi.SciterGetNthAttributeNameCb ( element, i, receiver.Callback, 1 );

                var attributeName = receiver.Result.ToString ();
                if ( caseSensitiveMode == CaseInsensitiveMode.CaseInsensitive ) attributeName = attributeName.ToLowerInvariant ();

                if ( attributeName == name ) return true;
            }

            return false;
        }

        public IDictionary<string, string> GetElementAttributes ( IntPtr element ) {
            m_basicApi.SciterGetAttributeCount ( element, out var count );

            if ( count <= 0 ) return new Dictionary<string, string> ();

            var result = new Dictionary<string, string> ();

            var nameReceiver = new LPCStrReceiverCallback ();
            var valueReceiver = new LPCWStrReceiverCallback ();

            for ( uint i = 0; i < count; i++ ) {
                nameReceiver.Clear ();
                valueReceiver.Clear ();

                m_basicApi.SciterGetNthAttributeNameCb ( element, i, nameReceiver.Callback, 1 );
                m_basicApi.SciterGetNthAttributeValueCb ( element, i, valueReceiver.Callback, 1 );

                result.Add ( nameReceiver.Result.ToString (), valueReceiver.Result.ToString () );
            }

            return result;
        }

        public void SetElementAttribute ( IntPtr element, string name, string value ) {
            m_basicApi.SciterSetAttributeByName ( element, name, value );
        }

        public void SetElementHtml ( IntPtr element, string text, SetElementHtml insertMode ) {
            var bytes = Encoding.UTF8.GetBytes ( text );
            m_basicApi.SciterSetElementHtml ( element, bytes, (uint) bytes.Length, insertMode );
        }

        /// <summary>
        /// Add <see cref="EventHandler"/> related with window.
        /// All events from all elements will be fired in this event handler.
        /// </summary>
        /// <param name="handler">Event Handler.</param>
        /// <exception cref="ArgumentException">Event handler must be with mode <see cref="SciterEventHandlerMode.Window"/>.</exception>
        public void AddWindowEventHandler ( SciterEventHandler handler ) {
            if ( handler.Mode != SciterEventHandlerMode.Window ) throw new ArgumentException ( "Passed EventHandler must be with mode = SciterEventHandlerMode.Window!" );

            m_basicApi.SciterWindowAttachEventHandler ( m_mainWindow, handler.InnerDelegate, 1, (uint) EventBehaviourGroups.HandleAll );
        }

        /// <summary>
        /// Add event handler and optionally attach to element.
        /// </summary>
        /// <param name="handler">Handler that will be attached to element.</param>
        /// <param name="fromFactory">If parameter is true which mean </param>
        /// <exception cref="ArgumentException"></exception>
        public void AddEventHandler ( SciterEventHandler handler, bool fromFactory = false ) {
            if ( handler.Mode != SciterEventHandlerMode.Element ) throw new ArgumentException ( "Passed EventHandler must be with mode = SciterEventHandlerMode.Element!" );

            if ( m_eventHandlers.Contains ( handler ) ) return;

            m_eventHandlers.Add ( handler );
            if ( !fromFactory ) m_basicApi.SciterAttachEventHandler ( handler.SubscribedElement, handler.InnerDelegate, 1 );
        }

        /// <summary>
        /// Remove from handler but you need to be sure it event handler actually detached from Sciter.
        /// By default it behaviout happened only in SciterEventHandler.HandleInitializationEvent.
        /// </summary>
        /// <param name="handler">The handler to be removed.</param>
        public void RemoveEventHandler ( SciterEventHandler handler ) {
            if ( !m_eventHandlers.Contains ( handler ) ) return;

            m_eventHandlers.Remove ( handler );
        }

    }

}

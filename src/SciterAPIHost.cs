using SciterLibraryAPI.SystemApis;
using System.Runtime.InteropServices;

namespace SciterLibraryAPI {

    public class SciterAPIHost {

        [DllImport ( SciterLoader.SciterLibrary, EntryPoint = "SciterAPI" )]
        private static extern IntPtr SciterAPI ();

        private IntPtr m_apiPointer = IntPtr.Zero;

        private string m_className = "";

        private Version m_version = new Version ( 0, 0, 0, 0 );

        private IntPtr m_mainWindow = IntPtr.Zero;

        SciterApiStruct m_basicApi;

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
            Console.WriteLine ( m_className );
            Console.WriteLine ( sciterVersion.ToString () );
        }

        public string ClassName => m_className;

        public Version Version => m_version;

        public const SciterRuntimeFeatures DefaultRuntimeFeatures = SciterRuntimeFeatures.ALLOW_EVAL | SciterRuntimeFeatures.ALLOW_FILE_IO | SciterRuntimeFeatures.ALLOW_SOCKET_IO | SciterRuntimeFeatures.ALLOW_SYSINFO;

        public void CreateMainWindow ( int width, int height, bool enableDebug = false, bool enableFeature = false ) {
            if ( enableDebug ) m_basicApi.SciterSetOption ( IntPtr.Zero, RtOptions.SCITER_SET_DEBUG_MODE, new IntPtr ( 1 ) );
            if ( enableFeature ) m_basicApi.SciterSetOption ( IntPtr.Zero, RtOptions.SCITER_SET_SCRIPT_RUNTIME_FEATURES, new IntPtr ( (int) DefaultRuntimeFeatures ) );

            var rectangle = new SciterRectangle ( 100, 100, width, height );

            m_mainWindow = m_basicApi.SciterCreateWindow (
                WindowsFlags.Main | WindowsFlags.Resizeable | WindowsFlags.Titlebar | WindowsFlags.Controls,
                ref rectangle,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero
            );

            /*m_basicApi.SciterSetupDebugOutput (
                m_mainWindow,
                1,
                ( IntPtr param, uint subsystem, uint severity, IntPtr text_ptr, uint text_length ) => {
                    Console.WriteLine ( "text_ptr" + Marshal.PtrToStringUni ( text_ptr, (int) text_length ) );
                    return IntPtr.Zero;
                }
            );*/

            m_basicApi.SciterLoadFile ( m_mainWindow, "file://C:/IDEs/sciter/sciter-js-sdk-6.0.0.1/sciter-js-sdk-6.0.0.1/samples/vue/hello-vue.htm" );
        }

        public void Process () {
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Windows ) ) {
                WindowsProcess ();
            }
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Linux ) ) {
                LinuxProcess ();
            }
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.OSX ) ) {
                MacOSProcess ();
            }
        }

        public void WindowsProcess () {
            WindowsApis.ShowWindow ( m_mainWindow, ShowWindowCommands.Normal );

            var windowisOpened = true;
            while ( windowisOpened ) {
                while ( WindowsApis.GetMessage ( out var msg, m_mainWindow, 0, 0 ) != 0 ) {
                    // mean main windows is closed
                    if ( msg.message == 0 ) {
                        windowisOpened = false;
                        break;
                    }

                    WindowsApis.TranslateMessage ( ref msg );
                    WindowsApis.DispatchMessage ( ref msg );
                }
            }
        }

        public void LinuxProcess () {
            LinuxApis.gtk_widget_get_toplevel ( m_mainWindow );
            LinuxApis.gtk_main ();
        }

        public void MacOSProcess () {
            MacOsApis.CreateNsApplicationAndRun ();
        }

        public IEnumerable<IntPtr> MakeCssSelector ( string cssSelector ) {
            IntPtr element;
            var domResult = m_basicApi.SciterGetRootElement ( m_mainWindow, out element );
            if ( domResult != DomResult.SCDOM_OK ) return Enumerable.Empty<IntPtr> ();

            var elements = new List<IntPtr> ();
            SciterElementCallback callback = ( IntPtr he, IntPtr param ) => {
                elements.Add ( he );
                return true;
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

        private List<ElementEventProc> m_windowEventHandlers = new List<ElementEventProc> ();

        public void AddWindowEventHandler ( SciterEventHandler handler ) {
            if ( handler.SubscribedElement != IntPtr.Zero ) throw new ArgumentException ( "Passed element inside property SubscribedElement must be IntPtr.Zero!" );

            ElementEventProc @delegate = ( IntPtr tag, IntPtr he, uint evtg, IntPtr prms ) => {
                return Handle ( (EventBehaviourGroups) evtg, prms, handler );
            };
            m_windowEventHandlers.Add ( @delegate );

            m_basicApi.SciterWindowAttachEventHandler ( m_mainWindow, @delegate, 1, (uint) EventBehaviourGroups.HandleAll );
        }

        public void AddElementEventHandler ( SciterEventHandler handler ) {
            if ( handler.SubscribedElement == IntPtr.Zero ) throw new ArgumentException ( "Passed element inside property SubscribedElement must be non IntPtr.Zero!" );

            ElementEventProc @delegate = ( IntPtr tag, IntPtr he, uint evtg, IntPtr prms ) => {
                return Handle ( (EventBehaviourGroups) evtg, prms, handler );
            };
            m_windowEventHandlers.Add ( @delegate );

            m_basicApi.SciterAttachEventHandler ( handler.SubscribedElement, @delegate, 1 );
        }

        private bool Handle ( EventBehaviourGroups groups, IntPtr parameters, SciterEventHandler handler ) {
            switch ( groups ) {
                case EventBehaviourGroups.SUBSCRIPTIONS_REQUEST:
                    Marshal.WriteInt32 ( parameters, (int) EventBehaviourGroups.HandleAll );
                    return true;
                case EventBehaviourGroups.HANDLE_MOUSE:
                    var mouseArguments = Marshal.PtrToStructure<MouseParameters> ( parameters );
                    handler.MouseEvent (
                        mouseArguments.cmd,
                        mouseArguments.pos,
                        mouseArguments.pos_view,
                        mouseArguments.alt_state,
                        mouseArguments.dragging_mode,
                        mouseArguments.cursor_type,
                        mouseArguments.target,
                        mouseArguments.dragging,
                        mouseArguments.is_on_icon,
                        mouseArguments.button_state
                    );
                    break;
                case EventBehaviourGroups.HANDLE_KEY:
                    var keyboardArguments = Marshal.PtrToStructure<KeyParams> ( parameters );
                    handler.KeyboardEvent ( keyboardArguments.cmd, keyboardArguments.alt_state );
                    break;
                case EventBehaviourGroups.HANDLE_FOCUS:
                    var focusArguments = Marshal.PtrToStructure<FocusParameters> ( parameters );
                    handler.FocusEvent ( focusArguments.cmd, focusArguments.by_mouse_click, focusArguments.cancel, focusArguments.target );
                    break;
                case EventBehaviourGroups.HANDLE_SCROLL:
                    var scrollArguments = Marshal.PtrToStructure<ScrollParameters> ( parameters );
                    handler.ScrollEvent ( scrollArguments.cmd, scrollArguments.target, scrollArguments.pos, scrollArguments.vertical, scrollArguments.source, scrollArguments.reason );
                    break;
                case EventBehaviourGroups.HANDLE_TIMER:
                    var timerArguments = Marshal.PtrToStructure<TimerParameters> ( parameters );
                    handler.TimerEvent ( timerArguments.timerId );
                    break;
                case EventBehaviourGroups.HANDLE_SIZE:
                    handler.SizeEvent ();
                    break;
                case EventBehaviourGroups.HANDLE_GESTURE:
                    var gestureArguments = Marshal.PtrToStructure<GestureParameters> ( parameters );
                    handler.GestureEvent ( gestureArguments.cmd, gestureArguments.target, gestureArguments.pos, gestureArguments.pos_view );
                    break;
                case EventBehaviourGroups.HANDLE_DRAW:
                    var drawArguments = Marshal.PtrToStructure<DrawParameters> ( parameters );
                    handler.DrawEvent ( drawArguments.cmd, drawArguments.gfx, drawArguments.area, drawArguments.reserved );
                    break;
                case EventBehaviourGroups.HANDLE_DATA_ARRIVED:
                    var arrivedArguments = Marshal.PtrToStructure<DataArrivedParameters> ( parameters );
                    handler.DataArrived ( arrivedArguments.initiator, arrivedArguments.data, arrivedArguments.dataSize, arrivedArguments.dataType, arrivedArguments.status, arrivedArguments.uri );
                    break;
                case EventBehaviourGroups.HANDLE_EXCHANGE:
                    var exchangeArguments = Marshal.PtrToStructure<ExchangeParameters> ( parameters );
                    handler.ExchangeParameters (
                        exchangeArguments.cmd,
                        exchangeArguments.target,
                        exchangeArguments.source,
                        exchangeArguments.pos,
                        exchangeArguments.pos_view,
                        exchangeArguments.mode,
                        exchangeArguments.data
                    );
                    break;
                case EventBehaviourGroups.HANDLE_METHOD_CALL:
                    var methodCallArguments = Marshal.PtrToStructure<MethodParameters> ( parameters );
                    handler.MethodCall ( methodCallArguments.methodID );
                    break;
                case EventBehaviourGroups.HANDLE_BEHAVIOR_EVENT:
                    var behaviourArguments = Marshal.PtrToStructure<BehaviourEventsParameters> ( parameters );
                    handler.BehaviourEvent (
                        behaviourArguments.cmd,
                        behaviourArguments.heTarget,
                        behaviourArguments.he,
                        behaviourArguments.reason,
                        behaviourArguments.data,
                        behaviourArguments.name
                    );
                    break;
                case EventBehaviourGroups.HANDLE_SCRIPTING_METHOD_CALL:
                    var scriptArguments = Marshal.PtrToStructure<ScriptingMethodParameters> ( parameters );
                    var resultOffset = Marshal.OffsetOf ( typeof ( ScriptingMethodParameters ), nameof ( ScriptingMethodParameters.result ) );

                    var scriptValues = new List<SciterValue> ( Convert.ToInt32 ( scriptArguments.argc ) );
                    for ( var i = 0; i < scriptArguments.argc; i++ ) {
                        var ptr = IntPtr.Add ( scriptArguments.argv, i * Marshal.SizeOf<SciterValue> () );

                        scriptValues.Add ( Marshal.PtrToStructure<SciterValue> ( ptr ) );
                    }

                    var resultValue = handler.ScriptMethodCall ( Marshal.PtrToStringAnsi ( scriptArguments.name ), scriptValues );
                    var resultValuePtr = IntPtr.Add ( parameters, resultOffset.ToInt32 () );
                    Marshal.StructureToPtr ( resultValue, resultValuePtr, false );
                    break;
                case EventBehaviourGroups.HANDLE_SOM:
                    var somArguments = Marshal.PtrToStructure<SOMParameters> ( parameters );
                    handler.SOMEvent ( somArguments.cmd, somArguments.data );
                    break;
                case EventBehaviourGroups.HANDLE_INITIALIZATION:
                    var initializationArguments = Marshal.PtrToStructure<InitializationParameters> ( parameters );
                    handler.HandleInitializationEvent ( initializationArguments.cmd );
                    return true;
            }

            return false;
        }

    }

}

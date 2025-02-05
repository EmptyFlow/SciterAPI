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
            var m_apiPointer = SciterAPI ();
            if ( m_apiPointer == IntPtr.Zero ) return;

            m_basicApi = Marshal.PtrToStructure<SciterApiStruct> ( m_apiPointer );

            var major = m_basicApi.SciterVersion ( 0 );
            var minor = m_basicApi.SciterVersion ( 1 );
            var build = m_basicApi.SciterVersion ( 2 );
            var revision = m_basicApi.SciterVersion ( 3 );
            var sciterVersion = new Version ( (int) major, (int) minor, (int) build, (int) revision );

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

        public void SelectElement ( string cssSelector ) {
            //first step, get root element
            //var getRoot = m_basicApi.SciterGetRootElement ( m_mainWindow );

            //second step, select elements by css selector
            //m_basicApi.SciterSelectElementsW(getRoot, cssSelector)
        }

        private List<ElementEventProc> m_windowEventHandlers = new List<ElementEventProc> ();

        public void AddWindowEventHandler ( SciterWindowEventHandler handler ) {
            ElementEventProc @delegate = ( IntPtr tag, IntPtr he, uint evtg, IntPtr prms ) => {
                return Handle ( (EventBehaviourGroups) evtg, prms, handler );
            };
            m_windowEventHandlers.Add ( @delegate );

            m_basicApi.SciterWindowAttachEventHandler ( m_mainWindow, @delegate, 1, (uint) EventBehaviourGroups.HandleAll );
        }

        private bool Handle ( EventBehaviourGroups groups, IntPtr @params, SciterWindowEventHandler handler ) {
            switch ( groups ) {
                case EventBehaviourGroups.SUBSCRIPTIONS_REQUEST:
                    Marshal.WriteInt32 ( @params, (int) EventBehaviourGroups.HandleAll );
                    return true;
                case EventBehaviourGroups.HANDLE_INITIALIZATION:
                    /*
					 var initializationParams =
						Marshal.PtrToStructure<SciterBehaviors.INITIALIZATION_PARAMS>(ptr: prms);

					switch (initializationParams.cmd)
					{
						case SciterBehaviors.INITIALIZATION_EVENTS.BEHAVIOR_ATTACH:
#if DEBUG
							Debug.WriteLine($"Attach {this.Name}");
							Debug.Assert(_isAttached == false);
							_isAttached = true;
#endif
							this.Element = sourceElement;
							AttachedHandlers.Add(this);
							Attached(sourceElement);
							break;
						case SciterBehaviors.INITIALIZATION_EVENTS.BEHAVIOR_DETACH:
#if DEBUG
							Debug.Assert(_isAttached == true);
							_isAttached = false;
#endif
							Detached(sourceElement);
							AttachedHandlers.Remove(this);
							this.Element = null;
							break;
						default:
							return false;
					}
					 */
                    return true;
                case EventBehaviourGroups.HANDLE_MOUSE:
                    var mouseArguments = Marshal.PtrToStructure<MouseParameters> ( @params );
                    handler.MouseEvent ( mouseArguments.cmd, mouseArguments.pos, mouseArguments.pos_view, mouseArguments.alt_state, mouseArguments.dragging_mode, mouseArguments.cursor_type);
                    break;
                case EventBehaviourGroups.HANDLE_KEY:
					var keyboardArguments = Marshal.PtrToStructure<KeyParams>( @params );
                    handler.KeyboardEvent ( keyboardArguments.cmd, keyboardArguments.alt_state );
                    break;
                case EventBehaviourGroups.HANDLE_FOCUS:
                    /*
					 var args = Marshal.PtrToStructure<SciterBehaviors.FOCUS_PARAMS>(prms).ToEventArgs();
					return OnFocus(element: sourceElement, args: args);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_SCROLL:
                    /*
					var eventArgs = Marshal.PtrToStructure<SciterBehaviors.SCROLL_PARAMS>(prms).ToEventArgs();
					return OnScroll(element: sourceElement, args: eventArgs);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_TIMER:
                    /*
					 var timerParams = Marshal.PtrToStructure<SciterBehaviors.TIMER_PARAMS>(prms);
					return OnTimer(sourceElement,
						timerParams.timerId.Equals(IntPtr.Zero) ? (IntPtr?) null : timerParams.timerId);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_SIZE:
                    /*
					 return OnSize(sourceElement);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_DRAW:
                    /*
					 var args = Marshal.PtrToStructure<SciterBehaviors.DRAW_PARAMS>(prms).ToEventArgs();
					return OnDraw(element: sourceElement, args: args);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_DATA_ARRIVED:
                    /*
					 var arrivedParams = Marshal.PtrToStructure<SciterBehaviors.DATA_ARRIVED_PARAMS>(prms);
					return OnDataArrived(sourceElement, arrivedParams);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_BEHAVIOR_EVENT:
                    /*
					 var eventParams = Marshal.PtrToStructure<SciterBehaviors.BEHAVIOR_EVENT_PARAMS>(prms);
					var targetElement = eventParams.he != IntPtr.Zero ? SciterElement.Attach(eventParams.he) : null;

					Element = eventParams.cmd switch
					{
						SciterBehaviors.BEHAVIOR_EVENTS.DOCUMENT_CREATED => targetElement,
						SciterBehaviors.BEHAVIOR_EVENTS.DOCUMENT_CLOSE => null,
						_ => Element
					};

					return OnEvent(sourceElement: sourceElement, targetElement: targetElement,
						eventType: (BehaviorEvents) (int) eventParams.cmd, reason: eventParams.reason,
						data: SciterValue.Attach(eventParams.data), eventName: eventParams.name);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_METHOD_CALL:
                    /*
					 var methodParams = Marshal.PtrToStructure<SciterXDom.METHOD_PARAMS>(prms);
					return OnMethodCall(sourceElement, methodParams.methodID);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_SCRIPTING_METHOD_CALL:
                    /*
					 var resultOffset = Marshal.OffsetOf(typeof(SciterBehaviors.SCRIPTING_METHOD_PARAMS),
						nameof(SciterBehaviors.SCRIPTING_METHOD_PARAMS.result));
#if OSX
					if(IntPtr.Size == 4)
						Debug.Assert(resultOffset.ToInt32() == 12);
#else
					if (IntPtr.Size == 4)
						Debug.Assert(resultOffset.ToInt32() == 16); // yep 16, strange but is what VS C++ compiler says
#endif
					else if (IntPtr.Size == 8)
						Debug.Assert(resultOffset.ToInt32() == 24);

					var methodParams = Marshal.PtrToStructure<SciterBehaviors.SCRIPTING_METHOD_PARAMS>(prms);
					var methodParamsWrapper = new SciterBehaviors.SCRIPTING_METHOD_PARAMS_WRAPPER(methodParams);

					var scriptResult = OnScriptCall(sourceElement, methodParamsWrapper.name, methodParamsWrapper.args);

					if (!scriptResult.IsSuccessful)
					{
						var methodInfos = GetType().GetMethods()
							.Where(w => w.GetCustomAttributes<SciterFunctionNameAttribute>()
								.Any(a => a.FunctionName.Equals(methodParamsWrapper.name)) || w.Name.Equals(methodParamsWrapper.name))
							.ToArray();

						if (methodInfos?.Any() != true)
							return false;

						MethodInfo methodInfo;

						if (methodInfos.Length == 1)
						{
							methodInfo = methodInfos.First();
						}
						else
						{
							methodInfo = methodInfos.Where(w =>
								(w.GetParameters().Count(c => c.ParameterType == typeof(SciterValue)) ==
								 methodParamsWrapper.args.Length)
								||
								w.GetParameters().Any(a =>
									a.ParameterType == typeof(SciterValue[]))
							).OrderByDescending(ob =>
								ob.GetParameters().Count(c => c.ParameterType == typeof(SciterValue)) ==
								methodParamsWrapper.args.Length).FirstOrDefault();
						}

						if (methodInfo == null)
							return false;
						
						scriptResult = OnScriptCall(sourceElement, methodInfo, methodParamsWrapper.args);

						if (scriptResult.IsSuccessful)
						{
							//pw.result = scriptResult.Value;
							var resultValue = (scriptResult.Value ?? SciterValue.Null).ToVALUE();
							var resultValuePtr = IntPtr.Add(prms, resultOffset.ToInt32());
							Marshal.StructureToPtr(resultValue, resultValuePtr, false);
						}
					}

					return scriptResult.IsSuccessful;
					 */
                    break;
                case EventBehaviourGroups.HANDLE_EXCHANGE:
                    /*
					 var eventArgs = Marshal.PtrToStructure<SciterBehaviors.EXCHANGE_PARAMS>(prms).ToEventArgs();
					return OnExchange(element: sourceElement, args: eventArgs);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_GESTURE:
                    /*
					var eventArgs = Marshal.PtrToStructure<SciterBehaviors.GESTURE_PARAMS>(prms).ToEventArgs();
					return OnGesture(element: sourceElement, args: eventArgs);
					 */
                    break;
                case EventBehaviourGroups.HANDLE_SOM:
                    /*
					 //SOM_PARAMS *p = (SOM_PARAMS *)prms;
					SciterBehaviors.SOM_PARAMS p = Marshal.PtrToStructure<SciterBehaviors.SOM_PARAMS>(ptr: prms);

					if (p.cmd == SciterBehaviors.SOM_EVENTS.SOM_GET_PASSPORT)
					{
						//	p->data.passport = pThis->asset_get_passport();
					}
					else if (p.cmd == SciterBehaviors.SOM_EVENTS.SOM_GET_ASSET)
					{
						//	p->data.asset = static_cast<som_asset_t*>(pThis); // note: no add_ref
					}

					return false;
					 */
                    break;
                case EventBehaviourGroups.HandleAll:
                    break;
            }

            return false;
        }

    }

}

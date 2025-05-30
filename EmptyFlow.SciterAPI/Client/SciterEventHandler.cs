using EmptyFlow.SciterAPI.Structs;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    public class SciterEventHandler {

        private IntPtr m_processedElement = IntPtr.Zero;

        private nint m_relatedPassport = nint.Zero;

        private nint m_relatedAsset = nint.Zero;

        private readonly IntPtr m_subscribedElement = IntPtr.Zero;

        private readonly SciterEventHandlerMode m_mode = SciterEventHandlerMode.Element;

        private readonly SciterAPIHost m_host;

        private readonly ElementEventProc m_innerDelegate;

        private readonly List<EventBehaviourGroups> m_includedEvents = [];

        /// <summary>
        /// Element which is processed now in handler.
        /// </summary>
        protected IntPtr ProcessedElement => m_processedElement;

        /// <summary>
        /// Related Sciter host.
        /// </summary>
        public SciterAPIHost Host => m_host;

        /// <summary>
        /// Element on which event subscribed this handler.
        /// </summary>
        public IntPtr SubscribedElement => m_subscribedElement;

        /// <summary>
        /// Mode of handler, can be two types Window or Element.
        /// </summary>
        public SciterEventHandlerMode Mode => m_mode;

        /// <summary>
        /// Inner delegate which is used in handling events.
        /// </summary>
        public ElementEventProc InnerDelegate => m_innerDelegate;

        public nint AttachedPassport => m_relatedPassport;

        public nint AttachedAsset => m_relatedAsset;

        public SciterEventHandler ( IntPtr relatedThing, SciterAPIHost host, SciterEventHandlerMode mode ) {
            m_subscribedElement = relatedThing;
            m_host = host ?? throw new ArgumentNullException ( nameof ( host ) );
            m_innerDelegate = SciterHandleEvent;
            m_mode = mode;
        }

        public void SetActiveBehaviourGroups ( IEnumerable<EventBehaviourGroups> groups ) {
            m_includedEvents.Clear ();
            m_includedEvents.AddRange ( groups );
        }

        private bool SciterHandleEvent ( IntPtr tag, IntPtr he, uint evtg, IntPtr prms ) => EventHandler ( tag, he, evtg, prms );

        private int m_referenceCounter = 0;

        private AssetAddOrReleasesDelegate? AssetAddRefDelegate;
        private AssetAddOrReleasesDelegate? AssetReleaseDelegate;
        private AssetGetInterfaceDelegate? AssetGetInterfaceDelegate;
        private AssetGetPassportDelegate? AssetGetPassportDelegate;

        public int AssetAddRef ( nint thing ) {
            m_referenceCounter++;
            return m_referenceCounter;
        }

        public int AssetRelease ( nint thing ) {
            m_referenceCounter--;
            return m_referenceCounter;
        }

        public int AssetGetInterface ( nint thing, string name, nint @out ) {
            //if ( name != "asset.sciter.com" ) return 0;
            //if ( 0 != strcmp ( name, interface_name () ) ) return false;
            //if (out) { this->asset_add_ref (); *out = this; }
            return 1;
        }

        [UnmanagedCallConv]
        public nint AssetGetPassport ( IntPtr thing ) => m_relatedPassport;

        private nint GetAsset () {
            if ( m_relatedAsset != nint.Zero ) return m_relatedAsset;

            AssetAddRefDelegate = new AssetAddOrReleasesDelegate ( AssetAddRef );
            AssetReleaseDelegate = new AssetAddOrReleasesDelegate ( AssetRelease );
            AssetGetInterfaceDelegate = new AssetGetInterfaceDelegate ( AssetGetInterface );
            AssetGetPassportDelegate = new AssetGetPassportDelegate ( AssetGetPassport );

            var assetClass = new SomAssetClass {
                AssetAddRef = Marshal.GetFunctionPointerForDelegate ( AssetAddRefDelegate ),
                AssetRelease = Marshal.GetFunctionPointerForDelegate ( AssetReleaseDelegate ),
                AssetGetInterface = Marshal.GetFunctionPointerForDelegate ( AssetGetInterfaceDelegate ),
                AssetGetPassport = Marshal.GetFunctionPointerForDelegate ( AssetGetPassportDelegate )
            };

            var classPointer = Marshal.AllocHGlobal ( Marshal.SizeOf<SomAssetClass> () );

            var asset = new SomAsset {
                isa = classPointer
            };

            m_relatedAsset = Marshal.AllocHGlobal ( Marshal.SizeOf<SomAsset> () );

            Marshal.StructureToPtr ( asset, m_relatedAsset, false );

            return m_relatedAsset;
        }

        public virtual bool EventHandler ( IntPtr tag, IntPtr he, uint evtg, IntPtr prms ) {
            if ( m_includedEvents.Any () && !m_includedEvents.Contains ( (EventBehaviourGroups) evtg ) ) return false;

            m_processedElement = he;

            var parameters = prms;

            switch ( (EventBehaviourGroups) evtg ) {
                case EventBehaviourGroups.SUBSCRIPTIONS_REQUEST:
                    Marshal.WriteInt32 ( parameters, (int) EventBehaviourGroups.HandleAll );
                    AfterRegisterEvent ();
                    return true;
                case EventBehaviourGroups.HANDLE_MOUSE:
                    var mouseArguments = Marshal.PtrToStructure<MouseParameters> ( parameters );
                    MouseEvent (
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
                    KeyboardEvent ( keyboardArguments.cmd, keyboardArguments.alt_state );
                    break;
                case EventBehaviourGroups.HANDLE_FOCUS:
                    var focusArguments = Marshal.PtrToStructure<FocusParameters> ( parameters );
                    FocusEvent ( focusArguments.cmd, focusArguments.by_mouse_click, focusArguments.cancel, focusArguments.target );
                    break;
                case EventBehaviourGroups.HANDLE_SCROLL:
                    var scrollArguments = Marshal.PtrToStructure<ScrollParameters> ( parameters );
                    ScrollEvent ( scrollArguments.cmd, scrollArguments.target, scrollArguments.pos, scrollArguments.vertical, scrollArguments.source, scrollArguments.reason );
                    break;
                case EventBehaviourGroups.HANDLE_TIMER:
                    var timerArguments = Marshal.PtrToStructure<TimerParameters> ( parameters );
                    TimerEvent ( timerArguments.timerId );
                    break;
                case EventBehaviourGroups.HANDLE_SIZE:
                    SizeEvent ();
                    break;
                case EventBehaviourGroups.HANDLE_GESTURE:
                    var gestureArguments = Marshal.PtrToStructure<GestureParameters> ( parameters );
                    GestureEvent ( gestureArguments.cmd, gestureArguments.target, gestureArguments.pos, gestureArguments.pos_view );
                    break;
                case EventBehaviourGroups.HANDLE_DRAW:
                    var drawArguments = Marshal.PtrToStructure<DrawParameters> ( parameters );
                    DrawEvent ( drawArguments.cmd, drawArguments.gfx, drawArguments.area, drawArguments.reserved );
                    break;
                case EventBehaviourGroups.HANDLE_DATA_ARRIVED:
                    var arrivedArguments = Marshal.PtrToStructure<DataArrivedParameters> ( parameters );
                    DataArrived ( arrivedArguments.initiator, arrivedArguments.data, arrivedArguments.dataSize, arrivedArguments.dataType, arrivedArguments.status, arrivedArguments.uri );
                    break;
                case EventBehaviourGroups.HANDLE_EXCHANGE:
                    var exchangeArguments = Marshal.PtrToStructure<ExchangeParameters> ( parameters );
                    ExchangeParameters (
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
                    MethodCall ( methodCallArguments.methodID );
                    break;
                case EventBehaviourGroups.HANDLE_BEHAVIOR_EVENT:
                    var behaviourArguments = Marshal.PtrToStructure<BehaviourEventsParameters> ( parameters );
                    BehaviourEvent (
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
                    var methodName = Marshal.PtrToStringAnsi ( scriptArguments.name ) ?? "";

                    var scriptValues = new List<SciterValue> ( Convert.ToInt32 ( scriptArguments.argc ) );
                    for ( var i = 0; i < scriptArguments.argc; i++ ) {
                        var ptr = IntPtr.Add ( scriptArguments.argv, i * Marshal.SizeOf<SciterValue> () );

                        scriptValues.Add ( Marshal.PtrToStructure<SciterValue> ( ptr ) );
                    }

                    var (resultValue, handled) = ScriptMethodCall ( methodName, scriptValues );
                    if ( handled && resultValue.HasValue ) scriptArguments.result = resultValue.Value;
                    Marshal.StructureToPtr ( scriptArguments, parameters, true );
                    return handled;
                case EventBehaviourGroups.HANDLE_SOM:
                    var somCommand = Marshal.PtrToStructure<SOMParameters> ( parameters );
                    if ( somCommand.Command == SOMEvents.SOM_GET_PASSPORT ) {
                        var passport = m_relatedPassport != nint.Zero ? m_relatedPassport : SOMEventPassport ();
                        if ( passport != nint.Zero ) {
                            m_relatedPassport = passport;
                            somCommand.Data = passport;
                            Marshal.StructureToPtr ( somCommand, parameters, true );
                            return true;
                        }
                    }
                    if ( somCommand.Command == SOMEvents.SOM_GET_ASSET ) {
                        somCommand.Data = GetAsset ();
                        Marshal.StructureToPtr ( somCommand, parameters, true );
                        return true;
                    }
                    break;
                case EventBehaviourGroups.HANDLE_INITIALIZATION:
                    var initializationArguments = Marshal.PtrToStructure<InitializationParameters> ( parameters );
                    HandleInitializationEvent ( initializationArguments.cmd );

                    if ( initializationArguments.cmd == InitializationEvents.BEHAVIOR_DETACH ) {
                        //we need to handle behaviour detaching and remove event handler from attached list in host
                        m_host.RemoveEventHandler ( this );
                    }
                    return true;
            }

            m_processedElement = nint.Zero;

            return false;
        }

        public virtual void AfterRegisterEvent () {
        }

        public virtual void MouseEvent ( MouseEvents command, SciterPoint elementRelated, SciterPoint ViewRelated, KeyboardStates keyboardStates, DraggingType draggingMode, CursorType cursorType, IntPtr target, IntPtr dragging, bool isOnIcon, uint buttonState ) {
        }

        public virtual void KeyboardEvent ( KeyEvents command, KeyboardStates keyboardStates ) {
        }

        public virtual void FocusEvent ( FocusEvents command, bool byMouseClick, bool cancel, nint element ) {
        }

        public virtual void ScrollEvent ( ScrollEvents cmd, nint target, int position, bool vertical, ScrollSource source, uint reason ) {
        }

        public virtual void TimerEvent ( nint timerId ) {
        }

        public virtual void SizeEvent () {
        }

        public virtual void GestureEvent ( uint command, nint target, SciterPoint positionElement, SciterPoint positionView ) {
        }

        public virtual void DrawEvent ( DrawEvents command, nint gfx, SciterRectangle area, uint reserved ) {
        }

        public virtual void DataArrived ( nint initiator, byte[] data, uint dataSize, uint dataType, uint status, string uri ) {
        }

        public virtual void ExchangeParameters ( uint command, nint target, nint source, SciterPoint position, SciterPoint pos_view, uint mode, SciterValue data ) {
        }

        public virtual void MethodCall ( BehaviourMethodIdentifiers methodID ) {
        }

        public virtual nint SOMEventPassport () {
            return nint.Zero;
        }

        public virtual void BehaviourEvent ( BehaviourEvents command, nint targetElement, nint element, nint reason, SciterValue data, string name ) {
        }

        /// <summary>
        /// Handling element.xcall(name, param1, param2, param3)
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="arguments">Arguments.</param>
        /// <returns>Two cases - if method founded (value, true) or if method not found (null, false).</returns>
        public virtual (SciterValue? value, bool handled) ScriptMethodCall ( string name, IEnumerable<SciterValue> arguments ) => (null, false);

        public virtual void HandleInitializationEvent ( InitializationEvents command ) {
        }

    }

}

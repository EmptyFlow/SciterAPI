
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    public class SciterEventHandler {

        private IntPtr m_subscribedElement = IntPtr.Zero;

        private SciterAPIHost m_host;

        protected ElementEventProc m_innerDelegate;

        public SciterAPIHost Host => m_host;

        public IntPtr SubscribedElement => m_subscribedElement;

        public ElementEventProc InnerDelegate => m_innerDelegate;

        public SciterEventHandler ( IntPtr element, SciterAPIHost host ) {
            m_subscribedElement = element;
            m_host = host ?? throw new ArgumentNullException ( nameof ( host ) );
            m_innerDelegate = SciterHandleEvent;
        }

        private bool SciterHandleEvent ( IntPtr tag, IntPtr he, uint evtg, IntPtr prms ) {
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
                    var resultOffset = Marshal.OffsetOf ( typeof ( ScriptingMethodParameters ), nameof ( ScriptingMethodParameters.result ) );

                    var scriptValues = new List<SciterValue> ( Convert.ToInt32 ( scriptArguments.argc ) );
                    for ( var i = 0; i < scriptArguments.argc; i++ ) {
                        var ptr = IntPtr.Add ( scriptArguments.argv, i * Marshal.SizeOf<SciterValue> () );

                        scriptValues.Add ( Marshal.PtrToStructure<SciterValue> ( ptr ) );
                    }

                    var resultValue = ScriptMethodCall ( Marshal.PtrToStringAnsi ( scriptArguments.name ), scriptValues );
                    var resultValuePtr = IntPtr.Add ( parameters, resultOffset.ToInt32 () );
                    Marshal.StructureToPtr ( resultValue, resultValuePtr, false );
                    break;
                case EventBehaviourGroups.HANDLE_SOM:
                    var somArguments = Marshal.PtrToStructure<SOMParameters> ( parameters );
                    SOMEvent ( somArguments.cmd, somArguments.data );
                    break;
                case EventBehaviourGroups.HANDLE_INITIALIZATION:
                    var initializationArguments = Marshal.PtrToStructure<InitializationParameters> ( parameters );
                    HandleInitializationEvent ( initializationArguments.cmd );
                    return true;
            }

            return false;
        }

        public virtual void AfterRegisterEvent () {

        }

        public virtual void MouseEvent ( MouseEvents command, SciterPoint elementRelated, SciterPoint ViewRelated, KeyboardStates keyboardStates, DraggingType draggingMode, CursorType cursorType, IntPtr target, IntPtr dragging, bool is_on_icon, uint button_state ) {

        }

        public virtual void KeyboardEvent ( KeyEvents command, KeyboardStates keyboardStates ) {

        }

        public virtual void FocusEvent ( FocusEvents command, bool byMouseClick, bool cancel, nint element ) {

        }

        public virtual void ScrollEvent ( ScrollEvents cmd, nint target, int pos, bool vertical, ScrollSource source, uint reason ) {

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

        public virtual void ExchangeParameters ( uint cmd, nint target, nint source, SciterPoint pos, SciterPoint pos_view, uint mode, SciterValue data ) {
        }

        public virtual void MethodCall ( BehaviourMethodIdentifiers methodID ) {
        }

        public virtual void SOMEvent ( SOMEvents cmd, nint data ) {
            if ( cmd == SOMEvents.SOM_GET_PASSPORT ) {
                //	p->data.passport = pThis->asset_get_passport();
                return;
            }

            if ( cmd == SOMEvents.SOM_GET_ASSET ) {
                //	p->data.asset = static_cast<som_asset_t*>(pThis); // note: no add_ref
            }
        }

        public virtual void BehaviourEvent ( BehaviourEvents cmd, nint heTarget, nint he, nint reason, SciterValue data, string name ) {

        }

        public virtual SciterValue ScriptMethodCall ( string? v, IEnumerable<SciterValue> arguments ) {
            var value = new SciterValue ();
            return value;
        }

        public virtual void HandleInitializationEvent ( InitializationEvents cmd ) {
            if ( cmd == InitializationEvents.BEHAVIOR_DETACH ) {
                //we need to handle behaviour detaching and remove event handler from attached list in host
                m_host.RemoveEventHandler ( this );
            }
        }

    }

}

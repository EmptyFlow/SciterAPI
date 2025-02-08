
namespace SciterLibraryAPI {

    public class SciterWindowEventHandler {

        public virtual void MouseEvent ( MouseEvents command, SciterPoint elementRelated, SciterPoint ViewRelated, KeyboardStates keyboardStates, DraggingType draggingMode, CursorType cursorType ) {
            /*
        public IntPtr target;// HELEMENT target element
        public uint button_state;// UINT ->> actually SciterXBehaviors MOUSE_BUTTONS, but for MOUSE_EVENTS.MOUSE_WHEEL event it is the delta
        public bool is_on_icon;// BOOL mouse is over icon (foreground-image, foreground-repeat:no-repeat)
        public IntPtr dragging;// HELEMENT element that is being dragged over, this field is not NULL if (cmd & DRAGGING) != 0
             */
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

        public virtual void MethodCall ( BehaviourMerhodIdentifiers methodID ) {
            throw new NotImplementedException ();
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

        public virtual SciterValue ScriptMethodCall ( string? v, nint argv, uint argc ) {
            var value = new SciterValue ();
            return value;
        }

        public virtual void HandleInitializationEvent ( InitializationEvents cmd ) {

        }

    }

}

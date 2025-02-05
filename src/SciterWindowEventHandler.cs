
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

    }

}

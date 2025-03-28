using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct MouseParameters {
        public MouseEvents cmd;// MOUSE_EVENTS
        public IntPtr target;// HELEMENT target element
        public SciterPoint pos;// POINT position of cursor, element relative
        public SciterPoint pos_view;// POINT position of cursor, view relative
        public uint button_state;// UINT ->> actually SciterXBehaviors MOUSE_BUTTONS, but for MOUSE_EVENTS.MOUSE_WHEEL event it is the delta
        public KeyboardStates alt_state;// UINT 
        public CursorType cursor_type;
        public bool is_on_icon;// BOOL mouse is over icon (foreground-image, foreground-repeat:no-repeat)
        public IntPtr dragging;// HELEMENT element that is being dragged over, this field is not NULL if (cmd & DRAGGING) != 0
        public DraggingType dragging_mode;
    }

}

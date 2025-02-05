namespace SciterLibraryAPI {

    public enum MouseEvents : uint {
        MOUSE_ENTER = 0,
        MOUSE_LEAVE,
        MOUSE_MOVE,
        MOUSE_UP,
        MOUSE_DOWN,
        MOUSE_DCLICK,
        MOUSE_WHEEL,
        MOUSE_TICK,
        MOUSE_IDLE,

        DROP = 9, /* obsolete */
        DRAG_ENTER = 0xA, /* obsolete */
        DRAG_LEAVE = 0xB, /* obsolete */
        DRAG_REQUEST = 0xC, /* obsolete */

        MOUSE_CLICK = 0xFF,
        DRAGGING = 0x100, /* obsolete */
        MOUSE_TCLICK = 0xF, // triple click
        MOUSE_HIT_TEST = 0xFFE // sent to element, allows to handle elements with non-trivial shapes.
    }

}

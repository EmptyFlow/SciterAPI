using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct GestureParameters { // Original name GESTURE_PARAMS
        public uint cmd;                        // GESTURE_EVENTS
        public IntPtr target;               // target element
        public SciterPoint pos;      // position of cursor, element relative
        public SciterPoint pos_view; // position of cursor, view relative
    }

}

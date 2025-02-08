using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct ScrollParameters { // Original name SCROLL_PARAMS
        public ScrollEvents cmd;
        public IntPtr target;// HELEMENT
        public int pos;
        public bool vertical;
        public ScrollSource source;    // SCROLL_SOURCE
        public uint reason; // key or SCROLLBAR_PART
    }

}

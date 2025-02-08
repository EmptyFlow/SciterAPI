using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct DrawParameters { // Original name DRAW_PARAMS
        public DrawEvents cmd;
        public IntPtr gfx;  // HGFX - hdc to paint on
        public SciterRectangle area;  // element area, to get invalid area to paint use GetClipBox,
        public uint reserved;   // for DRAW_BACKGROUND/DRAW_FOREGROUND - it is a border box
                                // for DRAW_CONTENT - it is a content box
    }

}

using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterSize {
        public int cx;
        public int cy;

        public SciterSize ( int x, int y ) {
            cx = x;
            cy = y;
        }
    }

}

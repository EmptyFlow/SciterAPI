using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterSize { // Original name tagSIZE, SIZE, *PSIZE, *LPSIZE
        public int cx;
        public int cy;

        public SciterSize ( int x, int y ) {
            cx = x;
            cy = y;
        }
    }

}

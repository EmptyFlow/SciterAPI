using System.Runtime.InteropServices;

namespace SciterLibraryAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterPoint {
        public int X;
        public int Y;

        public SciterPoint ( int x, int y ) {
            X = x;
            Y = y;
        }
    }

}

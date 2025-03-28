using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

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

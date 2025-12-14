using System.Numerics;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterRectangle {
        public SciterRectangle ( int left, int top, int right, int bottom ) {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public SciterRectangle ( int right, int bottom ) {
            Left = 0;
            Top = 0;
            Right = right;
            Bottom = bottom;
        }

        public int Left, Top, Right, Bottom;

        public int Width => Right - Left;

        public int Height => Bottom - Top;

        public Vector2 LeftTopCorner => new Vector2 ( Left, Top );

        public Vector2 RightBottomCorner => new Vector2 ( Right, Bottom );

    }

}

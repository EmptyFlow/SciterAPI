using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct SOMParameters {
        public SOMEvents Command;
        public nint Data;
    }

}

using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct SOMParameters { // original name SOM_PARAMS
        public SOMEvents Command;
        public nint Data;
    }

}

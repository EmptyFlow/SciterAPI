using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct MethodParameters { // Original name METHOD_PARAMS
        public BehaviourMethodIdentifiers methodID;
    }

}

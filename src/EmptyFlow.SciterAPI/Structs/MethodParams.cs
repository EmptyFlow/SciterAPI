using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct MethodParams {
        public BehaviourMethodIdentifiers methodID;
    }

}

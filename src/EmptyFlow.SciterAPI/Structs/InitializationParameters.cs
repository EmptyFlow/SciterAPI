using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct InitializationParameters { // Original name INITIALIZATION_PARAMS
        public InitializationEvents cmd;
    }

}

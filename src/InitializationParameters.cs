using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct InitializationParameters { // Original name INITIALIZATION_PARAMS
        public InitializationEvents cmd;
    }

}

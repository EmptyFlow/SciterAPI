using System.Runtime.InteropServices;

namespace SciterLibraryAPI {

    [StructLayout ( LayoutKind.Sequential )]
    internal struct KeyParams { // original name KEY_PARAMS
        public KeyEvents cmd;
        public IntPtr target; // target element
        public uint key_code;
        public KeyboardStates alt_state;
    }

}

using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct RequestParam {
        [MarshalAs ( UnmanagedType.LPWStr )]
        public string name;
        [MarshalAs ( UnmanagedType.LPWStr )]
        public string value;
    }

}

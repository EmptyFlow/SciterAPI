using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct DataArrivedParameters { // Original name DATA_ARRIVED_PARAMS
        public IntPtr initiator;// HELEMENT
        public byte[] data;// LPCBYTE
        public uint dataSize;
        public uint dataType;
        public uint status;
        [MarshalAs ( UnmanagedType.LPWStr )]
        public string uri;// LPCWSTR
    }

}

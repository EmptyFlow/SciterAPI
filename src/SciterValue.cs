using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterValue {
        public uint t;// type: enum VALUE_TYPE
        public uint u;// unit
        public ulong d;// data
    }

}

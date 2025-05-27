using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomMethodDefinition { // Original name som_method_def_t
        public nint reserved;
        public ulong Name;
        public ulong Parameters;
        public SomMethod Function;
    }

}

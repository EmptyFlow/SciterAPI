using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomMethodDefinition { // Original name som_method_def_t
        IntPtr reserved;
        ulong name;
        IntPtr @params; /* size_t */
        SomMethod func;
    }

}

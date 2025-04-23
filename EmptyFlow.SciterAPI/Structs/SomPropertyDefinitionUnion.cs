using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomPropertyDefinitionUnion {
        SomPropertyDefinitionUnionDelegates accs;
        int i32;
        long i64;
        double f64;
        IntPtr str; /* const char* */
    }

}

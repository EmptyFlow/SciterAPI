using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomPropertyDefinitionAccessors {
        public SomPropertyGetter Getter;
        public SomPropertySetter Setter;
    }

}

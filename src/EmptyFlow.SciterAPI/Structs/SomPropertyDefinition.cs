using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomPropertyDefinition { // Original name som_property_def_t
        public SomPropertyType Type;
        public ulong Name;
        public SomPropertyDefinitionAccessors Accessors;
    }

}

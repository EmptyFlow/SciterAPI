using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomPropertyDefinition { // Original name som_property_def_t
        SomPropertyType type;
        ulong name;
        SomPropertyDefinitionUnion u;
    }

}

using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomPropertyDefinitionUnionDelegates {
        SomPropertyGetterOrSetter som_prop_getter_t;
        SomPropertyGetterOrSetter som_prop_setter_t;
    }

}

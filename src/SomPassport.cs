using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomPassport { // Original name  som_passport_t
        ulong flags;
        ulong name;

        SomPropertyDefinition properties;
        SomMethodDefinition methods;

        SomItemGetterOrSetter item_getter;
        SomItemGetterOrSetter item_setter;
        SomItemGetterOrSetter item_next;

        SomAnyPropertyGetterOrSetter prop_getter;
        SomAnyPropertyGetterOrSetter prop_setter;

        SomNameResolver name_resolver;

        IntPtr reserved;
    }

}

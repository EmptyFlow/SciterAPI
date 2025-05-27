using EmptyFlow.SciterAPI.Enums;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {

    [StructLayout ( LayoutKind.Sequential )]
    public struct SomPassport { // Original name  som_passport_t
        public SomPassportFlags flags;
        public ulong name;

        public nint Properties;
        public ulong NumberProperties;

        public nint Methods;
        public ulong NumberMethods;

        public nint ItemGetter;
        public nint ItemSetter;
        public nint ItemNext;

        public nint PropertyGetter;
        public nint PropertySetter;

        public nint NameResolver;

        public nint reserved;
    }

}

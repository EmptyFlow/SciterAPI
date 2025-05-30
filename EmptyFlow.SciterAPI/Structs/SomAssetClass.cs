using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomAssetClass { // Original name som_asset_class_t
        public nint AssetAddRef;
        public nint AssetRelease;
        public nint AssetGetInterface;
        public nint AssetGetPassport;
    }

}

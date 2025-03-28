using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SomAssetClass { // Original name som_asset_class_t
        AssetAddOrReleasesDelegate asset_add_ref;
        AssetAddOrReleasesDelegate asset_release;
        AssetGetInterface asset_get_interface;
        AssetGetPassport asset_get_passport;
    }

}

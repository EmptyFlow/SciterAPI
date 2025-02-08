using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SOMParameters {
        public SOMEvents cmd; // SOM_EVENTS
        public IntPtr data;
        /*union {
            som_passport_t* passport;
            som_asset_t*    asset;
        } data;*/
    }

}

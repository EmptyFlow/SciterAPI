using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterCallbackNotificationLoadData { // original name LPSCN_LOAD_DATA
        public SciterCallbackNotificationCode code;
        public IntPtr hwnd; /**< [in] HWINDOW of the window this callback was attached to.*/
        [MarshalAs ( UnmanagedType.LPWStr )] public string uri; // [in] Zero terminated string, fully qualified uri, for example "http://server/folder/file.ext".*/
        IntPtr outData; // [in,out] pointer to loaded data to return. if data exists in the cache then this field contain pointer to it
        uint outDataSize; // [in,out] loaded data size to return.
        uint dataType; // [in] SciterResourceType
        IntPtr requestId; // [in] request handle that can be used with sciter-x-request API */
        IntPtr principal; // Element
        IntPtr initiator; // Initiator
    }

}

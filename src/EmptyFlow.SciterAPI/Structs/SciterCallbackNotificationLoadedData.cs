using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterCallbackNotificationLoadedData { // original name SCN_DATA_LOADED
        public SciterCallbackNotificationCode code;
        public IntPtr hwnd; /**< [in] HWINDOW of the window this callback was attached to.*/
        [MarshalAs ( UnmanagedType.LPWStr )] public string uri; // [in] Zero terminated string, fully qualified uri, for example "http://server/folder/file.ext".*/
        public IntPtr data; // [in] pointer to loaded data.
        public uint dataSize; // [in] loaded data size (in bytes).
        public uint dataType; // [in] SciterResourceType
        public uint status; // [in] status = 0 (dataSize == 0) - unknown error. status = 100..505 - http response status, Note: 200 - OK! status > 12000 - wininet error code, see ERROR_INTERNET_*** in wininet.h
        public uint requestId; // [in] request handle that can be used with sciter-x-request API
    }

}

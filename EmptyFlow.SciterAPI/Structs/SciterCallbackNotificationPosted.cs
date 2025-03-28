using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterCallbackNotificationPosted { // Original name SCN_POSTED_NOTIFICATION
        public uint code; /**< [in] one of the codes above.*/
        public IntPtr hwnd; /**< [in] HWINDOW of the window this callback was attached to.*/
        public IntPtr wparam;
        public IntPtr lparam;
        public IntPtr lreturn;
    }

}

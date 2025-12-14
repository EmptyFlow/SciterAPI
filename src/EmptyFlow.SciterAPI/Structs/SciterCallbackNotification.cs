using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterCallbackNotification { // original name SCITER_CALLBACK_NOTIFICATION
        public SciterCallbackNotificationCode code;
        public IntPtr hwnd; /**< [in] HWINDOW of the window this callback was attached to.*/
    }

}

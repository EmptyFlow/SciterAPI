using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct SciterCallbackNotificationAttachBehaviour { // Original name SCN_ATTACH_BEHAVIOR
        public uint code; /**< [in] one of the codes above.*/
        public IntPtr hwnd; /**< [in] HWINDOW of the window this callback was attached to.*/
        public IntPtr element; // [in] target DOM element handle
        [MarshalAs ( UnmanagedType.LPTStr )] public string behaviorName; // [in] zero terminated string, string appears as value of CSS behavior:"???" attribute.
        public IntPtr elementProc; // [out] pointer to ElementEventProc function.
        public IntPtr elementTag; // [out] tag value, passed as is into pointer ElementEventProc function.
    }

}

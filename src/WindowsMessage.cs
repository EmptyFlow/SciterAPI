using System.Runtime.InteropServices;

namespace SciterLibraryAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct WindowsMessage {
        public IntPtr hwnd;
        public UInt32 message;
        public IntPtr wParam;
        public IntPtr lParam;
        public UInt32 time;
        public SciterPoint pt;
    }

}

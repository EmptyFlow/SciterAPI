using System.Runtime.InteropServices;

namespace SciterLibraryAPI.SystemApis {

    internal static class WindowsApis {

        [DllImport ( "user32.dll" )]
        public static extern bool ShowWindow ( IntPtr hwnd, ShowWindowCommands nCmdShow );

        [DllImport ( "user32.dll" )]
        public static extern bool TranslateMessage ( [In] ref WindowsMessage lpMsg );

        [DllImport ( "user32.dll" )]
        public static extern IntPtr DispatchMessage ( [In] ref WindowsMessage lpmsg );

        [DllImport ( "user32.dll" )]
        public static extern sbyte GetMessage ( out WindowsMessage lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax );

    }

}

using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Client {

    public static class WindowsExtras {

        [DllImport ( "kernel32.dll" )]
        static extern IntPtr GetConsoleWindow ();

        [DllImport ( "user32.dll" )]
        static extern bool ShowWindow ( IntPtr hWnd, int nCmdShow );

        private const int NotVisible = 0;

        private const int Visible = 5;

        public static void HideConsoleWindow () {
            var handle = GetConsoleWindow ();

            ShowWindow ( handle, NotVisible );
        }

        public static void ShowConsoleWindow () {
            var handle = GetConsoleWindow ();

            ShowWindow ( handle, Visible );
        }

    }

}

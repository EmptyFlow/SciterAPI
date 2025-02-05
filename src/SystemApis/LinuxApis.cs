using System.Runtime.InteropServices;

namespace SciterLibraryAPI.SystemApis {

    internal static class LinuxApis {

        private const string LibGtkLibrary = "libgtk-4.so";

        [DllImport ( LibGtkLibrary, CallingConvention = CallingConvention.Cdecl )]
        public static extern void gtk_init ( IntPtr argc, IntPtr argv );

        [DllImport ( LibGtkLibrary, CallingConvention = CallingConvention.Cdecl )]
        public static extern void gtk_main ();

        [DllImport ( LibGtkLibrary, CallingConvention = CallingConvention.Cdecl )]
        public static extern IntPtr gtk_widget_get_toplevel ( IntPtr widget );

        [DllImport ( LibGtkLibrary, CallingConvention = CallingConvention.Cdecl )]
        public static extern void gtk_widget_destroy ( IntPtr widget );

    }

}

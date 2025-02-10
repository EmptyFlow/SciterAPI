using System.Runtime.InteropServices;

namespace SciterLibraryAPI.SystemApis {

    internal static class LinuxApis {

        private const string LibGtk30Library = "libgtk-3.so.0";

        [DllImport ( LibGtk30Library, CallingConvention = CallingConvention.Cdecl )]
        public static extern void gtk_init ( IntPtr argc, IntPtr argv );

        [DllImport ( LibGtk30Library, CallingConvention = CallingConvention.Cdecl )]
        public static extern void gtk_main ();

        [DllImport ( LibGtk30Library, CallingConvention = CallingConvention.Cdecl )]
        public static extern IntPtr gtk_widget_get_toplevel ( IntPtr widget );

        private const string LibGtk40Library = "libgtk-4.so";

        [DllImport ( LibGtk40Library, CallingConvention = CallingConvention.Cdecl )]
        public static extern uint g_list_model_get_n_items ( IntPtr list );

        [DllImport ( LibGtk40Library, CallingConvention = CallingConvention.Cdecl )]
        public static extern void g_main_context_iteration ( IntPtr context, bool may_block);

        [DllImport ( LibGtk40Library, CallingConvention = CallingConvention.Cdecl )]
        public static extern IntPtr gtk_window_get_toplevels ();

        [DllImport ( LibGtk40Library, CallingConvention = CallingConvention.Cdecl )]
        public static extern void gtk_window_present (IntPtr window);

    }

}

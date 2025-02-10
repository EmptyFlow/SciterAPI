using System.Runtime.InteropServices;

namespace SciterLibraryAPI.SystemApis {

    internal static class LinuxApis {

        private const string LibGtkLibrary = "libgtk-4.so";

        [DllImport ( LibGtkLibrary, CallingConvention = CallingConvention.Cdecl )]
        public static extern uint g_list_model_get_n_items ( IntPtr list );

        [DllImport ( LibGtkLibrary, CallingConvention = CallingConvention.Cdecl )]
        public static extern void g_main_context_iteration ( IntPtr context, bool may_block);

        [DllImport ( LibGtkLibrary, CallingConvention = CallingConvention.Cdecl )]
        public static extern IntPtr gtk_window_get_toplevels ();

        [DllImport ( LibGtkLibrary, CallingConvention = CallingConvention.Cdecl )]
        public static extern void gtk_widget_destroy ( IntPtr widget );

    }

}

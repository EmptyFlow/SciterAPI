using EmptyFlow.SciterAPI.Client;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    public class SciterLoader {

        public const string SciterLibrary = "SciterLibrary";

        private static IntPtr m_sciterLoadedHandle = IntPtr.Zero;

        private static string m_sciterPath = "";

        private const string LinuxName = "libsciter.so";

        private const string WindowsName = "sciter.dll";

        private const string MacOSName = "libsciter.dylib";

        private static bool m_isInitialized = false;

        public static bool IsInitialized => m_isInitialized;

        /// <summary>
        /// Initialize 
        /// </summary>
        /// <param name="sciterPath">Path to folder where will be located sciter dynamic library file.</param>
        /// <param name="hideConsoleWindow">Hide console window for release (and restore for debug), it actual for Windows OS.</param>
        public static void Initialize ( string sciterPath, bool hideConsoleWindow = true ) {
            if ( m_isInitialized ) return;

            m_isInitialized = true;
            m_sciterPath = sciterPath;
            NativeLibrary.SetDllImportResolver ( typeof ( SciterAPIHost ).Assembly, ImportResolver );
#if !DEBUG
            if ( hideConsoleWindow ) HideConsoleWindow ();
#else
            if ( hideConsoleWindow ) ShowConsoleWindow ();
#endif
        }

        /// <summary>
        /// Hide console window for Windows OS.
        /// </summary>
        public static void HideConsoleWindow () {
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Windows ) ) WindowsExtras.HideConsoleWindow ();
        }

        /// <summary>
        /// Show console window for Windows OS.
        /// </summary>
        public static void ShowConsoleWindow () {
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Windows ) ) WindowsExtras.ShowConsoleWindow ();
        }


        private static bool TryLoadLibrary () {
            var libraryName = "";
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Windows ) ) libraryName = WindowsName;
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Linux ) ) libraryName = LinuxName;
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.OSX ) ) libraryName = MacOSName;

            if ( string.IsNullOrEmpty ( libraryName ) ) throw new NotSupportedException ( "You operating system not supported!" );

            if ( NativeLibrary.TryLoad ( Path.Combine ( m_sciterPath, libraryName ), out var libHandle ) ) {
                m_sciterLoadedHandle = libHandle;
                return true;
            }

            return false;
        }

        private static IntPtr ImportResolver ( string libraryName, Assembly assembly, DllImportSearchPath? searchPath ) {
            if ( libraryName == SciterLibrary ) {
                if ( m_sciterLoadedHandle != IntPtr.Zero ) return m_sciterLoadedHandle;

                if ( TryLoadLibrary () ) return m_sciterLoadedHandle;
            }
            return IntPtr.Zero;
        }

    }

}

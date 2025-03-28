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

        public static void Initialize ( string sciterPath ) {
            m_sciterPath = sciterPath;
            NativeLibrary.SetDllImportResolver ( typeof ( SciterAPIHost ).Assembly, ImportResolver );
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
            if ( libraryName == SciterLibrary) {
                if ( m_sciterLoadedHandle != IntPtr.Zero ) return m_sciterLoadedHandle;

                if ( TryLoadLibrary () ) return m_sciterLoadedHandle;
            }
            return IntPtr.Zero;
        }

    }

}

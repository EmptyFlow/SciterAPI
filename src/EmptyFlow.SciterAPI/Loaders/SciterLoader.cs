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
		/// Try to initialize from folder in file system.
		/// </summary>
		/// <param name="sciterPath">Path to folder where will be located sciter dynamic library file.</param>
		public static void Initialize ( string sciterPath ) {
			if ( m_isInitialized ) return;

			m_isInitialized = true;
			m_sciterPath = sciterPath;
			NativeLibrary.SetDllImportResolver ( typeof ( SciterAPIHost ).Assembly, ImportResolver );
		}

		public static void InitializeFromEmbedded ( string sciterPath, string savePath, Assembly assembly ) {
			if ( m_isInitialized ) return;

			var libraryName = GetLibraryPlatformName ();
			var fullSavedPath = Path.Combine ( savePath, libraryName );

			using var stream = assembly.GetManifestResourceStream ( sciterPath + "/" + libraryName );
			if ( stream == null ) throw new NotSupportedException ( "Not found sciter library in embedded resources!" );

			if ( File.Exists ( fullSavedPath ) ) File.Delete ( fullSavedPath );

			try {
				using var file = File.OpenWrite ( fullSavedPath );
				stream.CopyTo ( file );
				file.Close ();
			} catch ( Exception e ) {
				throw new Exception ( $"Can't save sciter library to {fullSavedPath}, check inner exception for details!", e );
			}

			m_isInitialized = true;
			m_sciterPath = sciterPath;
			NativeLibrary.SetDllImportResolver ( typeof ( SciterAPIHost ).Assembly, ImportResolver );
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

		private static string GetLibraryPlatformName () {
			var libraryName = "";
			if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Windows ) ) libraryName = WindowsName;
			if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Linux ) ) libraryName = LinuxName;
			if ( RuntimeInformation.IsOSPlatform ( OSPlatform.OSX ) ) libraryName = MacOSName;

			if ( string.IsNullOrEmpty ( libraryName ) ) throw new NotSupportedException ( "You operating system not supported!" );

			return libraryName;
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

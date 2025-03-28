using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct ScriptingMethodParameters { // Original name SCRIPTING_METHOD_PARAMS
        public IntPtr name;// LPCSTR
        public IntPtr argv;// VALUE*
        public uint argc;
        public SciterValue result;    // plz note, Sciter will internally call ValueClear to this VALUE,
                                      // that is, it own this data, so always assign a copy with a positive ref-count of your VALUE to this variable
                                      // you will know that if you get an "Access Violation" error
    }

}

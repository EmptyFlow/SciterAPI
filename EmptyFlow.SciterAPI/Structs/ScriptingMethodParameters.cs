using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct ScriptingMethodParameters { // Original name SCRIPTING_METHOD_PARAMS
        public IntPtr name;// LPCSTR method name
        public IntPtr argv;// VALUE* vector of arguments
        public uint argc; // argument count
        public SciterValue result; // return value
    }

}

using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct ExchangeParameters { // Original name EXCHANGE_PARAMS
        public uint cmd;                    // EXCHANGE_EVENTS
        public IntPtr target;                   // target element
        public IntPtr source;                   // source element (can be null if D&D from external window)
        public SciterPoint pos;      // position of cursor, element relative
        public SciterPoint pos_view; // position of cursor, view relative
        public uint mode;                   // DD_MODE 
        public SciterValue data;      // packaged drag data
    }

}

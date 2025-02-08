using System.Runtime.InteropServices;

namespace SciterLibraryAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct TimerParameters { // Original name TIMER_PARAMS
        public IntPtr timerId;// UINT_PTR
    }

}

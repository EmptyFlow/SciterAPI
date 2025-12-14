using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct TimerParameters { // Original name TIMER_PARAMS
        public IntPtr timerId;// UINT_PTR
    }

}

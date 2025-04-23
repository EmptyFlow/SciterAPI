using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {
    /// <summary>
    /// Original name SC_COLOR_STOP.
    /// </summary>
    [StructLayout ( LayoutKind.Sequential )]
    public struct ColorStop {

        uint color;

        /// <summary>
        /// 0.0 ... 1.0.
        /// </summary>
        float offset;
    }

}

﻿using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {
    [StructLayout ( LayoutKind.Sequential )]
    public struct FocusParameters { // original name FOCUS_PARAMS
        public FocusEvents cmd;
        public IntPtr target;// HELEMENT
        public bool by_mouse_click;
        public bool cancel;
    }

}

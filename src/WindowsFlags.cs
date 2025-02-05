namespace SciterLibraryAPI {

    [Flags]
    public enum WindowsFlags : uint {
        /// <summary>Child window only, if this flag is set all other flags ignored</summary>
        Child = ( 1 << 0 ),
        /// <summary>Toplevel window, has titlebar</summary>
        Titlebar = ( 1 << 1 ),
        /// <summary>Has resizeable frame</summary>
        Resizeable = ( 1 << 2 ),
        /// <summary>Is tool window</summary>
        Tool = ( 1 << 3 ),
        /// <summary>Has minimize / maximize buttons</summary>
        Controls = ( 1 << 4 ),
        /// <summary>Glassy window ( DwmExtendFrameIntoClientArea on windows )</summary>
        Glassy = ( 1 << 5 ),
        /// <summary>Transparent window ( e.g. WS_EX_LAYERED on Windows )</summary>
        Alpha = ( 1 << 6 ),
        /// <summary>Main window of the app, will terminate app on close</summary>
        Main = ( 1 << 7 ),
        ///<summary> The window is created as topmost</summary>
        Popup = ( 1 << 8 ),
        /// <summary>Make this window inspector ready</summary>
        Debug = ( 1 << 9 ),
        /// <summary>It has its own script VM</summary>
        OwnVM = ( 1 << 10 )
    }

    public enum Win32Messge : uint {
        WM_CREATE = 0x0001,
        WM_CLOSE = 0x0010,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_SETICON = 0x0080,
        WM_NCCALCSIZE = 0x0083
    }

}

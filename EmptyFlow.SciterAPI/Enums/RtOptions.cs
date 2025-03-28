namespace EmptyFlow.SciterAPI {

    public enum RtOptions : uint {
        /// <summary>value:TRUE - enable, value:FALSE - disable, enabled by default</summary>
        SCITER_SMOOTH_SCROLL = 1,

        /// <summary>value: milliseconds, connection timeout of http client</summary>
        SCITER_CONNECTION_TIMEOUT = 2,

        /// <summary>value: 0 - drop connection, 1 - use builtin dialog, 2 - accept connection silently</summary>
        SCITER_HTTPS_ERROR = 3,

        /// <summary>value: 0 - system default, 1 - no smoothing, 2 - std smoothing, 3 - clear type</summary>
        SCITER_FONT_SMOOTHING = 4,

        /// <summary>
        /// Windows Aero support, value:
        /// 0 - normal drawing,
        /// 1 - window has transparent background after calls DwmExtendFrameIntoClientArea() or DwmEnableBlurBehindWindow()
        /// </summary>
        SCITER_TRANSPARENT_WINDOW = 6,  // Windows Aero support, value:
                                        // 0 - normal drawing,
                                        // 1 - window has transparent background after calls DwmExtendFrameIntoClientArea() or DwmEnableBlurBehindWindow().

        /// <summary>
        /// hWnd = NULL,
        /// value = LPCBYTE, json - GPU black list, see: gpu-blacklist.json resource.
        /// </summary>
        SCITER_SET_GPU_BLACKLIST = 7,

        /// <summary>value - combination of SCRIPT_RUNTIME_FEATURES flags.</summary>
        SCITER_SET_SCRIPT_RUNTIME_FEATURES = 8,

        /// <summary>hWnd = NULL, value - GFX_LAYER</summary>
        SCITER_SET_GFX_LAYER = 9,

        /// <summary>hWnd, value - TRUE/FALSE</summary>
        SCITER_SET_DEBUG_MODE = 10,

        /// <summary>
        /// hWnd = NULL, value - BOOL, TRUE - the engine will use "unisex" theme that is common for all platforms.
        /// That UX theme is not using OS primitives for rendering input elements. Use it if you want exactly
        /// the same (modulo fonts) look-n-feel on all platforms.
        /// </summary>
        SCITER_SET_UX_THEMING = 11,

        /// <summary>hWnd, value - TRUE/FALSE - window uses per pixel alpha (e.g. WS_EX_LAYERED/UpdateLayeredWindow() window)</summary>
        SCITER_ALPHA_WINDOW = 12,

        /// <summary>
        ///	hWnd - N/A , value LPCSTR - UTF-8 encoded script source to be loaded into each view before any other script execution.
        /// The engine copies this string inside the call.
        /// </summary>
        SCITER_SET_INIT_SCRIPT = 13,

        /// <summary>
        /// hWnd, value - TRUE/FALSE - window is main, will destroy all other dependent windows on close
        /// </summary>
        SCITER_SET_MAIN_WINDOW = 14,

        /// <summary>
        /// hWnd - N/A , value - max request length in megabytes (1024*1024 bytes)
        /// </summary>
        SCITER_SET_MAX_HTTP_DATA_LENGTH = 15,

        /// <summary>
        /// value 1 - 1px in CSS is treated as 1dip, value 0 - default behavior - 1px is a physical pixel
        /// </summary>
        SCITER_SET_PX_AS_DIP = 16,
    }

}

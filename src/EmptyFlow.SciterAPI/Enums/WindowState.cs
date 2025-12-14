namespace EmptyFlow.SciterAPI {

    /// <summary>
    /// Window state, can be used for get or set windows state. Name in C++ SCITER_WINDOW_STATE.
    /// </summary>
    public enum WindowState {
        SCITER_WINDOW_STATE_CLOSED = 0, // close window
        SCITER_WINDOW_STATE_SHOWN = 1,
        SCITER_WINDOW_STATE_MINIMIZED = 2,
        SCITER_WINDOW_STATE_MAXIMIZED = 3,
        SCITER_WINDOW_STATE_HIDDEN = 4,
        SCITER_WINDOW_STATE_FULL_SCREEN = 5,
    }

}

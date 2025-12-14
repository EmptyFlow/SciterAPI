namespace EmptyFlow.SciterAPI.Enums {

    /// <summary>
    /// Mode for get window size in which monitor related size will be calculated.
    /// </summary>
    public enum WindowRelateMode {

        /// <summary>
        /// Coordinates are relative to desktop (outline of all monitors in the system).
        /// </summary>
        Desktop = 0,

        /// <summary>
        /// Coordinates are relative to the monitor this window is replaced on.
        /// </summary>
        Monitor = 1,

        /// <summary>
        /// Coordinates are relative to the origin of window's client area.
        /// </summary>
        Self = 2,

    };

}

namespace EmptyFlow.SciterAPI {

    /// <summary>
    /// Application command, original name in C++ SCITER_APP_CMD.
    /// </summary>
    public enum ApplicationCommand : uint {

        /// <summary>
        /// reuest to quit message pump loop
        /// </summary>
        SCITER_APP_STOP = 0,

        /// <summary>
        /// run message pump loop until SCITER_APP_STOP or main window closure.
        /// </summary>
        SCITER_APP_LOOP = 1,

        /// <summary>
        /// pass argc/argv to application: p1 - argc, p2 - CHAR** argv.
        /// </summary>
        SCITER_APP_INIT = 2,

        /// <summary>
        /// free resources of the application.
        /// </summary>
        SCITER_APP_SHUTDOWN = 3,

        /// <summary>
        /// scapp mode: load JS and run message pump loop until SCITER_APP_STOP or main window closure, p1 - JS url, p2 - 0 or SciterPrimordialLoader.
        /// </summary>
        SCITER_APP_RUN = 4,

    }

}

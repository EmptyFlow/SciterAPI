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

        /// <summary>
        /// does single message pump loop iteration, SCITER_APP_LOOP is essentially this:
        /// while( SciterExec(SCITER_APP_LOOP_ITERATION,0,0) );
        /// </summary>
        SCITER_APP_LOOP_ITERATION = 6,

        /// <summary>
        /// checks outstanding tasks and timers,
        /// like SCITER_APP_LOOP_ITERATION but without message processing 
        /// </summary>
        SCITER_APP_LOOP_HEARTBIT = 7,

    }

}

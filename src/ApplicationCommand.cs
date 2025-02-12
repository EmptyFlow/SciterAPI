namespace SciterLibraryAPI {
    public enum ApplicationCommand : uint { // original name SCITER_APP_CMD
        SCITER_APP_STOP = 0,     /// reuest to quit message pump loop
        SCITER_APP_LOOP = 1,     /// run message pump loop until SCITER_APP_STOP or main window closure
        SCITER_APP_INIT = 2,     /// pass argc/argv to application: p1 - argc, p2 - CHAR** argv 
        SCITER_APP_SHUTDOWN = 3, /// free resources of the application 
        SCITER_APP_RUN = 4,     /// scapp mode: load JS and run message pump loop until SCITER_APP_STOP or main window closure, p1 - JS url, p2 - 0 or SciterPrimordialLoader; 
    }

}

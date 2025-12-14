namespace EmptyFlow.SciterAPI.Enums {
    public enum RequestResult {
        Panic = -1, // REQUEST_PANIC, e.g. not enough memory
        Ok = 0, // REQUEST_OK
        BadParam = 1, // REQUEST_BAD_PARAM,  // bad parameter
        Failure = 2, // REQUEST_FAILURE, operation failed, e.g. index out of bounds
        NotSupported = 3, // REQUEST_NOTSUPPORTED, the platform does not support requested feature
    };

}

namespace EmptyFlow.SciterAPI.Enums {
    /// <summary>
    /// Original name GRAPHIN_RESULT.
    /// </summary>
    public enum GraphInResult {
        Panic = -1, // GRAPHIN_PANIC e.g. not enough memory
        Ok = 0, // GRAPHIN_OK
        BadParameter = 1,  // GRAPHIN_BAD_PARAM bad parameter
        Failure = 2,    // GRAPHIN_FAILURE operation failed, e.g. restore() without save()
        NotSupported = 3 // GRAPHIN_NOTSUPPORTED the platform does not support requested feature
    };

}

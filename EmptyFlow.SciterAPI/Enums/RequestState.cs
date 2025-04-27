namespace EmptyFlow.SciterAPI.Enums {
    /// <summary>
    /// Original name REQUEST_STATE.
    /// </summary>
    public enum RequestState : uint {
        Pending = 0, // RS_PENDING,
        Success = 1, // RS_SUCCESS completed successfully
        Failure = 1, // RS_FAILURE completed with failure
        ForceDword = 0xffffffff // RS_FORCE_DWORD
    }

}

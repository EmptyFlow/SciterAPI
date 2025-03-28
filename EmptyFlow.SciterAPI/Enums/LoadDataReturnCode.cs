namespace EmptyFlow.SciterAPI {
    public enum LoadDataReturnCode : uint { // Original name SC_LOAD_DATA_RETURN_CODES
        LOAD_OK = 0,      /**< do default loading if data not set */
        LOAD_DISCARD = 1, /**< discard request completely */
        LOAD_DELAYED = 2, /**< data will be delivered later by the host application.
                         Host application must call SciterDataReadyAsync(,,, requestId) on each LOAD_DELAYED request to avoid memory leaks. */
        LOAD_MYSELF = 3, /**< you return LOAD_MYSELF result to indicate that your (the host) application took or will take care about HREQUEST in your code completely.
                         Use sciter-x-request.h[pp] API functions with SCN_LOAD_DATA::requestId handle . */
    };

}

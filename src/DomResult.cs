namespace SciterLibraryAPI {

    public enum DomResult : int {
        SCDOM_OK = 0,
        SCDOM_INVALID_HWND = 1,
        SCDOM_INVALID_HANDLE = 2,
        SCDOM_PASSIVE_HANDLE = 3,
        SCDOM_INVALID_PARAMETER = 4,
        SCDOM_OPERATION_FAILED = 5,
        SCDOM_OK_NOT_HANDLED = ( -1 )
    }

}

namespace SciterLibraryAPI {


    [Flags]
    public enum SciterRuntimeFeatures : uint {
        ALLOW_FILE_IO = 0x00000001,
        ALLOW_SOCKET_IO = 0x00000002,
        ALLOW_EVAL = 0x00000004,
        ALLOW_SYSINFO = 0x00000008
    }

}

namespace EmptyFlow.SciterAPI.Enums {

    public enum SciterResourceType : uint {
        Html = 0, // RT_DATA_HTML
        Image = 1, // RT_DATA_IMAGE
        Style = 2, // RT_DATA_STYLE
        Cursor = 3, // RT_DATA_CURSOR
        Script = 4, //RT_DATA_SCRIPT
        Raw = 5, //RT_DATA_RAW
        Font, // RT_DATA_FONT
        Media, // RT_DATA_MEDIA video, audio, lottie, etc.
        ForceDword = 0xffffffff // RT_DATA_FORCE_DWORD
    }

}
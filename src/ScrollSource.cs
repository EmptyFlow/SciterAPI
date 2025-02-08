namespace SciterLibraryAPI {
    public enum ScrollSource { // Original name SCROLL_SOURCE
        SCROLL_SOURCE_UNKNOWN,
        SCROLL_SOURCE_KEYBOARD,  // SCROLL_PARAMS::reason <- keyCode
        SCROLL_SOURCE_SCROLLBAR, // SCROLL_PARAMS::reason <- SCROLLBAR_PART 
        SCROLL_SOURCE_ANIMATOR,
        SCROLL_SOURCE_WHEEL,
    }

}

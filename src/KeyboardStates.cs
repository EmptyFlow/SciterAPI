namespace SciterLibraryAPI {

    [Flags]
    public enum KeyboardStates : uint { // original name KEYBOARD_STATES
        CONTROL_KEY_PRESSED = 0x1,
        SHIFT_KEY_PRESSED = 0x2,
        ALT_KEY_PRESSED = 0x4
    }

}

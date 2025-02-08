namespace SciterLibraryAPI {
    // Sciter or TIScript specific
    public enum ValueUnitTypeString : uint { // Original name VALUE_UNIT_TYPE_STRING
        UT_STRING_STRING = 0,       // string
        UT_STRING_ERROR = 1,        // is an error string
        UT_STRING_SECURE = 2,       // secure string ("wiped" on destroy)
        UT_STRING_SYMBOL = 0xffff   // symbol in tiscript sense
    }

}

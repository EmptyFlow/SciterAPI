namespace EmptyFlow.SciterAPI {
    public enum ValueType : uint { // Original name VALUE_TYPE
        T_UNDEFINED = 0,
        T_NULL = 1,
        T_BOOL = 2,
        T_INT = 3,
        T_FLOAT = 4,
        T_STRING = 5,
        T_DATE = 6,     // INT64 - contains a 64-bit value representing the number of 100-nanosecond intervals since January 1, 1601 (UTC), a.k.a. FILETIME on Windows
        T_CURRENCY = 7, // INT64 - 14.4 fixed number. E.g. dollars = int64 / 10000; 
        T_LENGTH = 8,   // length units, value is int or float, units are VALUE_UNIT_TYPE
        T_ARRAY = 9,
        T_MAP = 10,
        T_FUNCTION = 11,    // named tuple , like array but with name tag
        T_BYTES = 12,       // sequence of bytes - e.g. image data
        T_OBJECT = 13,      // scripting object proxy (TISCRIPT/SCITER)
        T_DOM_OBJECT = 14,  // DOM object (CSSS!), use get_object_data to get HELEMENT 
        T_RESOURCE = 15,  // 15 - other thing derived from tool::resource
                          //T_RANGE = 16,     // 16 - N..M, integer range.
        T_DURATION = 17,    // double, seconds
        T_ANGLE = 18,       // double, radians
        T_COLOR = 19,       // [unsigned] INT, ABGR
        T_ASSET = 21,      // sciter::om::iasset* add_ref'ed pointer
    }

}

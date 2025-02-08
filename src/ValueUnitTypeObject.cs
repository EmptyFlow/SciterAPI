namespace SciterLibraryAPI {
    public enum ValueUnitTypeObject : uint { // Original name VALUE_UNIT_TYPE_OBJECT
        UT_OBJECT_ARRAY = 0,   // type T_OBJECT of type Array
        UT_OBJECT_OBJECT = 1,   // type T_OBJECT of type Object
        UT_OBJECT_CLASS = 2,   // type T_OBJECT of type Class (class or namespace)
        UT_OBJECT_NATIVE = 3,   // type T_OBJECT of native Type with data slot (LPVOID)
        UT_OBJECT_FUNCTION = 4, // type T_OBJECT of type Function
        UT_OBJECT_ERROR = 5,    // type T_OBJECT of type Error
        UT_OBJECT_BUFFER = 6,   // type T_OBJECT of type ArrayBuffer or TypedArray
    }

}

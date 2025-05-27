namespace EmptyFlow.SciterAPI.Enums {

    [Flags]
    public enum SomPassportFlags : ulong { //som_passport_flags
        SOM_SEALED_OBJECT = 0x00,     // not extendable
        SOM_EXTENDABLE_OBJECT = 0x01, // extendable, asset may have new properties added
        SOM_HAS_NAME_RESOLVER = 0x02  // if name_resolver is valid 
    };

}

namespace SciterLibraryAPI {
    public enum ValueUnitTypeDate : uint { // Original name VALUE_UNIT_TYPE_DATE
        DT_HAS_DATE = 0x01, // date contains date portion
        DT_HAS_TIME = 0x02, // date contains time portion HH:MM
        DT_HAS_SECONDS = 0x04, // date contains time and seconds HH:MM:SS
        DT_UTC = 0x10, // T_DATE is known to be UTC. Otherwise it is local date/time
    }

}

namespace EmptyFlow.SciterAPI {
    public enum ValueStringCvtType : uint { // Original name VALUE_STRING_CVT_TYPE
        CVT_SIMPLE,        //< simple conversion of terminal values 
        CVT_JSON_LITERAL,  //< json literal parsing/emission 
        CVT_JSON_MAP,      //< json parsing/emission, it parses as if token '{' already recognized
        CVT_XJSON_LITERAL, //< x-json parsing/emission, date is emitted as ISO8601 date literal, currency is emitted in the form DDDD$CCC
    }

}

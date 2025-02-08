namespace SciterLibraryAPI {
    public enum ValueUnitType : uint { // Original name VALUE_UNIT_TYPE
        UT_EM = 1, //height of the element's font. 
        UT_EX = 2, //height of letter 'x' 
        UT_PR = 3, //%
        UT_SP = 4, //%% "springs", a.k.a. flex units
        reserved1 = 5,
        reserved2 = 6,
        UT_PX = 7, //pixels
        UT_IN = 8, //inches (1 inch = 2.54 centimeters). 
        UT_CM = 9, //centimeters. 
        UT_MM = 10, //millimeters. 
        UT_PT = 11, //points (1 point = 1/72 inches). 
        UT_PC = 12, //picas (1 pica = 12 points). 
        UT_DIP = 13,
        reserved3 = 14,
        reserved4 = 15,

        UT_PR_WIDTH = 16, // width(n%)
        UT_PR_HEIGHT = 17, // height(n%)
        UT_PR_VIEW_WIDTH = 18, // vw
        UT_PR_VIEW_HEIGHT = 19, // vh
        UT_PR_VIEW_MIN = 20, // vmin
        UT_PR_VIEW_MAX = 21, // vmax

        UT_REM = 22, // root em
        UT_PPX = 23, // physical px
        UT_CH = 24   // width of '0'
    }

}

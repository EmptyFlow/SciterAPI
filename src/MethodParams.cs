using System.Runtime.InteropServices;

namespace SciterLibraryAPI {

    public enum BahaviourMethodIdentifiers : uint {
        DO_CLICK = 0,
        /*GET_TEXT_VALUE = 1,
        SET_TEXT_VALUE,
        TEXT_EDIT_GET_SELECTION,
        TEXT_EDIT_SET_SELECTION,
        TEXT_EDIT_REPLACE_SELECTION,
        SCROLL_BAR_GET_VALUE,
        SCROLL_BAR_SET_VALUE,
        TEXT_EDIT_GET_CARET_POSITION, 
        TEXT_EDIT_GET_SELECTION_TEXT,
        TEXT_EDIT_GET_SELECTION_HTML,
        TEXT_EDIT_CHAR_POS_AT_XY,*/
        IS_EMPTY = 0xFC,
        GET_VALUE = 0xFD,
        SET_VALUE = 0xFE,
        FIRST_APPLICATION_METHOD_ID = 0x100
    }

    [StructLayout ( LayoutKind.Sequential )]
    public struct MethodParams {
        public BahaviourMethodIdentifiers methodID;
    }

}

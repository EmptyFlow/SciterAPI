namespace EmptyFlow.SciterAPI.Enums {

    public enum SciterTextPosition : uint {
        BottomLeft = 1, // The x, y coordinates define the bottom-left corner of the text.
        BottomCenter = 2, // The x, y coordinates define the bottom-center of the text.
        BottomRight = 3, // The x, y coordinates define the bottom-right corner of the text.
        MiddleLeft = 4, // The x, y coordinates define the middle-left edge of the text.
        CenterCenter = 5, // The x, y coordinates define the exact center of the text.
        MiddleRight = 6, // The x, y coordinates define the middle-right edge of the text.
        TopLeft = 7, // The x, y coordinates define the top-left corner of the text ( this is the default if pointOf is omitted).
        TopCenter = 8, // The x, y coordinates define the top-center of the text.
        TopRight = 9 // The x, y coordinates define the top-right corner of the text.
    };

}

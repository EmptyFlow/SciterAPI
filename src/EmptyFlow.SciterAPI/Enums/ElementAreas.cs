namespace EmptyFlow.SciterAPI {
    [Flags]
    public enum ElementAreas : uint {
        /// <summary>
        /// `or` this flag if you want to get HTMLayout window relative coordinates,
        /// otherwise it will use nearest windowed container e.g. popup window.
        /// </summary>
        ROOT_RELATIVE = 0x01,

        /// <summary>
        /// `or` this flag if you want to get coordinates relative to the origin
        /// of element itself.
        /// </summary>
        SELF_RELATIVE = 0x02,

        /// <summary>
        /// position inside immediate container.
        /// </summary>
        CONTAINER_RELATIVE = 0x03,

        /// <summary>
        /// position relative to view - HTMLayout window
        /// </summary>
        VIEW_RELATIVE = 0x04,

        /// <summary>
        /// content (inner)  box
        /// </summary>
        CONTENT_BOX = 0x00,

        /// <summary>
        /// content + paddings
        /// </summary>
        PADDING_BOX = 0x10,

        /// <summary>
        /// content + paddings + border
        /// </summary>
        BORDER_BOX = 0x20,

        /// <summary>
        /// content + paddings + border + margins 
        /// </summary>
        MARGIN_BOX = 0x30,

        /// <summary>
        /// relative to content origin - location of background image (if it set no-repeat)
        /// </summary>
        BACK_IMAGE_AREA = 0x40,

        /// <summary>
        /// relative to content origin - location of foreground image (if it set no-repeat)
        /// </summary>
        FORE_IMAGE_AREA = 0x50,

        /// <summary>
        /// scroll_area - scrollable area in content box 
        /// </summary>
        SCROLLABLE_AREA = 0x60,
    }

}

namespace EmptyFlow.SciterAPI {
    public enum SetElementHtml : uint {

        /// <summary>
        /// Replace content of the element.
        /// </summary>
        SIH_REPLACE_CONTENT = 0,

        /// <summary>
        /// Insert html before first child of the element.
        /// </summary>
        SIH_INSERT_AT_START = 1,

        /// <summary>
        /// Insert html after last child of the element.
        /// </summary>
        SIH_APPEND_AFTER_LAST = 2,

        /// <summary>
        /// Replace element by html, a.k.a. element.outerHtml = "something".
        /// </summary>
        /// <remarks>operations do not work for inline elements like SPAN.</remarks>
        SOH_REPLACE = 3,

        /// <summary>
        /// Insert html before the element
        /// </summary>
        /// <remarks>operations do not work for inline elements like SPAN.</remarks>
        SOH_INSERT_BEFORE = 4,

        /// <summary>
        /// Insert html after the element.
        /// </summary>
        /// <remarks>operations do not work for inline elements like SPAN.</remarks>
        SOH_INSERT_AFTER = 5
    }

}

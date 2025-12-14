namespace EmptyFlow.SciterAPI {

    public enum SetElementHtml : uint {

        /// <summary>
        /// Replace content of the element. Original name SIH_REPLACE_CONTENT
        /// </summary>
        ReplaceContent = 0,

        /// <summary>
        /// Insert html before first child of the element. Original name SIH_INSERT_AT_START.
        /// </summary>
        InsertAtStart = 1,

        /// <summary>
        /// Insert html after last child of the element. Original name SIH_APPEND_AFTER_LAST.
        /// </summary>
        AppendAfterStart = 2,

        /// <summary>
        /// Replace element by html, a.k.a. element.outerHtml = "something". Original name SOH_REPLACE.
        /// </summary>
        /// <remarks>operations do not work for inline elements like SPAN.</remarks>
        Replace = 3,

        /// <summary>
        /// Insert html before the element. Original name SOH_INSERT_BEFORE.
        /// </summary>
        /// <remarks>operations do not work for inline elements like SPAN.</remarks>
        InsertBefore = 4,

        /// <summary>
        /// Insert html after the element. Original name SOH_INSERT_AFTER.
        /// </summary>
        /// <remarks>operations do not work for inline elements like SPAN.</remarks>
        InsertAfter = 5

    }

}

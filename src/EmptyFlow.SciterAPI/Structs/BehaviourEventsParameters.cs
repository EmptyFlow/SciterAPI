using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct BehaviourEventsParameters { // Original name BEHAVIOR_EVENT_PARAMS
        /// <summary>
        /// BEHAVIOR_EVENTS
        /// </summary>
        public BehaviourEvents cmd;

        /// <summary>
        /// target element handler, in MENU_ITEM_CLICK this is owner element that caused this menu - e.g. context menu owner
        /// In scripting this field named as Event.owner
        /// </summary>
        public IntPtr heTarget;

        /// <summary>
        /// source element e.g. in SELECTION_CHANGED it is new selected &lt;option&gt;, in MENU_ITEM_CLICK it is menu item (LI) element
        /// </summary>
        public IntPtr he;

        /// <summary>
        /// CLICK_REASON or EDIT_CHANGED_REASON - UI action causing change.
        /// In case of custom event notifications this may be any application specific value.
        /// </summary>
        public IntPtr reason;// UINT_PTR

        /// <summary>
        /// auxiliary data accompanied with the event. E.g. FORM_SUBMIT event is using this field to pass collection of values.
        /// </summary>
        public SciterValue data;// SCITER_VALUE

        /// <summary>
        /// name of custom event (when cmd == CUSTOM)
        /// </summary>
        [MarshalAs ( UnmanagedType.LPWStr )]
        public string name;
    }

}

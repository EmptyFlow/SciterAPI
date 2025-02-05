namespace SciterLibraryAPI {
    public enum FocusEvents : uint { // original name FOCUS_EVENTS
        FOCUS_OUT = 0,              // container got focus on element inside it, target is an element that got focus
        FOCUS_IN = 1,               // container lost focus from any element inside it, target is an element that lost focus
        FOCUS_GOT = 2,              // target element got focus
        FOCUS_LOST = 3,             // target element lost focus
        FOCUS_REQUEST = 4,          // bubbling event/request, gets sent on child-parent chain to accept/reject focus to be set on the child (target)
        FOCUS_ADVANCE_REQUEST = 5,  // bubbling event/request, gets sent on child-parent chain to advance focus 
    }

}

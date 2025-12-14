namespace EmptyFlow.SciterAPI {

    public enum BehaviourEvents : uint { // Original name BEHAVIOR_EVENTS
        BUTTON_CLICK = 0,               // click on button
        BUTTON_PRESS = 1,               // mouse down or key down in button
        VALUE_CHANGED = 2,       // checkbox/radio/slider changed its state/value
        VALUE_CHANGING = 3,        // before text change
        SELECTION_CHANGED = 5,   // selection in <select> changed
        SELECTION_CHANGING = 0xC,       // node in select expanded/collapsed, heTarget is the node

        POPUP_REQUEST = 7,          // request to show popup just received,
                                    //     here DOM of popup element can be modifed.
        POPUP_READY = 8,            // popup element has been measured and ready to be shown on screen,
                                    //     here you can use functions like ScrollToView.
        POPUP_DISMISSED = 9,            // popup element is closed,
                                        //     here DOM of popup element can be modifed again - e.g. some items can be removed
                                        //     to free memory.

        MENU_ITEM_ACTIVE = 0xA,         // menu item activated by mouse hover or by keyboard,
        MENU_ITEM_CLICK = 0xB,          // menu item click,
                                        //   BEHAVIOR_EVENT_PARAMS structure layout
                                        //   BEHAVIOR_EVENT_PARAMS.cmd - MENU_ITEM_CLICK/MENU_ITEM_ACTIVE
                                        //   BEHAVIOR_EVENT_PARAMS.heTarget - owner(anchor) of the menu
                                        //   BEHAVIOR_EVENT_PARAMS.he - the menu item, presumably <li> element
                                        //   BEHAVIOR_EVENT_PARAMS.reason - BY_MOUSE_CLICK | BY_KEY_CLICK


        CONTEXT_MENU_REQUEST = 0x10,    // "right-click", BEHAVIOR_EVENT_PARAMS::he is current popup menu HELEMENT being processed or NULL.
                                        // application can provide its own HELEMENT here (if it is NULL) or modify current menu element.

        VISIUAL_STATUS_CHANGED = 0x11,  // broadcast notification, sent to all elements of some container being shown or hidden
        DISABLED_STATUS_CHANGED = 0x12, // broadcast notification, sent to all elements of some container that got new value of :disabled state

        POPUP_DISMISSING = 0x13,        // popup is about to be closed

        CONTENT_CHANGED = 0x15,         // content has been changed, is posted to the element that gets content changed,  reason is combination of CONTENT_CHANGE_BITS.
                                        // target == NULL means the window got new document and this event is dispatched only to the window.

        // "grey" event codes  - notfications from behaviors from this SDK
        HYPERLINK_CLICK = 0x80,         // hyperlink click

        //TABLE_HEADER_CLICK,			// click on some cell in table header,
        //								//     target = the cell,
        //								//     reason = index of the cell (column number, 0..n)
        //TABLE_ROW_CLICK,				// click on data row in the table, target is the row
        //								//     target = the row,
        //								//     reason = index of the row (fixed_rows..n)
        //TABLE_ROW_DBL_CLICK,			// mouse dbl click on data row in the table, target is the row
        //								//     target = the row,
        //								//     reason = index of the row (fixed_rows..n)

        ELEMENT_COLLAPSED = 0x90,       // element was collapsed, so far only behavior:tabs is sending these two to the panels
        ELEMENT_EXPANDED = 0x91,        // element was expanded,

        ACTIVATE_CHILD = 0x92,      // activate (select) child,
                                    // used for example by accesskeys behaviors to send activation request, e.g. tab on behavior:tabs.

        FORM_SUBMIT = 0x96,     // behavior:form detected submission event. BEHAVIOR_EVENT_PARAMS::data field contains data to be posted.
                                // BEHAVIOR_EVENT_PARAMS::data is of type T_MAP in this case key/value pairs of data that is about 
                                // to be submitted. You can modify the data or discard submission by returning true from the handler.
        FORM_RESET = 0x97,      // behavior:form detected reset event (from button type=reset). BEHAVIOR_EVENT_PARAMS::data field contains data to be reset.
                                // BEHAVIOR_EVENT_PARAMS::data is of type T_MAP in this case key/value pairs of data that is about 
                                // to be rest. You can modify the data or discard reset by returning true from the handler.

        DOCUMENT_COMPLETE = 0x98,       // document in behavior:frame or root document is complete.

        HISTORY_PUSH = 0x99,            // requests to behavior:history (commands)
        HISTORY_DROP = 0x9A,
        HISTORY_PRIOR = 0x9B,
        HISTORY_NEXT = 0x9C,
        HISTORY_STATE_CHANGED = 0x9D,   // behavior:history notification - history stack has changed

        CLOSE_POPUP = 0x9E,             // close popup request,
        REQUEST_TOOLTIP = 0x9F,         // request tooltip, evt.source <- is the tooltip element.

        ANIMATION = 0xA0,       // animation started (reason=1) or ended(reason=0) on the element.
        TRANSITION = 0xA1,      // transition started (reason=1) or ended(reason=0) on the element.
        SWIPE = 0xB0,      // swipe gesture detected, reason=4,8,2,6 - swipe direction, only from behavior:swipe-touch
        DOCUMENT_CREATED = 0xC0,        // document created, script namespace initialized. target -> the document
        DOCUMENT_CLOSE_REQUEST = 0xC1,  // document is about to be closed, to cancel closing do: evt.data = sciter::value("cancel");
        DOCUMENT_CLOSE = 0xC2,      // last notification before document removal from the DOM
        DOCUMENT_READY = 0xC3,       // document has got DOM structure, styles and behaviors of DOM elements. Script loading run is complete at this moment. 
        DOCUMENT_PARSED = 0xC4,      // document just finished parsing - has got DOM structure. This event is generated before DOCUMENT_READY
        DOCUMENT_CLOSING = 0xC6, // view::notify_close
        CONTAINER_CLOSE_REQUEST = 0xC7, // window of host document is processing DOCUMENT_CLOSE_REQUEST
        CONTAINER_CLOSING = 0xC8,       // window of host document is processing DOCUMENT_CLOSING

        VIDEO_INITIALIZED = 0xD1,       // <video> "ready" notification   
        VIDEO_STARTED = 0xD2,       // <video> playback started notification   
        VIDEO_STOPPED = 0xD3,       // <video> playback stoped/paused notification   
        VIDEO_BIND_RQ = 0xD4,       // <video> request for frame source binding, 
                                    //   If you want to provide your own video frames source for the given target <video> element do the following:
                                    //   1. Handle and consume this VIDEO_BIND_RQ request 
                                    //   2. You will receive second VIDEO_BIND_RQ request/event for the same <video> element
                                    //      but this time with the 'reason' field set to an instance of sciter::video_destination interface.
                                    //   3. add_ref() it and store it for example in worker thread producing video frames.
                                    //   4. call sciter::video_destination::start_streaming(...) providing needed parameters
                                    //      call sciter::video_destination::render_frame(...) as soon as they are available
                                    //      call sciter::video_destination::stop_streaming() to stop the rendering (a.k.a. end of movie reached)
        VIDEO_FRAME_REQUEST = 0xD8,    // animation step, a.k.a. animation frame

        PAGINATION_STARTS = 0xE0,       // behavior:pager starts pagination
        PAGINATION_PAGE = 0xE1,     // behavior:pager paginated page no, reason -> page no
        PAGINATION_ENDS = 0xE2,     // behavior:pager end pagination, reason -> total pages

        CUSTOM = 0xF0,      // event with custom name
        EGL_RENDER = 0x20,

        FIRST_APPLICATION_EVENT_CODE = 0x100
        // all custom event codes shall be greater
        // than this number. All codes below this will be used
        // solely by application - HTMLayout will not interpret it
        // and will do just dispatching.
        // To send event notifications with these codes use
        // HTMLayoutSend/PostEvent API.
    }

}

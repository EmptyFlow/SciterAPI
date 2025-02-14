namespace SciterLibraryAPI {
    public enum SciterCallbackNotificationCode : uint {
        /*
             /**Notifies that Sciter is about to download a referred resource.
         *
         * \param lParam #LPSCN_LOAD_DATA.
         * \return #SC_LOAD_DATA_RETURN_CODES
         *
         * This notification gives application a chance to override built-in loader and
         * implement loading of resources in its own way (for example images can be loaded from
         * database or other resource). To do this set #SCN_LOAD_DATA::outData and
         * #SCN_LOAD_DATA::outDataSize members of SCN_LOAD_DATA. Sciter does not
         * store pointer to this data. You can call #SciterDataReady() function instead
         * of filling these fields. This allows you to free your outData buffer
         * immediately.
        **/
        SC_LOAD_DATA = 0x01,

        /**This notification indicates that external data (for example image) download process
         * completed.
         *
         * \param lParam #LPSCN_DATA_LOADED
         *
         * This notifiaction is sent for each external resource used by document when
         * this resource has been completely downloaded. Sciter will send this
         * notification asynchronously.
         **/
        SC_DATA_LOADED = 0x02,

        /**This notification is sent on parsing the document and while processing
         * elements having non empty style.behavior attribute value.
         *
         * \param lParam #LPSCN_ATTACH_BEHAVIOR
         *
         * Application has to provide implementation of #sciter::behavior interface.
         * Set #SCN_ATTACH_BEHAVIOR::impl to address of this implementation.
         **/
        SC_ATTACH_BEHAVIOR = 0x04,

        /**This notification is sent when instance of the engine is destroyed.
         * It is always final notification.
         *
         * \param lParam #LPSCN_ENGINE_DESTROYED
         *
         **/
        SC_ENGINE_DESTROYED = 0x05,

        /**Posted notification.
         
         * \param lParam #LPSCN_POSTED_NOTIFICATION
         *
         **/
        SC_POSTED_NOTIFICATION = 0x06,


        /**This notification is sent when the engine encounters critical rendering error: e.g. DirectX gfx driver error.
           Most probably bad gfx drivers.
         
         * \param lParam #LPSCN_GRAPHICS_CRITICAL_FAILURE
         *
         **/
        SC_GRAPHICS_CRITICAL_FAILURE = 0x07,


        /**This notification is sent when the engine needs keyboard to be present on screen
           E.g. when <input|text> gets focus

         * \param lParam #LPSCN_KEYBOARD_REQUEST
         *
         **/
        SC_KEYBOARD_REQUEST = 0x08,

        /**This notification is sent when the engine needs some area to be redrawn
         
         * \param lParam #LPSCN_INVLIDATE_RECT
         *
         **/
        SC_INVALIDATE_RECT = 0x09,

        /**This notification is sent when the engine needs some area to be redrawn

         * \param lParam #LPSCN_SET_CURSOR
         *
         **/
        SC_SET_CURSOR = 0x0A

    }

}

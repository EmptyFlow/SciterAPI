using System.Runtime.InteropServices;

namespace SciterLibraryAPI {

    public class SciterAPIGlobalCallbacks {

        private SciterApiStruct m_sciterApiStruct;

        private SciterAPIHost m_host;

        private SciterHostCallback m_sciterHostCallback;

        private Dictionary<string, Func<string, byte[]>> m_protocolHandlers = new Dictionary<string, Func<string, byte[]>> ();

        private Action<string, uint, uint> m_loadedDataAction;

        private Action m_engineDestroyedAction;

        private Action m_graphicalFailureAction;

        private Action<IntPtr, IntPtr, IntPtr> m_notificationPostedAction;

        private Func<string, IntPtr, ElementEventProc?> m_attachBehaviourAction;

        protected Dictionary<string, Func<IntPtr, SciterEventHandler>> m_attachBehaviourFactories = new Dictionary<string, Func<IntPtr, SciterEventHandler>> ();

        public SciterAPIGlobalCallbacks ( SciterAPIHost host ) {
            m_loadedDataAction = EmptyLoadedDataAction;
            m_engineDestroyedAction = EmptyAction;
            m_graphicalFailureAction = EmptyAction;
            m_notificationPostedAction = EmptyNotificationPostedAction;
            m_attachBehaviourAction = DefaultAttachedBahaviourAction;

            m_sciterApiStruct = host.OriginalApi;
            m_host = host;
            m_sciterHostCallback = SciterHostCallback;

            m_sciterApiStruct.SciterSetCallback ( m_host.MainWindow, m_sciterHostCallback, 1 );
        }

        public Action<string, uint, uint> LoadedDataAction {
            get => m_loadedDataAction;
            set => m_loadedDataAction = value;
        }

        public Action EngineDestroyedAction {
            get => m_engineDestroyedAction;
            set => m_engineDestroyedAction = value;
        }

        public Action GraphicalFailureAction {
            get => m_graphicalFailureAction;
            set => m_graphicalFailureAction = value;
        }

        public Action<IntPtr, IntPtr, IntPtr> NotificationPostedAction {
            get => m_notificationPostedAction;
            set => m_notificationPostedAction = value;
        }

        public Func<string, IntPtr, ElementEventProc?> AttachBehaviourAction {
            get => m_attachBehaviourAction;
            set => m_attachBehaviourAction = value;
        }

        public void AddProtocolHandler ( string protocol, Func<string, byte[]> handlers ) {
            if ( string.IsNullOrEmpty ( protocol ) ) throw new ArgumentNullException ( "protocol" );
            if ( handlers == null ) throw new ArgumentNullException ( "handlers" );
            if ( m_protocolHandlers.ContainsKey ( protocol ) ) throw new ArgumentException ( $"Protocol {protocol} already added!" );

            m_protocolHandlers.Add ( protocol, handlers );
        }

        private uint SciterHostCallback ( IntPtr pns, IntPtr callbackParam ) {
            var commonStructure = Marshal.PtrToStructure<SciterCallbackNotification> ( pns );
            switch ( commonStructure.code ) {
                case SciterCallbackNotificationCode.SC_LOAD_DATA:
                    var loadDataStructure = Marshal.PtrToStructure<SciterCallbackNotificationLoadData> ( pns );
                    return OnLoadData ( loadDataStructure );
                case SciterCallbackNotificationCode.SC_DATA_LOADED:
                    var loadedDataStructure = Marshal.PtrToStructure<SciterCallbackNotificationLoadedData> ( pns );
                    m_loadedDataAction ( loadedDataStructure.uri, loadedDataStructure.status, loadedDataStructure.dataSize );
                    return 0;
                case SciterCallbackNotificationCode.SC_ATTACH_BEHAVIOR:
                    var behaviourStructure = Marshal.PtrToStructure<SciterCallbackNotificationAttachBehaviour> ( pns );
                    var elementProc = m_attachBehaviourAction ( behaviourStructure.behaviorName, behaviourStructure.element );
                    if ( elementProc != null ) {
                        behaviourStructure.elementProc = Marshal.GetFunctionPointerForDelegate ( elementProc );
                        behaviourStructure.elementTag = 1;
                        return 1;
                    }
                    return 0;
                case SciterCallbackNotificationCode.SC_ENGINE_DESTROYED:
                    m_engineDestroyedAction ();
                    return 0;
                case SciterCallbackNotificationCode.SC_POSTED_NOTIFICATION:
                    var notificationStructure = Marshal.PtrToStructure<SciterCallbackNotificationPosted> ( pns );
                    m_notificationPostedAction ( notificationStructure.wparam, notificationStructure.lparam, notificationStructure.lparam );
                    return 0;
                case SciterCallbackNotificationCode.SC_GRAPHICS_CRITICAL_FAILURE:
                    m_graphicalFailureAction ();
                    return 0;
                /*case SciterCallbackNotificationCode.SC_KEYBOARD_REQUEST:
                    break;
                case SciterCallbackNotificationCode.SC_INVALIDATE_RECT:
                    break;
                case SciterCallbackNotificationCode.SC_SET_CURSOR:
                    break;*/
                default: return 0;
            }
        }

        private uint OnLoadData ( SciterCallbackNotificationLoadData loadDataStruct ) {
            if ( string.IsNullOrEmpty ( loadDataStruct.uri ) ) return (uint) LoadDataReturnCode.LOAD_OK; // in this case we don't need override load something

            foreach ( var m_protocolHandler in m_protocolHandlers ) {
                if ( !loadDataStruct.uri.StartsWith ( m_protocolHandler.Key ) ) continue;

                byte[] array = m_protocolHandler.Value ( loadDataStruct.uri );
                m_sciterApiStruct.SciterDataReady ( m_host.MainWindow, loadDataStruct.uri, array, (uint) array.Length );
                return (uint) LoadDataReturnCode.LOAD_DISCARD; // in this case we override standart loading functions
            }

            return (uint) LoadDataReturnCode.LOAD_OK; // in this case we don't need override load something
        }

        private void EmptyLoadedDataAction ( string uri, uint status, uint dataSize ) {
            Console.WriteLine ( $"{uri} - {status}({dataSize})" );
        }

        private void EmptyAction () {
        }

        private void EmptyNotificationPostedAction ( IntPtr wparam, IntPtr lparam, IntPtr lreturn ) {
        }

        private ElementEventProc? DefaultAttachedBahaviourAction ( string behaviourName, IntPtr element ) {
            if ( m_attachBehaviourFactories.ContainsKey ( behaviourName ) ) {
                try {
                    var handler = m_attachBehaviourFactories[behaviourName] ( element );
                    var handlerDelegate = m_host.AddElementEventHandler ( handler );
                    return handlerDelegate;
                } catch ( Exception e ) {
                    Console.WriteLine ( $"Error while create behaviour handler with name {behaviourName}: " + e.Message );
                    return null;
                }
            }
            return null;
        }

        /*
         virtual LRESULT on_load_data(LPSCN_LOAD_DATA pnmld)
      {
        LPCBYTE pb = 0; UINT cb = 0;
        aux::wchars wu = aux::chars_of(pnmld->uri);

        if(wu.like(WSTR("res:*")))
        {
          // then by calling possibly overloaded load_resource_data method
          if (static_cast<BASE*>(this)->load_resource_data(wu.start + 4, pb, cb))
            ::SciterDataReady(pnmld->hwnd, pnmld->uri, pb, cb);
          else {
#ifdef SDEBUG
#ifdef CPP11
            auto console = debug_output::instance();
            if (console)
              console->printf("LOAD FAILURE:%S\n", pnmld->uri);
#endif
#endif
            return LOAD_DISCARD;
          }
        } else if(wu.like(WSTR("this://app/*"))) {
          // try to get them from archive first
          aux::bytes adata = archive::instance().get(wu.start+11);
          if (adata.length)
            ::SciterDataReady(pnmld->hwnd, pnmld->uri, adata.start, UINT(adata.length));
          else {
#ifdef SDEBUG
#ifdef CPP11
            auto console = debug_output::instance();
            if (console)
              console->printf("LOAD FAILURE:%S\n", pnmld->uri);
#endif
#endif
            return LOAD_DISCARD;
          }
        }
        return LOAD_OK;
      }
         */

        /*

  LRESULT handle_notification(LPSCITER_CALLBACK_NOTIFICATION pnm)
  {
    // Crack and call appropriate method

    // here are all notifiactions
    switch(pnm->code)
    {
      case SC_LOAD_DATA:          return static_cast<BASE*>(this)->on_load_data((LPSCN_LOAD_DATA) pnm);
      case SC_DATA_LOADED:        return static_cast<BASE*>(this)->on_data_loaded((LPSCN_DATA_LOADED)pnm);
      //case SC_DOCUMENT_COMPLETE: return on_document_complete();
      case SC_ATTACH_BEHAVIOR:    return static_cast<BASE*>(this)->on_attach_behavior((LPSCN_ATTACH_BEHAVIOR)pnm );
      case SC_ENGINE_DESTROYED:   return static_cast<BASE*>(this)->on_engine_destroyed();
      case SC_POSTED_NOTIFICATION: return static_cast<BASE*>(this)->on_posted_notification((LPSCN_POSTED_NOTIFICATION)pnm);
      case SC_GRAPHICS_CRITICAL_FAILURE: static_cast<BASE*>(this)->on_graphics_critical_failure(); return 0;
    }
    return 0;
  }

         */

    }

}

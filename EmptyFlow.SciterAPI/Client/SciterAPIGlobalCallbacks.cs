﻿using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    public class SciterAPIGlobalCallbacks {

        private SciterApiStruct m_sciterApiStruct;

        private SciterAPIHost m_host;

        private SciterHostCallback m_sciterHostCallback;

        private Dictionary<string, Func<string, byte[]>> m_protocolHandlers = new Dictionary<string, Func<string, byte[]>> ();

        private Action<string, uint, uint> m_loadedDataAction;

        private Action m_engineDestroyedAction;

        private Action m_graphicalFailureAction;

        private Action<IntPtr, IntPtr, IntPtr> m_notificationPostedAction;

        private Func<string, IntPtr, SciterEventHandler?> m_attachBehaviourAction;

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

        public Func<string, IntPtr, SciterEventHandler?> AttachBehaviourAction {
            get => m_attachBehaviourAction;
            set => m_attachBehaviourAction = value;
        }

        public void AddProtocolHandler ( string protocol, Func<string, byte[]> handlers ) {
            if ( string.IsNullOrEmpty ( protocol ) ) throw new ArgumentNullException ( "protocol" );
            if ( handlers == null ) throw new ArgumentNullException ( "handlers" );
            if ( m_protocolHandlers.ContainsKey ( protocol ) ) throw new ArgumentException ( $"Protocol {protocol} already added!" );

            m_protocolHandlers.Add ( protocol, handlers );
        }

        public void AddAttachBehaviourFactory ( string name, Func<IntPtr, SciterEventHandler> handler ) {
            if ( m_attachBehaviourFactories.ContainsKey ( name ) ) throw new ArgumentException ( $"Factory with name {name} already attached!" );
            if ( handler == null ) throw new ArgumentException ( $"Parameter handler contains null!" );

            m_attachBehaviourFactories.Add ( name, handler );
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
                    var behaviourName = Marshal.PtrToStringUTF8 ( behaviourStructure.behaviorName ) ?? "";
                    var eventHandler = m_attachBehaviourAction ( behaviourName, behaviourStructure.element );
                    if ( eventHandler != null ) {
                        behaviourStructure.elementProc = Marshal.GetFunctionPointerForDelegate ( eventHandler.InnerDelegate );
                        behaviourStructure.elementTag = 1; // Is need customization???
                        Marshal.StructureToPtr ( behaviourStructure, pns, true );
                        m_host.AddEventHandler ( eventHandler, fromFactory: true );
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

        private SciterEventHandler? DefaultAttachedBahaviourAction ( string behaviourName, IntPtr element ) {
            if ( m_attachBehaviourFactories.ContainsKey ( behaviourName ) ) {
                try {
                    var handler = m_attachBehaviourFactories[behaviourName] ( element );
                    return handler;
                } catch ( Exception e ) {
                    Console.WriteLine ( $"Error while create behaviour handler with name {behaviourName}: " + e.Message );
                    return null;
                }
            }
            return null;
        }


    }

}

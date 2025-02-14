using System.Runtime.InteropServices;

namespace SciterLibraryAPI {

// Common API

public delegate uint SciterVersion ( uint n );
public delegate IntPtr SciterClassName ();
public delegate bool SciterDataReady ( IntPtr hwnd, [MarshalAs ( UnmanagedType.LPWStr )] string uri, byte[] data, uint dataLength );
public delegate bool SciterDataReadyAsync ( IntPtr hwnd, string uri, byte[] data, uint dataLength, IntPtr requestId );
public delegate IntPtr SciterProc ( IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam );
public delegate IntPtr SciterProcNd ( IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, ref bool pbHandled );
public delegate bool SciterLoadFile ( IntPtr hwnd, [MarshalAs ( UnmanagedType.LPWStr )] string filename );
public delegate bool SciterLoadHtml ( IntPtr hwnd, byte[] html, uint htmlSize, [MarshalAs ( UnmanagedType.LPWStr )] string baseUrl );
public delegate void SciterSetCallback ( IntPtr hwnd, MulticastDelegate cb /*SciterHostCallback*/, IntPtr param );
public delegate bool SciterSetMasterCss ( byte[] utf8, uint numBytes );
public delegate bool SciterAppendMasterCss ( byte[] utf8, uint numBytes );
public delegate bool SciterSetCss ( IntPtr hwnd, byte[] utf8, uint numBytes, [MarshalAs ( UnmanagedType.LPWStr )] string baseUrl, [MarshalAs ( UnmanagedType.LPWStr )] string mediaType );
public delegate bool SciterSetMediaType ( IntPtr hwnd, [MarshalAs ( UnmanagedType.LPWStr )] string mediaType );
public delegate bool SciterSetMediaVars ( IntPtr hwnd, ref SciterScriptValue mediaVars );
public delegate uint SciterGetMinWidth ( IntPtr hwnd );
public delegate uint SciterGetMinHeight ( IntPtr hwnd, uint width );
public delegate bool SciterCall ( IntPtr hwnd, [MarshalAs ( UnmanagedType.LPStr )] string functionName, uint argc, SciterScriptValue[] argv, out SciterScriptValue retval );
public delegate bool SciterEval ( IntPtr hwnd, [MarshalAs ( UnmanagedType.LPWStr )] string script, uint scriptLength, out SciterScriptValue pretval );
public delegate bool SciterUpdateWindow ( IntPtr hwnd );
public delegate bool SciterTranslateMessage ( IntPtr lpMsg );
public delegate bool SciterSetOption ( IntPtr hwnd, RtOptions option, IntPtr value );
public delegate void SciterGetPpi ( IntPtr hwnd, ref uint px, ref uint py );
public delegate bool SciterGetViewExpando ( IntPtr hwnd, out SciterScriptValue pval );
public delegate bool SciterRenderD2D ( IntPtr hwnd, IntPtr prt );
public delegate bool SciterD2DFactory ( IntPtr ppf );
public delegate bool SciterDwFactory ( IntPtr ppf );
public delegate bool SciterGraphicsCaps ( ref uint pcaps );
public delegate bool SciterSetHomeUrl ( IntPtr hwnd, string baseUrl );
public delegate IntPtr SciterCreateNsView ( ref SciterRectangle frame );
public delegate IntPtr SciterCreateWidget ( ref SciterRectangle frame );
public delegate IntPtr SciterCreateWindow ( WindowsFlags creationFlags, ref SciterRectangle frame, IntPtr delegt, IntPtr delegateParam, IntPtr parent );
public delegate void SciterSetupDebugOutput ( IntPtr hwndOrNull, IntPtr param, DebugOutputProc pfOutput );

public delegate IntPtr DebugOutputProc ( IntPtr param, uint subsystem, uint severity, IntPtr text_ptr, uint text_length );
public delegate uint SciterHostCallback ( IntPtr pns, IntPtr callbackParam );

// DOM API

public delegate DomResult SciterUseElement ( IntPtr he );
public delegate DomResult SciterUnuseElement ( IntPtr he );
public delegate DomResult SciterGetRootElement ( IntPtr hwnd, out IntPtr phe );
public delegate DomResult SciterGetFocusElement ( IntPtr hwnd, out IntPtr phe );
public delegate DomResult SciterFindElement ( IntPtr hwnd, SciterPoint pt, out IntPtr phe );
public delegate DomResult SciterGetChildrenCount ( IntPtr he, out uint count );
public delegate DomResult SciterGetNthChild ( IntPtr he, uint n, out IntPtr phe );
public delegate DomResult SciterGetParentElement ( IntPtr he, out IntPtr pParentHe );
public delegate DomResult SciterGetElementHtmlCb ( IntPtr he, bool outer, lpcbyteReceiver rcv, IntPtr rcvParam );
public delegate DomResult SciterGetElementTextCb ( IntPtr he, lpcwstReceiver rcv, IntPtr rcvParam );
public delegate DomResult SciterSetElementText ( IntPtr he, [MarshalAs ( UnmanagedType.LPWStr )] string utf16, uint length );
public delegate DomResult SciterGetAttributeCount ( IntPtr he, out uint pCount );
public delegate DomResult SciterGetNthAttributeNameCb ( IntPtr he, uint n, lpcstrReceiver rcv, IntPtr rcvParam );
public delegate DomResult SciterGetNthAttributeValueCb ( IntPtr he, uint n, lpcwstReceiver rcv, IntPtr rcvParam );
public delegate DomResult SciterGetAttributeByNameCb ( IntPtr he, [MarshalAs ( UnmanagedType.LPStr )] string name, lpcwstReceiver rcv, IntPtr rcvParam );
public delegate DomResult SciterSetAttributeByName ( IntPtr he, [MarshalAs ( UnmanagedType.LPStr )] string name, [MarshalAs ( UnmanagedType.LPWStr )] string value );
public delegate DomResult SciterClearAttributes ( IntPtr he );
public delegate DomResult SciterGetElementIndex ( IntPtr he, out uint pIndex );
public delegate DomResult SciterGetElementType ( IntPtr he, out IntPtr pType );
public delegate DomResult SciterGetElementTypeCb ( IntPtr he, lpcstrReceiver rcv, IntPtr rcvParam );
public delegate DomResult SciterGetStyleAttributeCb ( IntPtr he, [MarshalAs ( UnmanagedType.LPStr )] string name, lpcwstReceiver rcv, IntPtr rcvParam );
public delegate DomResult SciterSetStyleAttribute ( IntPtr he, [MarshalAs ( UnmanagedType.LPStr )] string name, [MarshalAs ( UnmanagedType.LPWStr )] string value );
public delegate DomResult SciterGetElementLocation ( IntPtr he, out SciterRectangle pLocation, ElementAreas areas );
public delegate DomResult SciterScrollToView ( IntPtr he, uint sciterScrollFlags );
public delegate DomResult SciterUpdateElement ( IntPtr he, bool andForceRender );
public delegate DomResult SciterRefreshElementArea ( IntPtr he, SciterRectangle rc );
public delegate DomResult SciterSetCapture ( IntPtr he );
public delegate DomResult SciterReleaseCapture ( IntPtr he );
public delegate DomResult SciterGetElementHwnd ( IntPtr he, out IntPtr pHwnd, bool rootWindow );
public delegate DomResult SciterCombineUrl ( IntPtr he, IntPtr szUrlBuffer, uint urlBufferSize );
public delegate DomResult SciterSelectElements ( IntPtr he, [MarshalAs ( UnmanagedType.LPStr )] string cssSelectors, SciterElementCallback callback, IntPtr param );
public delegate DomResult SciterSelectElementsW ( IntPtr he, [MarshalAs ( UnmanagedType.LPWStr )] string cssSelectors, SciterElementCallback callback, IntPtr param );
public delegate DomResult SciterSelectParent ( IntPtr he, [MarshalAs ( UnmanagedType.LPStr )] string selector, uint depth, out IntPtr heFound );
public delegate DomResult SciterSelectParentW ( IntPtr he, [MarshalAs ( UnmanagedType.LPWStr )] string selector, uint depth, out IntPtr heFound );
public delegate DomResult SciterSetElementHtml ( IntPtr he, byte[] html, uint htmlLength, SetElementHtml where );
public delegate DomResult SciterGetElementUid ( IntPtr he, out uint puid );
public delegate DomResult SciterGetElementByUid ( IntPtr hwnd, uint uid, out IntPtr phe );
public delegate DomResult SciterShowPopup ( IntPtr he, IntPtr heAnchor, uint placement );
public delegate DomResult SciterShowPopupAt ( IntPtr he, SciterPoint pos, uint placement );
public delegate DomResult SciterHidePopup ( IntPtr he );
public delegate DomResult SciterGetElementState ( IntPtr he, out uint pstateBits );
public delegate DomResult SciterSetElementState ( IntPtr he, uint stateBitsToSet, uint stateBitsToClear, bool updateView );
public delegate DomResult SciterCreateElement ( [MarshalAs ( UnmanagedType.LPStr )] string tagname, [MarshalAs ( UnmanagedType.LPWStr )] string textOrNull, out IntPtr phe );
public delegate DomResult SciterCloneElement ( IntPtr he, out IntPtr phe );
public delegate DomResult SciterInsertElement ( IntPtr he, IntPtr hparent, uint index );
public delegate DomResult SciterDetachElement ( IntPtr he );
public delegate DomResult SciterDeleteElement ( IntPtr he );
public delegate DomResult SciterSetTimer ( IntPtr he, uint milliseconds, IntPtr timerId );
public delegate DomResult SciterDetachEventHandler ( IntPtr he, MulticastDelegate pep, IntPtr tag );
public delegate DomResult SciterAttachEventHandler ( IntPtr he, MulticastDelegate pep, IntPtr tag );
public delegate DomResult SciterWindowAttachEventHandler ( IntPtr hwndLayout, MulticastDelegate pep, IntPtr tag, uint subscription );
public delegate DomResult SciterWindowDetachEventHandler ( IntPtr hwndLayout, MulticastDelegate pep, IntPtr tag );
public delegate DomResult SciterSendEvent ( IntPtr he, uint appEventCode, IntPtr heSource, IntPtr reason, out bool handled );
public delegate DomResult SciterPostEvent ( IntPtr he, uint appEventCode, IntPtr heSource, IntPtr reason );
public delegate DomResult SciterCallBehaviorMethod ( IntPtr he, ref MethodParams param );
public delegate DomResult SciterRequestElementData ( IntPtr he, [MarshalAs ( UnmanagedType.LPWStr )] string url, uint dataType, IntPtr initiator );
public delegate DomResult SciterHttpRequest ( IntPtr he, [MarshalAs ( UnmanagedType.LPWStr )] string url, uint dataType, uint requestType, ref RequestParam requestParams, uint nParams );
public delegate DomResult SciterGetScrollInfo ( IntPtr he, out SciterPoint scrollPos, out SciterRectangle viewRect, out SciterSize contentSize );
public delegate DomResult SciterSetScrollPos ( IntPtr he, SciterPoint scrollPos, bool smooth );
public delegate DomResult SciterGetElementIntrinsicWidths ( IntPtr he, out int pMinWidth, out int pMaxWidth );
public delegate DomResult SciterGetElementIntrinsicHeight ( IntPtr he, int forWidth, out int pHeight );
public delegate DomResult SciterIsElementVisible ( IntPtr he, out bool pVisible );
public delegate DomResult SciterIsElementEnabled ( IntPtr he, out bool pEnabled );
public delegate DomResult SciterSortElements ( IntPtr he, uint firstIndex, uint lastIndex, ElementComparator cmpFunc, IntPtr cmpFuncParam );
public delegate DomResult SciterSwapElements ( IntPtr he, IntPtr he2 );
public delegate DomResult SciterTraverseUiEvent ( IntPtr he, IntPtr eventCtlStruct, out bool bOutProcessed );
public delegate DomResult SciterCallScriptingMethod ( IntPtr he, [MarshalAs ( UnmanagedType.LPStr )] string name, SciterValue[] argv, uint argc, out SciterValue retval );
public delegate DomResult SciterCallScriptingFunction ( IntPtr he, [MarshalAs ( UnmanagedType.LPStr )] string name, SciterValue[] argv, uint argc, out SciterValue retval );
public delegate DomResult SciterEvalElementScript ( IntPtr he, [MarshalAs ( UnmanagedType.LPWStr )] string script, uint scriptLength, out SciterValue retval );
public delegate DomResult SciterAttachHwndToElement ( IntPtr he, IntPtr hwnd );
public delegate DomResult SciterControlGetType ( IntPtr he, out uint pType );
public delegate DomResult SciterGetValue ( IntPtr he, out SciterValue pval );
public delegate DomResult SciterSetValue ( IntPtr he, ref SciterValue pval );
public delegate DomResult SciterGetExpando ( IntPtr he, out SciterValue pval, bool forceCreation );
public delegate DomResult SciterGetObject ( IntPtr he, out IntPtr pval, bool forceCreation );
public delegate DomResult SciterGetElementNamespace ( IntPtr he, out IntPtr pval );
public delegate DomResult SciterGetHighlightedElement ( IntPtr hwnd, out IntPtr phe );
public delegate DomResult SciterSetHighlightedElement ( IntPtr hwnd, IntPtr he );

//DOM Node API

public delegate DomResult SciterNodeAddRef ( IntPtr hn );
public delegate DomResult SciterNodeRelease ( IntPtr hn );
public delegate DomResult SciterNodeCastFromElement ( IntPtr he, out IntPtr phn );
public delegate DomResult SciterNodeCastToElement ( IntPtr hn, out IntPtr he );
public delegate DomResult SciterNodeFirstChild ( IntPtr hn, out IntPtr phn );
public delegate DomResult SciterNodeLastChild ( IntPtr hn, out IntPtr phn );
public delegate DomResult SciterNodeNextSibling ( IntPtr hn, out IntPtr phn );
public delegate DomResult SciterNodePrevSibling ( IntPtr hn, out IntPtr phn );
public delegate DomResult SciterNodeParent ( IntPtr hn, out IntPtr pheParent );
public delegate DomResult SciterNodeNthChild ( IntPtr hn, uint n, out IntPtr phn );
public delegate DomResult SciterNodeChildrenCount ( IntPtr hn, out uint pn );
public delegate DomResult SciterNodeType ( IntPtr hn, out NodeType pn );
public delegate DomResult SciterNodeGetText ( IntPtr hn, lpcwstReceiver rcv, IntPtr rcvParam );
public delegate DomResult SciterNodeSetText ( IntPtr hn, [MarshalAs ( UnmanagedType.LPWStr )] string text, uint textLength );
public delegate DomResult SciterNodeInsert ( IntPtr hn, uint where, IntPtr what );
public delegate DomResult SciterNodeRemove ( IntPtr hn, bool finalize );
public delegate DomResult SciterCreateTextNode ( [MarshalAs ( UnmanagedType.LPWStr )] string text, uint textLength, out IntPtr phnode );
public delegate DomResult SciterCreateCommentNode ( [MarshalAs ( UnmanagedType.LPWStr )] string text, uint textLength, out IntPtr phnode );

// Value API

public delegate ValueResult ValueInit ( out SciterValue pval );
public delegate ValueResult ValueClear ( out SciterValue pval );
public delegate ValueResult ValueCompare ( ref SciterValue pval, ref IntPtr pval2 );
public delegate ValueResult ValueCopy ( out SciterValue pdst, ref SciterValue psrc );
public delegate ValueResult ValueIsolate ( ref SciterValue pdst );
public delegate ValueResult ValueTypeDelegate ( ref SciterValue pval, out uint pType, out uint pUnits );
public delegate ValueResult ValueStringData ( ref SciterValue pval, out IntPtr pChars, out uint pNumChars );
public delegate ValueResult ValueStringDataSet ( ref SciterValue pval, [MarshalAs ( UnmanagedType.LPWStr )] string chars, uint numChars, uint units );
public delegate ValueResult ValueIntData ( ref SciterValue pval, out int pData );
public delegate ValueResult ValueIntDataSet ( ref SciterValue pval, int data, uint type, uint units );
public delegate ValueResult ValueInt64Data ( ref SciterValue pval, out long pData );
public delegate ValueResult ValueInt64DataSet ( ref SciterValue pval, long data, uint type, uint units );
public delegate ValueResult ValueFloatData ( ref SciterValue pval, out double pData );
public delegate ValueResult ValueFloatDataSet ( ref SciterValue pval, double data, uint type, uint units );
public delegate ValueResult ValueBinaryData ( ref SciterValue pval, out IntPtr pBytes, out uint pnBytes );
public delegate ValueResult ValueBinaryDataSet ( ref SciterValue pval, [MarshalAs ( UnmanagedType.LPArray )] byte[] pBytes, uint nBytes, uint type, uint units );
public delegate ValueResult ValueElementsCount ( ref SciterValue pval, out int pn );
public delegate ValueResult ValueNthElementValue ( ref SciterValue pval, int n, out SciterValue pretval );
public delegate ValueResult ValueNthElementValueSet ( ref SciterValue pval, int n, ref SciterValue pvalToSet );
public delegate ValueResult ValueNthElementKey ( ref SciterValue pval, int n, out SciterValue pretval );
public delegate ValueResult ValueEnumElements ( ref SciterValue pval, KeyValueCallback penum, IntPtr param );
public delegate ValueResult ValueSetValueToKey ( ref SciterValue pval, ref SciterValue pkey, ref SciterValue pvalToSet );
public delegate ValueResult ValueGetValueOfKey ( ref SciterValue pval, ref SciterValue pkey, out SciterValue pretval );
public delegate ValueResult ValueToString ( ref SciterValue pval, ValueStringCvtType how );
public delegate ValueResult ValueFromString ( ref SciterValue pval, [MarshalAs ( UnmanagedType.LPWStr )] string str, uint strLength, uint how );
public delegate ValueResult ValueInvoke ( ref SciterValue pval, ref SciterValue pthis, uint argc, SciterValue[] argv, out SciterValue pretval, [MarshalAs ( UnmanagedType.LPWStr )] string url );
public delegate ValueResult ValueNativeFunctorSet ( ref SciterValue pval, NativeFunctorInvoke pinvoke, NativeFunctorRelease prelease, IntPtr tag );
public delegate ValueResult ValueIsNativeFunctor ( ref SciterValue pval );

// Archive API

public delegate IntPtr SciterOpenArchive ( IntPtr archiveData, uint archiveDataLength ); // archiveData must point to a pinned byte[] array!
public delegate bool SciterGetArchiveItem ( IntPtr harc, [MarshalAs ( UnmanagedType.LPWStr )] string path, out IntPtr pdata, out uint pdataLength );
public delegate bool SciterCloseArchive ( IntPtr harc );

// Graphics and other routine API

public delegate DomResult SciterFireEvent ( ref BehaviourEventsParameters evt, bool post, out bool handled );
public delegate IntPtr SciterGetCallbackParam ( IntPtr hwnd );
public delegate IntPtr SciterPostCallback ( IntPtr hwnd, IntPtr wparam, IntPtr lparam, uint timeoutms );
public delegate IntPtr GetSciterGraphicsApi ();
public delegate IntPtr GetSciterRequestApi ();
public delegate bool SciterCreateOnDirectXWindow ( IntPtr hwnd, IntPtr pSwapChain );
public delegate bool SciterRenderOnDirectXWindow ( IntPtr hwnd, IntPtr elementToRenderOrNull, bool frontLayer );
public delegate bool SciterRenderOnDirectXTexture ( IntPtr hwnd, IntPtr elementToRenderOrNull, IntPtr surface );
public delegate bool SciterProcX ( IntPtr hwnd, IntPtr pMsg );

public delegate ulong SciterAtomValue ( IntPtr name );
public delegate bool SciterAtomNameCB ( ulong atomv, lpcstrReceiver rcv, IntPtr rcv_param );
public delegate bool SciterSetGlobalAsset ( SomAsset pass );
public delegate DomResult SciterGetElementAsset ( IntPtr el, ulong nameAtom, IntPtr ppass /* som_asset_t** */ );

public delegate uint SciterSetVariable ( IntPtr hwndOrNull, [MarshalAs ( UnmanagedType.LPStr )] string name, IntPtr pvalToSet /* const VALUE* */ );
public delegate uint SciterGetVariable ( IntPtr hwndOrNull, [MarshalAs ( UnmanagedType.LPStr )] string name, IntPtr pvalToGet /* VALUE* */ );
public delegate uint SciterElementUnwrap ( IntPtr pval /*const VALUE* */, IntPtr ppElement /* HELEMENT* */ );
public delegate uint SciterElementWrap ( IntPtr pval /* VALUE* */, IntPtr pElement /* HELEMENT */ );

public delegate uint SciterNodeUnwrap ( IntPtr pval /* const VALUE* */, IntPtr ppNode /* HNODE* */ );
public delegate uint SciterNodeWrap ( IntPtr pval /* VALUE* */, IntPtr pNode /* HNODE */ );

public delegate bool SciterReleaseGlobalAsset ( SomAsset pass );

public delegate int SciterExec ( ApplicationCommand appCmd, IntPtr p1, IntPtr p2 );
public delegate int SciterWindowExec ( IntPtr hwnd, WindowCommand windowCmd, int p1, IntPtr p2 );

public delegate IntPtr /* typedef void(* proc_ptr_t)(void) */ SciterEGLGetProcAddress ( IntPtr procName /* char const* */ );

public delegate DomResult SciterEGLSendEvent ( IntPtr he, uint eventCode, IntPtr reason );
public delegate DomResult SciterRequestAnimationFrameEvent ( IntPtr he, uint eventCode, IntPtr reason );

// Delegates participiating in API

public delegate void lpcbyteReceiver ( IntPtr bytes, uint num_bytes, IntPtr param );
public delegate void lpcwstReceiver ( IntPtr str, uint str_length, IntPtr param );
public delegate void lpcstrReceiver ( IntPtr str, uint str_length, IntPtr param );
public delegate bool SciterElementCallback ( IntPtr he, IntPtr param );
public delegate bool ElementComparator ( IntPtr he1, IntPtr he2, IntPtr param );
public delegate bool ElementEventProc ( IntPtr tag, IntPtr he, uint evtg, IntPtr prms );
public delegate bool KeyValueCallback ( IntPtr param, ref SciterValue pkey, ref SciterValue pval ); // Original name KEY_VALUE_CALLBACK
public delegate bool NativeFunctorInvoke ( IntPtr tag, uint argc, IntPtr argv, out SciterValue retval ); // Original name NATIVE_FUNCTOR_INVOKE
public delegate bool NativeFunctorRelease ( IntPtr tag ); // Original name NATIVE_FUNCTOR_RELEASE
public delegate long AssetAddOrReleasesDelegate ( IntPtr thing /* SomAsset */ );
public delegate long AssetGetInterface ( IntPtr thing /* SomAsset */, IntPtr name /* const char* */, IntPtr @out );
public delegate SomPassport AssetGetPassport ( IntPtr thing /* SomAsset */ );
public delegate bool SomPropertyGetterOrSetter ( IntPtr thing /* SomAsset */, SciterValue p_value );
public delegate bool SomItemGetterOrSetter ( IntPtr thing /* SomAsset */, SciterValue p_key, SciterValue p_value );
public delegate bool SomMethod ( IntPtr thing /* SomAsset */, uint argc, SciterValue argv, SciterValue p_result ); // original name som_method_t
public delegate bool SomAnyPropertyGetterOrSetter ( IntPtr thing /* SomAsset */, ulong propSymbol, SciterValue p_value ); // original name som_any_prop_getter_t, som_any_prop_setter_t
public delegate bool SomNameResolver ( IntPtr thing /* SomAsset */, ulong propSymbol, uint pIndex, bool pIsMethod ); // original name som_name_resolver_t


[StructLayout ( LayoutKind.Sequential )]
public readonly struct SciterApiStruct {
    public readonly int version;
    public readonly SciterClassName SciterClassName;
    public readonly SciterVersion SciterVersion;
    public readonly SciterDataReady SciterDataReady;
    public readonly SciterDataReadyAsync SciterDataReadyAsync;
    public readonly SciterProc SciterProc;
    public readonly SciterProcNd SciterProcNd;
    public readonly SciterLoadFile SciterLoadFile;
    public readonly SciterLoadHtml SciterLoadHtml;
    public readonly SciterSetCallback SciterSetCallback;
    public readonly SciterSetMasterCss SciterSetMasterCss;
    public readonly SciterAppendMasterCss SciterAppendMasterCss;
    public readonly SciterSetCss SciterSetCss;
    public readonly SciterSetMediaType SciterSetMediaType;
    public readonly SciterSetMediaVars SciterSetMediaVars;
    public readonly SciterGetMinWidth SciterGetMinWidth;
    public readonly SciterGetMinHeight SciterGetMinHeight;
    public readonly SciterCall SciterCall;
    public readonly SciterEval SciterEval;
    public readonly SciterUpdateWindow SciterUpdateWindow;
    public readonly SciterTranslateMessage SciterTranslateMessage;
    public readonly SciterSetOption SciterSetOption;
    public readonly SciterGetPpi SciterGetPpi;
    public readonly SciterGetViewExpando SciterGetViewExpando;
    public readonly SciterRenderD2D SciterRenderD2D;
    public readonly SciterD2DFactory SciterD2DFactory;
    public readonly SciterDwFactory SciterDwFactory;
    public readonly SciterGraphicsCaps SciterGraphicsCaps;
    public readonly SciterSetHomeUrl SciterSetHomeUrl;
    public readonly SciterCreateNsView SciterCreateNsView;
    public readonly SciterCreateWidget SciterCreateWidget;
    public readonly SciterCreateWindow SciterCreateWindow;
    public readonly SciterSetupDebugOutput SciterSetupDebugOutput;

    public readonly SciterUseElement SciterUseElement;
    public readonly SciterUnuseElement SciterUnuseElement;
    public readonly SciterGetRootElement SciterGetRootElement;
    public readonly SciterGetFocusElement SciterGetFocusElement;
    public readonly SciterFindElement SciterFindElement;
    public readonly SciterGetChildrenCount SciterGetChildrenCount;
    public readonly SciterGetNthChild SciterGetNthChild;
    public readonly SciterGetParentElement SciterGetParentElement;
    public readonly SciterGetElementHtmlCb SciterGetElementHtmlCb;
    public readonly SciterGetElementTextCb SciterGetElementTextCb;
    public readonly SciterSetElementText SciterSetElementText;
    public readonly SciterGetAttributeCount SciterGetAttributeCount;
    public readonly SciterGetNthAttributeNameCb SciterGetNthAttributeNameCb;
    public readonly SciterGetNthAttributeValueCb SciterGetNthAttributeValueCb;
    public readonly SciterGetAttributeByNameCb SciterGetAttributeByNameCb;
    public readonly SciterSetAttributeByName SciterSetAttributeByName;
    public readonly SciterClearAttributes SciterClearAttributes;
    public readonly SciterGetElementIndex SciterGetElementIndex;
    public readonly SciterGetElementType SciterGetElementType;
    public readonly SciterGetElementTypeCb SciterGetElementTypeCb;
    public readonly SciterGetStyleAttributeCb SciterGetStyleAttributeCb;
    public readonly SciterSetStyleAttribute SciterSetStyleAttribute;
    public readonly SciterGetElementLocation SciterGetElementLocation;
    public readonly SciterScrollToView SciterScrollToView;
    public readonly SciterUpdateElement SciterUpdateElement;
    public readonly SciterRefreshElementArea SciterRefreshElementArea;
    public readonly SciterSetCapture SciterSetCapture;
    public readonly SciterReleaseCapture SciterReleaseCapture;
    public readonly SciterGetElementHwnd SciterGetElementHwnd;
    public readonly SciterCombineUrl SciterCombineUrl;
    public readonly SciterSelectElements SciterSelectElements;
    public readonly SciterSelectElementsW SciterSelectElementsW;
    public readonly SciterSelectParent SciterSelectParent;
    public readonly SciterSelectParentW SciterSelectParentW;
    public readonly SciterSetElementHtml SciterSetElementHtml;
    public readonly SciterGetElementUid SciterGetElementUid;
    public readonly SciterGetElementByUid SciterGetElementByUid;
    public readonly SciterShowPopup SciterShowPopup;
    public readonly SciterShowPopupAt SciterShowPopupAt;
    public readonly SciterHidePopup SciterHidePopup;
    public readonly SciterGetElementState SciterGetElementState;
    public readonly SciterSetElementState SciterSetElementState;
    public readonly SciterCreateElement SciterCreateElement;
    public readonly SciterCloneElement SciterCloneElement;
    public readonly SciterInsertElement SciterInsertElement;
    public readonly SciterDetachElement SciterDetachElement;
    public readonly SciterDeleteElement SciterDeleteElement;
    public readonly SciterSetTimer SciterSetTimer;
    public readonly SciterDetachEventHandler SciterDetachEventHandler;
    public readonly SciterAttachEventHandler SciterAttachEventHandler;
    public readonly SciterWindowAttachEventHandler SciterWindowAttachEventHandler;
    public readonly SciterWindowDetachEventHandler SciterWindowDetachEventHandler;
    public readonly SciterSendEvent SciterSendEvent;
    public readonly SciterPostEvent SciterPostEvent;
    public readonly SciterCallBehaviorMethod SciterCallBehaviorMethod;
    public readonly SciterRequestElementData SciterRequestElementData;
    public readonly SciterHttpRequest SciterHttpRequest;
    public readonly SciterGetScrollInfo SciterGetScrollInfo;
    public readonly SciterSetScrollPos SciterSetScrollPos;
    public readonly SciterGetElementIntrinsicWidths SciterGetElementIntrinsicWidths;
    public readonly SciterGetElementIntrinsicHeight SciterGetElementIntrinsicHeight;
    public readonly SciterIsElementVisible SciterIsElementVisible;
    public readonly SciterIsElementEnabled SciterIsElementEnabled;
    public readonly SciterSortElements SciterSortElements;
    public readonly SciterSwapElements SciterSwapElements;
    public readonly SciterTraverseUiEvent SciterTraverseUiEvent;
    public readonly SciterCallScriptingMethod SciterCallScriptingMethod;
    public readonly SciterCallScriptingFunction SciterCallScriptingFunction;
    public readonly SciterEvalElementScript SciterEvalElementScript;
    public readonly SciterAttachHwndToElement SciterAttachHwndToElement;
    public readonly SciterControlGetType SciterControlGetType;
    public readonly SciterGetValue SciterGetValue;
    public readonly SciterSetValue SciterSetValue;
    public readonly SciterGetExpando SciterGetExpando;
    public readonly SciterGetObject SciterGetObject;
    public readonly SciterGetElementNamespace SciterGetElementNamespace;
    public readonly SciterGetHighlightedElement SciterGetHighlightedElement;
    public readonly SciterSetHighlightedElement SciterSetHighlightedElement;

    public readonly SciterNodeAddRef SciterNodeAddRef;
    public readonly SciterNodeRelease SciterNodeRelease;
    public readonly SciterNodeCastFromElement SciterNodeCastFromElement;
    public readonly SciterNodeCastToElement SciterNodeCastToElement;
    public readonly SciterNodeFirstChild SciterNodeFirstChild;
    public readonly SciterNodeLastChild SciterNodeLastChild;
    public readonly SciterNodeNextSibling SciterNodeNextSibling;
    public readonly SciterNodePrevSibling SciterNodePrevSibling;
    public readonly SciterNodeParent SciterNodeParent;
    public readonly SciterNodeNthChild SciterNodeNthChild;
    public readonly SciterNodeChildrenCount SciterNodeChildrenCount;
    public readonly SciterNodeType SciterNodeType;
    public readonly SciterNodeGetText SciterNodeGetText;
    public readonly SciterNodeSetText SciterNodeSetText;
    public readonly SciterNodeInsert SciterNodeInsert;
    public readonly SciterNodeRemove SciterNodeRemove;
    public readonly SciterCreateTextNode SciterCreateTextNode;
    public readonly SciterCreateCommentNode SciterCreateCommentNode;

    public readonly ValueInit ValueInit;
    public readonly ValueClear ValueClear;
    public readonly ValueCompare ValueCompare;
    public readonly ValueCopy ValueCopy;
    public readonly ValueIsolate ValueIsolate;
    public readonly ValueTypeDelegate ValueType;
    public readonly ValueStringData ValueStringData;
    public readonly ValueStringDataSet ValueStringDataSet;
    public readonly ValueIntData ValueIntData;
    public readonly ValueIntDataSet ValueIntDataSet;
    public readonly ValueInt64Data ValueInt64Data;
    public readonly ValueInt64DataSet ValueInt64DataSet;
    public readonly ValueFloatData ValueFloatData;
    public readonly ValueFloatDataSet ValueFloatDataSet;
    public readonly ValueBinaryData ValueBinaryData;
    public readonly ValueBinaryDataSet ValueBinaryDataSet;
    public readonly ValueElementsCount ValueElementsCount;
    public readonly ValueNthElementValue ValueNthElementValue;
    public readonly ValueNthElementValueSet ValueNthElementValueSet;
    public readonly ValueNthElementKey ValueNthElementKey;
    public readonly ValueEnumElements ValueEnumElements;
    public readonly ValueSetValueToKey ValueSetValueToKey;
    public readonly ValueGetValueOfKey ValueGetValueOfKey;
    public readonly ValueToString ValueToString;
    public readonly ValueFromString ValueFromString;
    public readonly ValueInvoke ValueInvoke;
    public readonly ValueNativeFunctorSet ValueNativeFunctorSet;
    public readonly ValueIsNativeFunctor ValueIsNativeFunctor;

    public readonly IntPtr reserved1;
    public readonly IntPtr reserved2;
    public readonly IntPtr reserved3;
    public readonly IntPtr reserved4;

    public readonly SciterOpenArchive SciterOpenArchive;
    public readonly SciterGetArchiveItem SciterGetArchiveItem;
    public readonly SciterCloseArchive SciterCloseArchive;

    public readonly SciterFireEvent SciterFireEvent;
    public readonly SciterGetCallbackParam SciterGetCallbackParam;
    public readonly SciterPostCallback SciterPostCallback;
    public readonly GetSciterGraphicsApi GetSciterGraphicsApi;
    public readonly GetSciterRequestApi GetSciterRequestApi;
    public readonly SciterCreateOnDirectXWindow SciterCreateOnDirectXWindow;
    public readonly SciterRenderOnDirectXWindow SciterRenderOnDirectXWindow;
    public readonly SciterRenderOnDirectXTexture SciterRenderOnDirectXTexture;
    public readonly SciterProcX SciterProcX;

    public readonly SciterAtomValue SciterAtomValue;
    public readonly SciterAtomNameCB SciterAtomNameCB;
    public readonly SciterSetGlobalAsset SciterSetGlobalAsset;
    public readonly SciterGetElementAsset SciterGetElementAsset;

    public readonly SciterSetVariable SciterSetVariable;
    public readonly SciterGetVariable SciterGetVariable;
    public readonly SciterElementUnwrap SciterElementUnwrap;
    public readonly SciterElementWrap SciterElementWrap;
    public readonly SciterNodeUnwrap SciterNodeUnwrap;
    public readonly SciterNodeWrap SciterNodeWrap;

    public readonly SciterReleaseGlobalAsset SciterReleaseGlobalAsset;
    public readonly SciterExec SciterExec;
    public readonly SciterWindowExec SciterWindowExec;
    public readonly SciterEGLGetProcAddress SciterEGLGetProcAddress;
    public readonly SciterEGLSendEvent SciterEGLSendEvent;
    public readonly SciterRequestAnimationFrameEvent SciterRequestAnimationFrameEvent;

}

}

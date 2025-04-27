using EmptyFlow.SciterAPI.Enums;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {

    public delegate RequestResult RequestsRequestUse ( nint rq );
    public delegate RequestResult RequestsRequestUnUse ( nint rq );
    public delegate RequestResult RequestsRequestUrl ( nint rq, lpcstrReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestContentUrl ( nint rq, lpcstrReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetRequestType ( nint rq, IntPtr pType );
    public delegate RequestResult RequestsRequestGetRequestedDataType ( nint rq, out SciterResourceType pData );
    public delegate RequestResult RequestsRequestGetReceivedDataType ( nint rq, lpcstrReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetNumberOfParameters ( nint rq, out uint pNumber );
    public delegate RequestResult RequestsRequestGetNthParameterName ( nint rq, uint n, lpcwstReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetNthParameterValue ( nint rq, uint n, lpcwstReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetTimes ( nint rq, out uint pStarted, out uint pEnded );
    public delegate RequestResult RequestsRequestGetNumberOfRqHeaders ( nint rq, out uint pNumber );
    public delegate RequestResult RequestsRequestGetNthRqHeaderName ( nint rq, uint n, lpcwstReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetNthRqHeaderValue ( nint rq, uint n, lpcwstReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetNumberOfRspHeaders ( nint rq, out uint pNumber );
    public delegate RequestResult RequestsRequestGetNthRspHeaderName ( nint rq, uint n, lpcwstReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetNthRspHeaderValue ( nint rq, uint n, lpcwstReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetCompletionStatus ( nint rq, out RequestState pState, out uint pCompletionStatus );
    public delegate RequestResult RequestsRequestGetProxyHost ( nint rq, lpcstrReceiver rcv, nint rcv_param );
    public delegate RequestResult RequestsRequestGetProxyPort ( nint rq, out uint pPort );
    public delegate RequestResult RequestsRequestSetSucceeded ( nint rq, uint status, byte[] dataOrNull, uint dataLength );
    public delegate RequestResult RequestsRequestSetFailed ( nint rq, uint status, byte[] dataOrNull, uint dataLength );
    public delegate RequestResult RequestsRequestAppendDataChunk ( nint rq, byte[] data, uint dataLength );
    public delegate RequestResult RequestsRequestSetRqHeader ( nint rq, [MarshalAs ( UnmanagedType.LPWStr )] string name, [MarshalAs ( UnmanagedType.LPWStr )] string value );
    public delegate RequestResult RequestsRequestSetRspHeader ( nint rq, [MarshalAs ( UnmanagedType.LPWStr )] string name, [MarshalAs ( UnmanagedType.LPWStr )] string value );
    public delegate RequestResult RequestsRequestSetReceivedDataType ( nint rq, /*LPCSTR*/ byte[] type );
    public delegate RequestResult RequestsRequestSetReceivedDataEncoding ( nint rq, /* LPCSTR */ byte[] encoding );
    public delegate RequestResult RequestsRequestGetData ( nint rq, lpcbyteReceiver rcv, uint rcv_param );

    [StructLayout ( LayoutKind.Sequential )]
    public readonly struct RequestApiStruct {

        /// <summary>
        /// a.k.a AddRef().
        /// </summary>
        public readonly RequestsRequestUse RequestUse;

        /// <summary>
        /// a.k.a Release().
        /// </summary>
        public readonly RequestsRequestUnUse RequestUnUse;

        /// <summary>
        /// Get requested URL.
        /// </summary>
        public readonly RequestsRequestUrl RequestUrl;

        /// <summary>
        /// Get real, content URL (after possible redirection)
        /// </summary>
        public readonly RequestsRequestContentUrl RequestContentUrl;

        /// <summary>
        /// Get requested data type as string "GET", "POST", etc.
        /// </summary>
        public readonly RequestsRequestGetRequestType RequestGetRequestType;

        /// <summary>
        /// Get requested data type.
        /// </summary>
        public readonly RequestsRequestGetRequestedDataType RequestGetRequestedDataType;

        /// <summary>
        /// Get received data type, string, mime type.
        /// </summary>
        public readonly RequestsRequestGetReceivedDataType RequestGetReceivedDataType;

        /// <summary>
        /// Get number of request parameters passed.
        /// </summary>
        public readonly RequestsRequestGetNumberOfParameters RequestGetNumberOfParameters;

        /// <summary>
        /// Get nth request parameter name.
        /// </summary>
        public readonly RequestsRequestGetNthParameterName RequestGetNthParameterName;

        /// <summary>
        /// Get nth request parameter value.
        /// </summary>
        public readonly RequestsRequestGetNthParameterValue RequestGetNthParameterValue;

        /// <summary>
        /// Get request times , ended - started = milliseconds to get the requst.
        /// </summary>
        public readonly RequestsRequestGetTimes RequestGetTimes;

        /// <summary>
        /// Get number of request headers.
        /// </summary>
        public readonly RequestsRequestGetNumberOfRqHeaders RequestGetNumberOfRqHeaders;

        /// <summary>
        /// Get nth request header name.
        /// </summary>
        public readonly RequestsRequestGetNthRqHeaderName RequestGetNthRqHeaderName;

        /// <summary>
        /// Get nth request header value.
        /// </summary>
        public readonly RequestsRequestGetNthRqHeaderValue RequestGetNthRqHeaderValue;

        /// <summary>
        /// Get number of response headers.
        /// </summary>
        public readonly RequestsRequestGetNumberOfRspHeaders RequestGetNumberOfRspHeaders;

        /// <summary>
        /// Get nth response header name.
        /// </summary>
        public readonly RequestsRequestGetNthRspHeaderName RequestGetNthRspHeaderName;

        /// <summary>
        /// Get nth response header value.
        /// </summary>
        public readonly RequestsRequestGetNthRspHeaderValue RequestGetNthRspHeaderValue;

        /// <summary>
        /// Get completion status (CompletionStatus - http response code : 200, 404, etc.).
        /// </summary>
        public readonly RequestsRequestGetCompletionStatus RequestGetCompletionStatus;

        /// <summary>
        /// Get proxy host.
        /// </summary>
        public readonly RequestsRequestGetProxyHost RequestGetProxyHost;

        /// <summary>
        /// Get proxy port.
        /// </summary>
        public readonly RequestsRequestGetProxyPort RequestGetProxyPort;

        /// <summary>
        /// Mark reequest as complete with status and data.
        /// </summary>
        public readonly RequestsRequestSetSucceeded RequestSetSucceeded;

        /// <summary>
        /// Mark reequest as complete with failure and optional data.
        /// </summary>
        public readonly RequestsRequestSetFailed RequestSetFailed;

        /// <summary>
        /// Append received data chunk.
        /// </summary>
        public readonly RequestsRequestAppendDataChunk RequestAppendDataChunk;

        /// <summary>
        /// Set request header (single item).
        /// </summary>
        public readonly RequestsRequestSetRqHeader RequestSetRqHeader;

        /// <summary>
        /// Set respone header (single item).
        /// </summary>
        public readonly RequestsRequestSetRspHeader RequestSetRspHeader;

        /// <summary>
        /// Set received data type, string, mime type.
        /// </summary>
        public readonly RequestsRequestSetReceivedDataType RequestSetReceivedDataType;

        /// <summary>
        /// Set received data encoding, string.
        /// </summary>
        public readonly RequestsRequestSetReceivedDataEncoding RequestSetReceivedDataEncoding;

        /// <summary>
        /// Get received (so far) data.
        /// </summary>
        public readonly RequestsRequestGetData RequestGetData;

    }

}

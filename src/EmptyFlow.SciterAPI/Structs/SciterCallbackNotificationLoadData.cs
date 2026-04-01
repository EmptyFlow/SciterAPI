using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

	[StructLayout ( LayoutKind.Sequential )]
	public struct SciterCallbackNotificationLoadData { // original name LPSCN_LOAD_DATA
		public SciterCallbackNotificationCode code;
		public nint hwnd; /**< [in] HWINDOW of the window this callback was attached to.*/
		[MarshalAs ( UnmanagedType.LPWStr )] public string uri; // [in] Zero terminated string, fully qualified uri, for example "http://server/folder/file.ext".*/
		public nint outData; // [in,out] pointer to loaded data to return. if data exists in the cache then this field contain pointer to it
		public uint outDataSize; // [in,out] loaded data size to return.
		public uint dataType; // [in] SciterResourceType
		public nint requestId; // [in] request handle that can be used with sciter-x-request API */
		public nint principal; // Element
		public nint initiator; // Initiator
	}

}

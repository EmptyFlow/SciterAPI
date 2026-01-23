using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    [StructLayout ( LayoutKind.Sequential )]
    public struct DataArrivedParameters { // Original name DATA_ARRIVED_PARAMS
        public nint initiator;// HELEMENT element intiator of SciterRequestElementData request
		public byte[] data;// LPCBYTE data buffer
		public uint dataSize; // size of data
		public uint dataType; // data type passed "as is" from SciterRequestElementData
		public uint status; // status = 0 (dataSize == 0) - unknown error. status = 100..505 - http response status, Note: 200 - OK! status > 12000 - wininet error code, see ERROR_INTERNET_*** in wininet.h
		[MarshalAs ( UnmanagedType.LPWStr )]
        public string uri;// LPCWSTR requested url
	}

}

using System.Runtime.InteropServices;
using System.Text;

namespace EmptyFlow.SciterAPI.Structs {

    internal record LPCWStrReceiverCallback {

        public StringBuilder Result = new StringBuilder ();

        public void Callback ( IntPtr byteArrayPointer, uint countBytes, IntPtr parameter ) {
            Result.Append ( Marshal.PtrToStringUni ( byteArrayPointer, Convert.ToInt32 ( countBytes ) ) );
        }

        public void Clear() {
            if ( Result.Length == 0 ) return;

            Result.Clear ();
        }

    }

}

using EmptyFlow.SciterAPI.Enums;
using EmptyFlow.SciterAPI.Structs;
using System.Runtime.InteropServices;
using System.Text;

namespace EmptyFlow.SciterAPI {

    public partial class SciterAPIHost {

        public IEnumerable<IntPtr> MakeCssSelector ( string cssSelector ) {
            IntPtr element;
            var domResult = m_basicApi.SciterGetRootElement ( m_mainWindow, out element );
            if ( domResult != DomResult.SCDOM_OK ) return Enumerable.Empty<IntPtr> ();

            var elements = new List<IntPtr> ();
            SciterElementCallback callback = ( IntPtr he, IntPtr param ) => {
                elements.Add ( he );
                return false;
            };
            m_basicApi.SciterSelectElementsW ( element, cssSelector, callback, 1 );

            return elements;
        }

        public IEnumerable<IntPtr> MakeCssSelector ( string cssSelector, nint insideElement ) {
            var elements = new List<IntPtr> ();
            SciterElementCallback callback = ( IntPtr he, IntPtr param ) => {
                elements.Add ( he );
                return false;
            };
            var domResult = m_basicApi.SciterSelectElementsW ( insideElement, cssSelector, callback, 1 );
            if ( domResult != DomResult.SCDOM_OK ) return Enumerable.Empty<IntPtr> ();

            return elements;
        }

        public int GetElementChildrensCount ( IntPtr element ) {
            var domResult = m_basicApi.SciterGetChildrenCount ( element, out var count );
            if ( domResult != DomResult.SCDOM_OK ) throw new Exception ( "Can't get childrens count!" );

            return (int) count;
        }

        public IEnumerable<nint> GetElementChildrens ( IntPtr element ) {
            var domResult = m_basicApi.SciterGetChildrenCount ( element, out var count );
            if ( domResult != DomResult.SCDOM_OK ) return [];

            var result = new List<nint> ();
            for ( var i = 0; i < count; i++ ) {
                var nthChildResult = m_basicApi.SciterGetNthChild ( element, (uint) i, out var nthElement );
                if ( nthChildResult != DomResult.SCDOM_OK ) continue;

                result.Add ( nthElement );
            }
            return result;
        }

        public string GetElementHtml ( IntPtr element, bool outer ) {
            var strings = new List<string> ();
            lpcbyteReceiver callback = ( IntPtr bytes, uint num_bytes, IntPtr param ) => {
                strings.Add ( Marshal.PtrToStringAnsi ( bytes, Convert.ToInt32 ( num_bytes ) ) );
            };
            m_basicApi.SciterGetElementHtmlCb ( element, outer, callback, 1 );

            return string.Join ( "", strings );
        }

        public string GetElementText ( IntPtr element ) {
            var strings = new List<string> ();
            lpcwstReceiver callback = ( IntPtr bytes, uint num_bytes, IntPtr param ) => {
                strings.Add ( Marshal.PtrToStringUni ( bytes, Convert.ToInt32 ( num_bytes ) ) );
            };
            m_basicApi.SciterGetElementTextCb ( element, callback, 1 );

            return string.Join ( "", strings );
        }

        public void SetElementText ( IntPtr element, string text ) {
            m_basicApi.SciterSetElementText ( element, text, (uint) text.Length );
        }

        public void SetElementHtml ( IntPtr element, string text, SetElementHtml insertMode ) {
            var bytes = Encoding.UTF8.GetBytes ( text );
            m_basicApi.SciterSetElementHtml ( element, bytes, (uint) bytes.Length, insertMode );
        }

        public string GetElementAttribute ( IntPtr element, string name ) {
            var strings = new List<string> ();
            lpcwstReceiver callback = ( IntPtr bytes, uint num_bytes, IntPtr param ) => {
                strings.Add ( Marshal.PtrToStringUni ( bytes, Convert.ToInt32 ( num_bytes ) ) );
            };
            m_basicApi.SciterGetAttributeByNameCb ( element, name, callback, 1 );

            return string.Join ( "", strings );
        }

        public void ClearAttributes ( nint element ) => m_basicApi.SciterClearAttributes ( element );

        public IEnumerable<string> GetElementAttributeNames ( IntPtr element ) {
            m_basicApi.SciterGetAttributeCount ( element, out var count );

            if ( count <= 0 ) return Enumerable.Empty<string> ();

            var result = new List<string> ();

            for ( uint i = 0; i < count; i++ ) {
                var receiver = new LPCStrReceiverCallback ();
                m_basicApi.SciterGetNthAttributeNameCb ( element, i, receiver.Callback, 1 );

                result.Add ( receiver.Result.ToString () );
            }

            return result;
        }

        public bool GetElementHasAttribute ( IntPtr element, string name, CaseInsensitiveMode caseSensitiveMode = CaseInsensitiveMode.CaseInsensitive ) {
            m_basicApi.SciterGetAttributeCount ( element, out var count );

            if ( count <= 0 ) return false;

            for ( uint i = 0; i < count; i++ ) {
                var receiver = new LPCStrReceiverCallback ();
                m_basicApi.SciterGetNthAttributeNameCb ( element, i, receiver.Callback, 1 );

                var attributeName = receiver.Result.ToString ();
                if ( caseSensitiveMode == CaseInsensitiveMode.CaseInsensitive ) attributeName = attributeName.ToLowerInvariant ();

                if ( attributeName == name ) return true;
            }

            return false;
        }

        public IDictionary<string, string> GetElementAttributes ( IntPtr element ) {
            m_basicApi.SciterGetAttributeCount ( element, out var count );

            if ( count <= 0 ) return new Dictionary<string, string> ();

            var result = new Dictionary<string, string> ();

            var nameReceiver = new LPCStrReceiverCallback ();
            var valueReceiver = new LPCWStrReceiverCallback ();

            for ( uint i = 0; i < count; i++ ) {
                nameReceiver.Clear ();
                valueReceiver.Clear ();

                m_basicApi.SciterGetNthAttributeNameCb ( element, i, nameReceiver.Callback, 1 );
                m_basicApi.SciterGetNthAttributeValueCb ( element, i, valueReceiver.Callback, 1 );

                result.Add ( nameReceiver.Result.ToString (), valueReceiver.Result.ToString () );
            }

            return result;
        }

        public void SetElementAttribute ( IntPtr element, string name, string value ) {
            m_basicApi.SciterSetAttributeByName ( element, name, value );
        }

        public string GetElementStyleProperty ( IntPtr element, string name ) {
            var valueReceiver = new LPCWStrReceiverCallback ();

            m_basicApi.SciterGetStyleAttributeCb ( element, name, valueReceiver.Callback, 0 );

            return valueReceiver.Value ();
        }

        public void SetElementStyleProperty ( IntPtr element, string name, string value ) {
            m_basicApi.SciterSetStyleAttribute ( element, name, value );
        }

    }

}

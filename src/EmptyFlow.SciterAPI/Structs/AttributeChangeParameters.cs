using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {

    [StructLayout ( LayoutKind.Sequential )]
    public struct AttributeChangeParameters { // original name ATTRIBUTE_CHANGE_PARAMS 

        /// <summary>
        /// Affected element.
        /// </summary>
        public nint Element;

        /// <summary>
        /// Attribute name.
        /// </summary>
        [MarshalAs ( UnmanagedType.LPStr )]
        public string Name;

        /// <summary>
        /// New attribute value, NULL if attribute was deleted.
        /// </summary>
         [MarshalAs ( UnmanagedType.LPWStr )]
        public string Value;

    }

}

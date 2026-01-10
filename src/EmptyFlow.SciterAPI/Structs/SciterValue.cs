using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

	public static class SciterValueUnitTypes {

		public const uint Nothing = 1; // Original name UT_NOTHING

		public const uint String = 0; // Original name UT_STRING_STRING
		public const uint StringError = 1; // Original name UT_STRING_ERROR - is an error string
		public const uint StringSecure = 2; // Original name UT_STRING_SECURE - secure string ("wiped" on destroy)
		public const uint StringSymbol = 0xffff; // Original name UT_NOTHING - symbol in tiscript sense

		public const uint ObjectArray = 0; // Original name UT_OBJECT_ARRAY - type T_OBJECT of type Array
		public const uint Object = 1; // Original name UT_OBJECT_OBJECT - type T_OBJECT of type Object
		public const uint ObjectClass = 2; // Original name UT_OBJECT_CLASS - type T_OBJECT of type Class (class or namespace)
		public const uint ObjectNative = 3; // Original name UT_OBJECT_NATIVE - type T_OBJECT of native Type with data slot (LPVOID)
		public const uint ObjectFunction = 4; // Original name UT_OBJECT_FUNCTION - type T_OBJECT of type Function
		public const uint ObjectError = 5; // Original name UT_OBJECT_ERROR - type T_OBJECT of type Error
		public const uint ObjectBuffer = 6; // Original name UT_OBJECT_BUFFER - type T_OBJECT of type ArrayBuffer or TypedArray

	}

	[StructLayout ( LayoutKind.Sequential )]
	public struct SciterValue {
		public ValueType t;// type: enum VALUE_TYPE
		public uint u;// unit
		public ulong d;// data

		public bool IsUndefined => t == ValueType.T_UNDEFINED;
		public bool IsBoolean => t == ValueType.T_BOOL;
		public bool IsInteger => t == ValueType.T_INT;
		public bool IsFloat => t == ValueType.T_FLOAT;
		public bool IsString => t == ValueType.T_STRING;
		public bool IsDate => t == ValueType.T_DATE;
		public bool IsLong => t == ValueType.T_BIG_INT;
		public bool IsMap => t == ValueType.T_MAP;
		public bool IsArray => t == ValueType.T_ARRAY;
		public bool IsFunction => t == ValueType.T_FUNCTION;
		public bool IsObject => t == ValueType.T_OBJECT;
		public bool IsAsset => t == ValueType.T_ASSET;
		public bool IsColor => t == ValueType.T_COLOR;
		public bool IsDuration => t == ValueType.T_DURATION;
		public bool IsAngle => t == ValueType.T_ANGLE;
		public bool IsNull => t == ValueType.T_NULL && u == 0;

		public bool IsNothing => t == ValueType.T_UNDEFINED && u == SciterValueUnitTypes.Nothing;
		public bool IsSymbol => t == ValueType.T_STRING && u == SciterValueUnitTypes.StringSymbol;
		public bool IsErrorString => t == ValueType.T_STRING && u == SciterValueUnitTypes.StringError;
		public bool IsArrayLike => t == ValueType.T_ARRAY || ( t == ValueType.T_OBJECT && u == SciterValueUnitTypes.ObjectArray );
		public bool IsBytes => t == ValueType.T_BYTES || ( t == ValueType.T_OBJECT && u == SciterValueUnitTypes.ObjectBuffer );

		public bool IsObjectNative => t == ValueType.T_OBJECT && u == SciterValueUnitTypes.ObjectNative;
		public bool IsObjectArray => t == ValueType.T_OBJECT && u == SciterValueUnitTypes.ObjectArray;
		public bool IsObjectFunction => t == ValueType.T_OBJECT && u == SciterValueUnitTypes.ObjectFunction;
		public bool IsObjectObject => t == ValueType.T_OBJECT && u == SciterValueUnitTypes.Object;
		public bool IsObjectClass => t == ValueType.T_OBJECT && u == SciterValueUnitTypes.ObjectClass;
		public bool IsObjectError => t == ValueType.T_OBJECT && u == SciterValueUnitTypes.ObjectError;
		public bool IsObjectBuffer => t == ValueType.T_OBJECT && u == SciterValueUnitTypes.ObjectBuffer;

	}

}

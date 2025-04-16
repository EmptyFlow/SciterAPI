using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    public partial class SciterAPIHost {


        public SciterValue CreateValue<T> ( T value ) {
            if ( value == null ) throw new ArgumentNullException ( nameof ( value ) );

            SciterValue sciterValue;
            m_basicApi.ValueInit ( out sciterValue );

            switch ( value ) {
                case bool boolValue:
                    m_basicApi.ValueIntDataSet ( ref sciterValue, boolValue ? 1 : 0, ValueType.T_BOOL, 0 );
                    break;
                case int intValue:
                    m_basicApi.ValueIntDataSet ( ref sciterValue, intValue, ValueType.T_INT, 0 );
                    break;
                case long longValue:
                    m_basicApi.ValueInt64DataSet ( ref sciterValue, longValue, ValueType.T_BIG_INT, 0 );
                    break;
                case decimal decimalValue:
                    m_basicApi.ValueFloatDataSet ( ref sciterValue, Convert.ToDouble ( decimalValue ), ValueType.T_FLOAT, 0 );
                    break;
                case double doubleValue:
                    m_basicApi.ValueFloatDataSet ( ref sciterValue, doubleValue, ValueType.T_FLOAT, 0 );
                    break;
                case float floatValue:
                    m_basicApi.ValueFloatDataSet ( ref sciterValue, floatValue, ValueType.T_FLOAT, 0 );
                    break;
                case string stringValue:
                    m_basicApi.ValueStringDataSet ( ref sciterValue, stringValue, Convert.ToUInt32 ( stringValue.Length ), 0 );
                    break;
                case IEnumerable<SciterValue> enumerableValues:
                    m_basicApi.ValueIntDataSet ( ref sciterValue, enumerableValues.Count (), ValueType.T_ARRAY, 0 ); // create empty array

                    for ( int i = 0; i < enumerableValues.Count (); i++ ) {
                        var indexValue = enumerableValues.ElementAt ( i );
                        SetArrayItem ( ref sciterValue, i, ref indexValue );
                    }
                    break;
                case IDictionary<string, SciterValue> dictionaryValues:
                    m_basicApi.ValueIntDataSet ( ref sciterValue, 0, ValueType.T_MAP, 0 ); // create empty map

                    foreach ( var dictionaryItem in dictionaryValues ) {
                        var mapValue = dictionaryItem.Value;
                        SetMapItem ( ref sciterValue, dictionaryItem.Key, ref mapValue );
                    }
                    break;
                default: throw new NotSupportedException ( $"Create value from type {value.GetType ().Name}" );
            }

            return sciterValue;
        }

        public SciterValue CreateColorValue ( uint color ) {
            SciterValue sciterValue;
            m_basicApi.ValueInit ( out sciterValue );
            m_basicApi.ValueIntDataSet ( ref sciterValue, Convert.ToInt32 ( color ), ValueType.T_COLOR, 0 );

            return sciterValue;
        }

        public SciterValue CreateDurationValue ( double seconds ) {
            SciterValue sciterValue;
            m_basicApi.ValueInit ( out sciterValue );
            m_basicApi.ValueFloatDataSet ( ref sciterValue, seconds, ValueType.T_DURATION, 0 );

            return sciterValue;
        }

        public SciterValue CreateAngleValue ( double radians ) {
            SciterValue sciterValue;
            m_basicApi.ValueInit ( out sciterValue );
            m_basicApi.ValueFloatDataSet ( ref sciterValue, radians, ValueType.T_ANGLE, 0 );

            return sciterValue;
        }

        public SciterValue CreateSecureString ( string value ) {
            m_basicApi.ValueInit ( out var sciterValue );
            m_basicApi.ValueStringDataSet ( ref sciterValue, value, Convert.ToUInt32 ( value.Length ), SciterValueUnitTypes.StringSecure );
            return sciterValue;
        }

        public SciterValue CreateError ( string value ) {
            m_basicApi.ValueInit ( out var sciterValue );
            m_basicApi.ValueStringDataSet ( ref sciterValue, value, Convert.ToUInt32 ( value.Length ), SciterValueUnitTypes.StringError );
            return sciterValue;
        }

        /// <summary>
        /// Clear value.
        /// </summary>
        /// <param name="array">The value that will be cleared.</param>
        public void ClearValue ( ref SciterValue value ) => m_basicApi.ValueClear ( out value );

        /// <summary>
        /// Get array or map count.
        /// </summary>
        /// <param name="array">The array from there the value will be retrieved.</param>
        /// <param name="index">The index of the array element.</param>
        /// <returns>Retrieved value.</returns>
        public int GetArrayOrMapCount ( ref SciterValue array ) {
            m_basicApi.ValueElementsCount ( ref array, out var count );
            return count;
        }

        /// <summary>
        /// Get array item.
        /// </summary>
        /// <param name="array">The array from there the value will be retrieved.</param>
        /// <param name="index">The index of the array element.</param>
        /// <returns>Retrieved value.</returns>
        public SciterValue GetArrayItem ( ref SciterValue array, int index ) {
            m_basicApi.ValueNthElementValue ( ref array, index, out var result );
            return result;
        }

        /// <summary>
        /// Set the value of the array element with index.
        /// </summary>
        /// <param name="array">The array when the value will be inserted.</param>
        /// <param name="index">The index of the array element.</param>
        /// <param name="value">The value to be inserted into the array.</param>
        public void SetArrayItem ( ref SciterValue array, int index, ref SciterValue value ) {
            m_basicApi.ValueNthElementValueSet ( ref array, index, ref value );
        }

        /// <summary>
        /// Append item ot array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public void AppendItemToArray ( ref SciterValue array, ref SciterValue value ) {
            var length = GetArrayOrMapCount ( ref array );
            m_basicApi.ValueNthElementValueSet ( ref array, length, ref value );
        }

        /// <summary>
        /// Get map item.
        /// </summary>
        /// <param name="array">The array from there the value will be retrieved.</param>
        /// <param name="index">The index of the array element.</param>
        /// <returns>Retrieved value.</returns>
        public SciterValue GetMapItem ( ref SciterValue map, string key ) {
            var stringKey = CreateValue ( key );
            m_basicApi.ValueGetValueOfKey ( ref map, ref stringKey, out var result );
            return result;
        }

        /// <summary>
        /// Get map item.
        /// </summary>
        /// <param name="array">The array from there the value will be retrieved.</param>
        /// <param name="index">The index of the array element.</param>
        /// <returns>Retrieved value.</returns>
        public void SetMapItem ( ref SciterValue map, string key, ref SciterValue value ) {
            var stringKey = CreateValue ( key );
            m_basicApi.ValueSetValueToKey ( ref map, ref stringKey, ref value );
        }

        public IDictionary<string, SciterValue> GetMapItems ( ref SciterValue array ) {
            var result = new Dictionary<string, SciterValue> ();
            KeyValueCallback callback = ( IntPtr param, ref SciterValue key, ref SciterValue pval ) => {
                var name = GetValueString ( ref key );
                result.Add ( name, pval );
                return true;
            };
            m_basicApi.ValueEnumElements ( ref array, callback, 1 );

            return result;
        }

        public SciterValue GetValueMapKey ( ref SciterValue value, int index ) {
            m_basicApi.ValueNthElementKey ( ref value, index, out var result );
            return result;
        }

        public int GetValueInt32 ( ref SciterValue value ) {
            m_basicApi.ValueIntData ( ref value, out var result );
            return result;
        }

        public long GetValueInt64 ( ref SciterValue value ) {
            m_basicApi.ValueInt64Data ( ref value, out var result );
            return result;
        }

        public double GetValueDouble ( ref SciterValue value ) {
            m_basicApi.ValueFloatData ( ref value, out var result );
            return result;
        }

        public string GetValueString ( ref SciterValue value ) {
            m_basicApi.ValueStringData ( ref value, out var stringPointer, out var length );
            return Marshal.PtrToStringUni ( stringPointer, (int) length ) ?? "";
        }

        public void IsolateValue ( ref SciterValue value ) => m_basicApi.ValueIsolate ( ref value );

        public void ValueInvoke ( ref SciterValue value, SciterValue? context = default, params SciterValue[] parameters ) {
            var contextPointer = nint.Zero;
            if ( context.HasValue ) {
                contextPointer = Marshal.AllocHGlobal ( Marshal.SizeOf ( context.Value ) );
                Marshal.StructureToPtr ( context.Value, contextPointer, false );
            }
            m_basicApi.ValueInvoke ( ref value, contextPointer, (uint) parameters.Count (), parameters, out var result, IntPtr.Zero );

            if (contextPointer != nint.Zero) Marshal.FreeHGlobal ( contextPointer );
        }

    }

}

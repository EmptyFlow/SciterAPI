namespace EmptyFlow.SciterAPI {

    public partial class SciterAPIHost {


        public SciterValue CreateValue<T> ( T value ) {
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
                    m_basicApi.ValueInt64DataSet ( ref sciterValue, longValue, ValueType.T_INT, 0 );
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
                    for ( int i = 0; i < enumerableValues.Count (); i++ ) {
                        var indexValue = enumerableValues.ElementAt ( i );
                        SetArrayItem ( ref sciterValue, i, ref indexValue );
                    }
                    break;
                default:
                    break;
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
        public SciterValue GetArrayItem( ref SciterValue array, int index ) {
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

    }

}

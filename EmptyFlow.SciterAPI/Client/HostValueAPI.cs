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
                    for ( int i = 0; i < enumerableValues.Count (); i++ ) {
                        var indexValue = enumerableValues.ElementAt ( i );
                        SetArrayItem ( ref sciterValue, i, ref indexValue );
                    }
                    break;
                case IDictionary<string, SciterValue> dictionaryValues:
                    foreach ( var dictionaryItem in dictionaryValues ) {

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
        /*
               // enum
      void enum_elements(enum_cb& cb) const
      {
        ValueEnumElements(const_cast<value*>(this), &enum_cb::_callback, &cb);
      }

#ifdef CPP11
      // calls cbf for each key/value pair found in T_OBJECT or T_MAP
      void each_key_value(key_value_cb cbf) const
      {
        ValueEnumElements(const_cast<value*>(this), &enum_cb::lambda_callback, &cbf);
      }
#endif

      value key(int n) const
      {
        value r;
        ValueNthElementKey( this, n, &r);
        return r;
      }
         */

        /*
               // T_OBJECT/UT_OBJECT_FUNCTION only, call TS function
      // 'self' here is what will be known as 'this' inside the function, can be undefined for invocations of global functions
      value call( int argc, const value* argv, value self = value(), const WCHAR* url_or_script_name = 0) const
      {
        value rv;
        ValueInvoke(const_cast<value*>(this),&self,argc,argv,&rv,LPCWSTR(url_or_script_name));
        return rv;
      }

      value call() const {  return call(0,0);  }
      value call( const value& p1 ) const {  return call(1,&p1); }
      value call( const value& p1, const value& p2 )  const { value args[2] = { p1,p2 };  return call(2,args); }
      value call( const value& p1, const value& p2, const value& p3 ) const { value args[3] = { p1,p2,p3 };  return call(3,args); }
      value call( const value& p1, const value& p2, const value& p3, const value& p4 )  const { value args[4] = { p1,p2,p3,p4 };  return call(4,args); }

      value call_this(const value& _this) const { return call(0, 0, _this); }
      value call_this(const value& _this, const value& p1) const { return call(1, &p1, _this); }
      value call_this(const value& _this, const value& p1, const value& p2)  const { value args[2] = { p1,p2 };  return call(2, args, _this); }
      value call_this(const value& _this, const value& p1, const value& p2, const value& p3) const { value args[3] = { p1,p2,p3 };  return call(3, args, _this); }
      value call_this(const value& _this, const value& p1, const value& p2, const value& p3, const value& p4)  const { value args[4] = { p1,p2,p3,p4 };  return call(4, args, _this); }

        void isolate () {
            ValueIsolate ( this );
        }
         */

    }

}

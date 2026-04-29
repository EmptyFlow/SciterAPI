using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EmptyFlow.SciterAPI.Client.PseudoSom {

	/// <summary>
	/// The model acts as an internal true value source; all properties inside the model can be C# types and to work with Sciter, the conversion happens every time.
	/// </summary>
	/// <typeparam name="T">Type of inner model.</typeparam>
	public class InnerPseudoSomModel<[DynamicallyAccessedMembers ( DynamicallyAccessedMemberTypes.All )] T> : IPseudoSomModel where T : class {
		private string m_unique = "";

		private Dictionary<string, PropertyInfo> m_properties = new Dictionary<string, PropertyInfo> ();

		private Dictionary<string, MethodInfo> m_methods = new Dictionary<string, MethodInfo> ();

		private readonly string m_modelName = "";

		private readonly SciterAPIHost m_sciterAPIHost;

		private readonly T m_model;

		public InnerPseudoSomModel ( T model, string modelName, SciterAPIHost host ) {
			m_modelName = modelName;
			m_sciterAPIHost = host;
			m_model = model;
			var modelType = typeof(T);
			var properties = modelType.GetProperties ( BindingFlags.Public | BindingFlags.Instance );
			foreach ( var property in properties ) {
				m_properties.Add ( MakeCamelCase ( property.Name ), property );
			}

			var methods = modelType.GetMethods ( BindingFlags.Public | BindingFlags.Instance )
				.Where (
					a =>
						a.GetParameters ().Count () == 1 && a.GetParameters ().First ().ParameterType == typeof ( IEnumerable<SciterValue> ) &&
						a.ReturnType == typeof ( SciterValue )
				);
			foreach ( var method in methods ) {
				m_methods.Add ( MakeCamelCase ( method.Name ), method );
			}
		}

		public string Unique => m_unique;

		public string GetModelName () => m_modelName;

		public SciterValue CallMethod ( string name, IEnumerable<SciterValue> parameters ) {
			if ( !m_methods.ContainsKey ( name ) ) return m_sciterAPIHost.NullValue;

			var method = m_methods[name];
			var result = method.Invoke ( m_model, [parameters] );
			if ( result is SciterValue sciterValue ) return sciterValue;

			return m_sciterAPIHost.NullValue;
		}

		public HashSet<string> GetMethods () => m_methods.Keys.ToHashSet ();

		public HashSet<string> GetProperties () => m_properties.Keys.ToHashSet ();

		public SciterValue GetPropetyValue ( string name ) {
			if ( m_properties.TryGetValue ( name, out var property ) ) {
				return m_sciterAPIHost.CreateValue ( property.GetValue ( m_model ) );
			}

			return m_sciterAPIHost.NullValue;
		}

		public bool SetPropetyValue ( SciterValue value, string name ) {
			if ( m_properties.TryGetValue ( name, out var property ) ) {
				var type = property.PropertyType;
				switch ( type ) {
					case Type _ when type == typeof ( string ):
						var sciterString = m_sciterAPIHost.GetValueString ( ref value );
						property.SetValue ( m_model, sciterString );
						break;
					case Type _ when type == typeof ( int ):
						var sciterInt = m_sciterAPIHost.GetValueInt32 ( ref value );
						property.SetValue ( m_model, sciterInt );
						break;
					case Type _ when type == typeof ( double ):
						var sciterDouble = m_sciterAPIHost.GetValueDouble ( ref value );
						property.SetValue ( m_model, sciterDouble );
						break;
					case Type _ when type == typeof ( decimal ):
						var sciterDecimal = m_sciterAPIHost.GetValueDouble ( ref value );
						property.SetValue ( m_model, sciterDecimal );
						break;
					case Type _ when type == typeof ( float ):
						var sciterFloat = m_sciterAPIHost.GetValueDouble ( ref value );
						property.SetValue ( m_model, sciterFloat );
						break;
					case Type _ when type == typeof ( long ):
						var sciterLong = m_sciterAPIHost.GetValueInt64 ( ref value );
						property.SetValue ( m_model, sciterLong );
						break;
					case Type _ when type == typeof ( bool ):
						var sciterBoolean = value.d == 1;
						property.SetValue ( m_model, sciterBoolean );
						break;
					case Type _ when type == typeof ( SciterValue ):
						property.SetValue ( m_model, value );
						break;
					case Type _ when type == typeof ( IEnumerable<int> ):
						var sciterIntArray = m_sciterAPIHost.GetArrayOrMapCount ( ref value );
						var list = new List<int> ();
						for ( int i = 0; i < sciterIntArray; i++ ) {
							var arrayItem = m_sciterAPIHost.GetArrayItem ( ref value, i );
							list.Add ( m_sciterAPIHost.GetValueInt32 ( ref arrayItem ) );
						}
						property.SetValue ( m_model, list );
						break;
					case Type _ when type == typeof ( IEnumerable<long> ):
						var sciterLongArray = m_sciterAPIHost.GetArrayOrMapCount ( ref value );
						var listLong = new List<long> ();
						for ( int i = 0; i < sciterLongArray; i++ ) {
							var arrayItem = m_sciterAPIHost.GetArrayItem ( ref value, i );
							listLong.Add ( m_sciterAPIHost.GetValueInt64 ( ref arrayItem ) );
						}
						property.SetValue ( m_model, listLong );
						break;
					case Type _ when type == typeof ( IEnumerable<double> ):
						var sciterDoubleArray = m_sciterAPIHost.GetArrayOrMapCount ( ref value );
						var listDouble = new List<double> ();
						for ( int i = 0; i < sciterDoubleArray; i++ ) {
							var arrayItem = m_sciterAPIHost.GetArrayItem ( ref value, i );
							listDouble.Add ( m_sciterAPIHost.GetValueDouble ( ref arrayItem ) );
						}
						property.SetValue ( m_model, listDouble );
						break;
					case Type _ when type == typeof ( IEnumerable<float> ):
						var sciterFloatArray = m_sciterAPIHost.GetArrayOrMapCount ( ref value );
						var listFloat = new List<double> ();
						for ( int i = 0; i < sciterFloatArray; i++ ) {
							var arrayItem = m_sciterAPIHost.GetArrayItem ( ref value, i );
							listFloat.Add ( m_sciterAPIHost.GetValueDouble ( ref arrayItem ) );
						}
						property.SetValue ( m_model, listFloat );
						break;
					case Type _ when type == typeof ( IEnumerable<string> ):
						var sciterStringArray = m_sciterAPIHost.GetArrayOrMapCount ( ref value );
						var listString = new List<string> ();
						for ( int i = 0; i < sciterStringArray; i++ ) {
							var arrayItem = m_sciterAPIHost.GetArrayItem ( ref value, i );
							listString.Add ( m_sciterAPIHost.GetValueString ( ref arrayItem ) );
						}
						property.SetValue ( m_model, listString );
						break;
					case Type _ when type == typeof ( IEnumerable<bool> ):
						var sciterBoolArray = m_sciterAPIHost.GetArrayOrMapCount ( ref value );
						var listBool = new List<bool> ();
						for ( int i = 0; i < sciterBoolArray; i++ ) {
							var arrayItem = m_sciterAPIHost.GetArrayItem ( ref value, i );
							listBool.Add ( arrayItem.d == 1 );
						}
						property.SetValue ( m_model, listBool );
						break;
					default: return false;
				}

				return true;
			}

			return false;
		}

		public void SetUnique ( string unique ) => m_unique = unique;

		private string MakeCamelCase ( string name ) {
			var firstChar = name[0].ToString ().ToLowerInvariant ();
			return firstChar + name.Substring ( 1 );
		}

	}

}

using EmptyFlow.SciterAPI.Enums;
using EmptyFlow.SciterAPI.Structs;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

	public partial class SciterAPIHost {

		private Dictionary<ulong, nint> m_activePassports = new Dictionary<ulong, nint> ();

		public ulong GetAtomName ( string name ) => m_basicApi.SciterAtomValue ( name );

		public nint CreateSomPassport (
			string name,
			SomAnyPropertyGetter? getter = default, SomAnyPropertySetter? setter = default,
			SomItemGetter? indexerGetter = default, SomItemSetter? indexerSetter = default, SomItemNext? indexerNext = default,
			SomNameResolver? nameResolver = default,
			IEnumerable<(string name, int countParameters, SomMethod method)>? methods = default,
			IEnumerable<(string name, SomPropertyType type, SomPropertyGetter getter, SomPropertySetter setter)>? virtualProperties = default ) {
			var result = new SomPassport ();
			result.name = GetAtomName ( name );
			result.ItemGetter = nint.Zero;
			result.ItemSetter = nint.Zero;
			result.ItemNext = nint.Zero;
			result.NumberProperties = 0;
			result.NumberMethods = 0;
			result.Methods = nint.Zero;
			result.Properties = nint.Zero;
			result.NameResolver = nint.Zero;
			result.reserved = nint.Zero;

			if ( nameResolver != null ) {
				result.flags |= SomPassportFlags.SOM_HAS_NAME_RESOLVER;
				result.NameResolver = Marshal.GetFunctionPointerForDelegate ( nameResolver );
			}

			if ( getter != null ) result.PropertyGetter = Marshal.GetFunctionPointerForDelegate ( getter );
			if ( setter != null ) result.PropertySetter = Marshal.GetFunctionPointerForDelegate ( setter );

			if ( indexerGetter != null ) result.ItemGetter = Marshal.GetFunctionPointerForDelegate ( indexerGetter );
			if ( indexerSetter != null ) result.ItemSetter = Marshal.GetFunctionPointerForDelegate ( indexerSetter );
			if ( indexerNext != null ) result.ItemNext = Marshal.GetFunctionPointerForDelegate ( indexerNext );

			if ( methods != null && methods.Any () ) {
				result.NumberMethods = (uint) methods.Count ();
				int nintSize = Marshal.SizeOf ( typeof ( IntPtr ) );
				int methodDefinitionSize = Marshal.SizeOf ( typeof ( SomMethodDefinition ) );
				IntPtr nativeArray = Marshal.AllocHGlobal ( nintSize * methods.Count () );
				var iterator = 0;
				foreach ( var methodDefinition in methods ) {
					var definition = new SomMethodDefinition ();
					definition.Name = GetAtomName ( methodDefinition.name );
					definition.Parameters = (uint) methodDefinition.countParameters;
					definition.Function = methodDefinition.method;

					nint methodPointer = Marshal.AllocHGlobal ( methodDefinitionSize );
					Marshal.StructureToPtr ( definition, methodPointer, false );
					Marshal.WriteIntPtr ( nativeArray, iterator * nintSize, methodPointer );
					iterator++;
				}
			}

			if ( virtualProperties != null && virtualProperties.Any () ) {
				result.NumberProperties = (uint) virtualProperties.Count ();
				int nintSize = Marshal.SizeOf ( typeof ( IntPtr ) );
				int propertyDefinitionSize = Marshal.SizeOf ( typeof ( SomPropertyDefinition ) );
				IntPtr nativeArray = Marshal.AllocHGlobal ( nintSize * virtualProperties.Count () );
				var iterator = 0;
				foreach ( var propertyDefinition in virtualProperties ) {
					var definition = new SomPropertyDefinition ();
					definition.Name = GetAtomName ( propertyDefinition.name );
					definition.Type = propertyDefinition.type;
					definition.Accessors = new SomPropertyDefinitionAccessors {
						Getter = propertyDefinition.getter,
						Setter = propertyDefinition.setter,
					};

					nint propertyPointer = Marshal.AllocHGlobal ( propertyDefinitionSize );
					Marshal.StructureToPtr ( definition, propertyPointer, false );
					Marshal.WriteIntPtr ( nativeArray, iterator * nintSize, propertyPointer );
					iterator++;
				}
			}

			var pointer = Marshal.AllocHGlobal ( Marshal.SizeOf<SomPassport> () );

			Marshal.StructureToPtr ( result, pointer, true );

			m_activePassports.Add ( result.name, pointer );

			return pointer;
		}

		public void DeleteSomPassport ( SomPassport passport ) {
			if ( m_activePassports.TryGetValue ( passport.name, out var pointer ) ) {
				Marshal.FreeHGlobal ( pointer );
			}
		}

	}

}

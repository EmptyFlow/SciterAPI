using System.Text;

namespace EmptyFlow.SciterAPI.Client.PseudoSom {

	public static class PseudoSom {

		public static (SciterValue? value, bool handled) Handle ( IPseudoSomModel model, SciterAPIHost host, string method, IEnumerable<SciterValue> parameters ) {
			var (target, name) = GetTarget ( method );

			switch ( target ) {
				case "get":
					return (model.GetPropetyValue ( name ), true);
				case "set":
					if ( !parameters.Any () ) return (host.CreateNullValue (), false);

					var setResult = model.SetPropetyValue ( parameters.FirstOrDefault (), name );
					return (host.CreateValue ( setResult ), true);
				case "call":
					return (model.CallMethod ( name, parameters ), true);
				default: return (host.CreateNullValue (), false);
			}
		}

		public static bool RegisterModel ( IPseudoSomModel model, SciterAPIHost host, nint window, nint element ) {
			if ( string.IsNullOrEmpty ( model.Unique ) ) model.SetUnique ( "model-unique-element-" + Guid.NewGuid ().ToString () );
			var tempId = model.Unique;
			host.SetElementAttribute ( element, tempId, "enabled" );
			var script = new StringBuilder ();
			script.AppendLine ( "(function () {" );

			script.AppendLine ( $"const element = document.querySelector('[{tempId}]')" );
			script.AppendLine ( "const model = Object.create(Object.prototype, {" );

			var properties = model.GetProperties ();
			var methods = model.GetMethods ();
			var hasMethods = methods.Any ();
			var lastProperty = properties.LastOrDefault ();
			var lastMethod = methods.LastOrDefault ();

			foreach ( var property in properties ) {
				script.Append (
					$$"""
					{{property}}: {
						configurable: false,
						enumerable: true,
						get: () => {
							return element.xcall('get_{{property}}');
						},
						set: (value) => {
							element.xcall('set_{{property}}', value);
						}
					}{{( ( lastProperty == property && !hasMethods ) ? "" : "," )}}
					"""
				);
			}
			foreach ( var method in methods ) {
				script.Append (
					$$"""
					{{method}}: {
						writable: false,
						configurable: false,
						enumerable: true,
						value: function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) {
							return element.xcall('call_{{method}}', arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
						}
					}{{( ( lastMethod == method ) ? "" : "," )}}
					"""
				);
			}

			script.AppendLine ( "});" );

			script.AppendLine ( $"element.{model.GetModelName ()} = model;" );

			script.AppendLine ( "})();" );

			host.ExecuteWindowEval ( window, script.ToString (), out var result );

			if ( result.IsErrorString || result.IsObjectError ) {
				Console.WriteLine ( $"PSOM Register Model Error({tempId}): " + host.GetValueString ( ref result ) );
				return false;
			}

			return true;
		}

		private static (string target, string name) GetTarget ( string method ) {
			var parts = method.Split ( '_' );
			var firstPath = parts[0];
			var secondPath = parts[1];

			return (firstPath, secondPath);
		}

	}

}

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
			var tempId = "temporary-element-" + Guid.NewGuid ().ToString ();
			host.SetElementAttribute ( element, tempId, "enabled" );
			var script = new StringBuilder ();
			script.AppendLine ( "const model = Object.create(Object.prototype, {" );

			foreach ( var property in model.GetProperties () ) {
				script.Append (
					$$"""
					{{property}}: {
						configurable: false,
						get() {
							return element.xcall('get_' + name);
						},
						set(value) {
							element.xcall('set_' + name, value);
						}
					}
					"""
				);
			}
			foreach ( var method in model.GetMethods () ) {
				script.Append (
					$$"""
					{{method}}: {
						writable: false,
						configurable: false,
						value: function(...args) {
							element.xcall('call_' + name, args);
						}
					}
					"""
				);
			}

			script.AppendLine ( "});" );
			script.AppendLine ( $"const element = document.querySelector('[{tempId}]')" );
			script.AppendLine ( $"element.{model.GetModelName ()} = model;" );

			host.ExecuteWindowEval ( window, script.ToString (), out var result );
			if ( result.IsErrorString || result.IsObjectError ) {
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

namespace EmptyFlow.SciterAPI.Client.PseudoSom {

	/// <summary>
	/// Single pseudo model event handler.
	/// </summary>
	public class PseudoSomModelHandler : SciterEventHandler {

		readonly IPseudoSomModel m_model;

		public PseudoSomModelHandler ( IPseudoSomModel model, nint relatedThing, nint window, SciterAPIHost sciterAPIHost ) : base ( relatedThing, sciterAPIHost ) {
			m_model = model ?? throw new ArgumentNullException ( nameof ( model ) );
			PseudoSom.RegisterModel ( m_model, sciterAPIHost, window, relatedThing );
		}

		public override EventBehaviourGroups BeforeRegisterEvent () => EventBehaviourGroups.HANDLE_SCRIPTING_METHOD_CALL | EventBehaviourGroups.HANDLE_METHOD_CALL;


		public override (SciterValue? value, bool handled) ScriptMethodCall ( string name, IEnumerable<SciterValue> arguments ) => PseudoSom.Handle ( m_model, Host, name, arguments );

	}

}

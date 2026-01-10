namespace EmptyFlow.SciterAPI.Client {

	/// <summary>
	/// Event handler for element.
	/// </summary>
	public class ElementEventHandler : SciterEventHandler {

		public ElementEventHandler ( nint element, SciterAPIHost host ) : base ( element, host, SciterEventHandlerMode.Element ) {
		}

	}

}

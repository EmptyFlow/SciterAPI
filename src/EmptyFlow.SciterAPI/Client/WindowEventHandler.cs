namespace EmptyFlow.SciterAPI.Client {

	/// <summary>
	/// Event handler for window.
	/// </summary>
	public class WindowEventHandler : SciterEventHandler {

		public WindowEventHandler ( nint window, SciterAPIHost host ) : base ( window, host, SciterEventHandlerMode.Window ) {
		}

	}

}

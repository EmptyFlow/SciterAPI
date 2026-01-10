namespace EmptyFlow.SciterAPI.Client.DeveloperConsole {

	internal class DeveloperConsoleWindowHandler : WindowEventHandler {

		private readonly DeveloperConsole m_developerConsole;

		public DeveloperConsoleWindowHandler ( nint window, SciterAPIHost host, DeveloperConsole developerConsole ) : base ( window, host ) {
			m_developerConsole = developerConsole;
		}

		public override void BehaviourEvent ( BehaviourEvents command, nint targetElement, nint element, nint reason, SciterValue data, string name ) {
			if ( command == BehaviourEvents.DOCUMENT_COMPLETE ) m_developerConsole.RefreshWindowLoadedPath ();
		}

	}

}

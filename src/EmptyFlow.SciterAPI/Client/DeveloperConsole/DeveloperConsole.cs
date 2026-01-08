namespace EmptyFlow.SciterAPI.Client.DeveloperConsole {

	public class DeveloperConsole {

		private DeveloperConsoleWindowHandler m_windowHandler;

		private nint m_consolePointer = nint.Zero;

		public DeveloperConsole ( SciterAPIHost host, nint windowPointer ) {
			m_windowHandler = new DeveloperConsoleWindowHandler ( windowPointer, host );
			host.AddWindowEventHandler ( m_windowHandler, windowPointer );
			var consoleWindowOutputDebug = false;
#if DEBUG
			consoleWindowOutputDebug = true;
#endif
			m_consolePointer = host.CreateWindow ( width: 500, height: 400, debugOutput: consoleWindowOutputDebug );
			host.Callbacks.AddAttachBehaviourFactory (
				"developerconsolehub",
				( element ) => {
					return new DeveloperConsoleHubHandler ( element, host, this );
				}
			);
			host.LoadHtml ( DeveloperConsoleHtml.Content, m_consolePointer );
			host.ShowWindow ( m_consolePointer );
			host.MoveWindow ( m_consolePointer, new SciterWindowPosition ( 100, 100 ) );
			host.SetWindowCaption ( m_consolePointer, "Developer Console" );

			RefreshWindowLoadedPath ();
		}

		public void ReloadPage () {
			var path = m_windowHandler.Host.GetLatestLoadedFilePath ( m_windowHandler.SubscribedElement );
			if ( string.IsNullOrEmpty ( path ) ) return;

			m_windowHandler.Host.LoadFile ( path, m_windowHandler.SubscribedElement );

			RefreshWindowLoadedPath ();
		}

		public bool RefreshWindowLoadedPath () {
			var currentPath = m_windowHandler.Host.GetLatestLoadedFilePath ( m_windowHandler.SubscribedElement ).Replace("\\", "/");
			var script = $$"""handleExternalEvent({type: "window-loaded-path",path: "{{currentPath}}"})""";
			if ( m_windowHandler.Host.ExecuteWindowEval ( m_consolePointer, script, out var result ) ) {
				return true;
			}

			return false;
		}

	}

	internal class DeveloperConsoleHubHandler : ElementEventHandler {

		private DeveloperConsole m_developerConsole;

		public DeveloperConsoleHubHandler ( nint relatedThing, SciterAPIHost host, DeveloperConsole developerConsole ) : base ( relatedThing, host ) {
			m_developerConsole = developerConsole;
		}

		public override EventBehaviourGroups BeforeRegisterEvent () =>
			EventBehaviourGroups.HANDLE_METHOD_CALL |
			EventBehaviourGroups.HANDLE_SCRIPTING_METHOD_CALL;

		public override (SciterValue? value, bool handled) ScriptMethodCall ( string name, IEnumerable<SciterValue> arguments ) {
			if ( name == "reloadpage" ) {
				m_developerConsole.ReloadPage ();
				var resultValue = Host.CreateValue ( true );
				return (resultValue, true);
			}

			return (null, false);
		}

	}

	internal static class DeveloperConsoleHtml {

		public static string Content =
""""
<html>
	<head>
		<style>
			body {
				margin: 0;
				padding: 0;
				background-color: #eef2f7;
			}
			.tabs {
				display: flex;
				flex-direction: row;
				margin-left: 2px;
			}
			.tab-option {
				border-width: 1px;
				border-style: solid;
				border-color: #e7e9eb;
				border-radius: 2px;
				background-color: white;
				height: 30px;
				width: 100px;
				display: flex;
				flex-direction: column;
				margin-right: 2px;
			}
			.tab-option.selected {
				background-color: #1c84c6;
				color: white;
			}
			.tab-page {
				display: none;
			}
			.tab-page-layout {
				display: flex;
				flex-direction: column;
			}
		</style>
	</head>
	<body>
		<div id="hub" style="behavior: developerconsolehub;"></div>
		<div class="tabs">
			<div class="tab-option" id="window-tab">
				<div style="display: flex; flex-direction: row; align-self:center; height: 100%;">
					<span style="align-self: center; height: 18px;">Window</span>
				</div>
			</div>
			<div class="tab-option" id="events-tab">
				<div style="display: flex; flex-direction: row; align-self:center; height: 100%;">
					<span style="align-self: center; height: 18px;">Events</span>
				</div>
			</div>
		</div>
		<div class="tab-page" id="window-page">
			<div class="tab-page-layout">
				<button id="reloadpage">Reload page</button>
				<div>
					Loaded document: <br><span id="window-loaded-path"></span>
				</div>
			</div>
		</div>
		<div class="tab-page" id="events-page">
			<div class="tab-page-layout">
				<span>Events</span>
			</div>
		</div>
		<script>
			const reloadPageButton = document.getElementById('reloadpage');
			const windowTab = document.getElementById('window-tab');
			const eventsTab = document.getElementById('events-tab');
			const windowPage = document.getElementById('window-page');
			const eventsPage = document.getElementById('events-page');
			const windowLoadedPath = document.getElementById('window-loaded-path');
			const tabsPages = [{tab: windowTab, page: windowPage}, {tab: eventsTab, page: eventsPage}];
			let selectedTab = null;
			const hub = document.getElementById('hub');
			reloadPageButton.addEventListener(
				"click",
				function() {
					hub.xcall('reloadpage');
				}
			);
			windowTab.addEventListener('click', selectTabHandler);
			eventsTab.addEventListener('click', selectTabHandler);

			function selectTabHandler(event) {
				selectTab(event.currentTarget);
			}
			function selectTab(newSelectTab) {
				const newItem = tabsPages.find(a => a.tab === newSelectTab);
				if (!newItem) return;

				if (selectedTab) {
					selectedTab.classList.remove('selected');
					const item = tabsPages.find(a => a.tab === selectedTab);
					if (item) item.page.style.display = 'none';
				}
				newSelectTab.classList.add('selected');
				selectedTab = newSelectTab;
				newItem.page.style.display = 'block';
			}

			selectTab(windowTab);

			function handleExternalEvent(event) {
				switch (event.type) {
					case 'window-loaded-path':
						windowLoadedPath.innerText = event.path;
						break;
				}
			}
		</script>
	</body>
</html>
"""";

	}

}

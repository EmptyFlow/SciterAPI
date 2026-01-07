namespace EmptyFlow.SciterAPI.Client.DeveloperConsole {

    public class DeveloperConsole {

        private DeveloperConsoleWindowHandler m_windowHandler;

        private nint m_consolePointer = nint.Zero;

        public DeveloperConsole ( SciterAPIHost host, nint windowPointer ) {
            m_windowHandler = new DeveloperConsoleWindowHandler ( windowPointer, host );
            host.AddWindowEventHandler ( m_windowHandler, windowPointer );
            m_consolePointer = host.CreateWindow ( parent: windowPointer, width: 500, height: 400 );
            host.Callbacks.AddAttachBehaviourFactory (
                "developerconsolehub",
                ( element ) => {
                    return new DeveloperConsoleHubHandler ( element, host, this );
                }
            );
            host.LoadHtml ( DeveloperConsoleHtml.Content, m_consolePointer );
            host.ShowWindow ( m_consolePointer );
            host.MoveWindow ( m_consolePointer, new SciterWindowPosition ( 100, 100 ) );
        }

        public void ReloadPage () {
            var path = m_windowHandler.Host.GetLatestLoadedFilePath ( m_windowHandler.SubscribedElement );
            if ( string.IsNullOrEmpty ( path ) ) return;

            m_windowHandler.Host.LoadFile ( path, m_windowHandler.SubscribedElement );
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
        }
    </style>
    <script>
        const reloadPageButton = document.getElementById('reloadpage');
        const hub = document.getElementById('hub');
        reloadPageButton.addEventListener(
            "click",
            function() {
                const pizda = document.getElementById('hub');
                pizda.xcall('reloadpage');
            }
        );
    </script>
</head>
<body>
<div id="hub" style="behavior: developerconsolehub;"></div>
<button id="reloadpage">Reload page</button>
</body>
</html>
"""";

    }

}

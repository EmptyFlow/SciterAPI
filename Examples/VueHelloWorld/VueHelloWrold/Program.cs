
using EmptyFlow.SciterAPI;

SciterLoader.Initialize ( Environment.CurrentDirectory );
var host = new SciterAPIHost ();
host.LoadAPI ();
host.CreateMainWindow ( 300, 300, enableDebug: true, enableFeature: true );
host.LoadFile ( "home://hello-vue.htm" );
host.Process ();
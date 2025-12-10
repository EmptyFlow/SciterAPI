
using EmptyFlow.SciterAPI;

var host = new SciterAPIHost ( Environment.CurrentDirectory );
host.CreateMainWindow ( 300, 300, enableDebug: true, enableFeature: true );
host.LoadFile ( "home://hello-vue.htm" );
host.Process ();
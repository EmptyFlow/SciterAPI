
using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;

SciterLoader.Initialize ( Environment.CurrentDirectory );
var host = new SciterAPIHost ();
host.LoadAPI ();
host.PrepareGraphicsApi ();
host.PrepareRequestApi ();
var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.1.8/samples/html/details-summary.htm";
host.CreateMainWindow ( 300, 300, enableDebug: true, enableFeature: true );
host.AddWindowEventHandler ( new MyWindowEventHandler ( host.MainWindow, host ) );
host.LoadFile ( path );
host.Process ();


public class MyWindowEventHandler : WindowEventHandler {

    public MyWindowEventHandler ( nint window, SciterAPIHost host ) : base ( window, host ) {
    }

    public override nint SOMEventPassport () {
        return Host.CreateSomPassport ( "testpass" );
    }

}


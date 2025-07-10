
using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;

SciterLoader.Initialize ( Environment.CurrentDirectory );
var host = new SciterAPIHost ();
host.LoadAPI ();
host.PrepareGraphicsApi ();
host.PrepareRequestApi ();
var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.2.1/samples/html/details-summary.htm";
host.Callbacks.AddAttachBehaviourFactory ( "testbehaviour", ( element ) => new TestGraphicsEventHandler ( element, host ) );
host.CreateMainWindow ( 300, 300, enableDebug: true, enableFeature: true );
host.AddWindowEventHandler ( new MyWindowEventHandler ( host.MainWindow, host ) );
host.LoadFile ( path );
host.Process ();


public class MyWindowEventHandler : WindowEventHandler {

    public MyWindowEventHandler ( nint window, SciterAPIHost host ) : base ( window, host ) {
    }

    public override void BehaviourEvent ( BehaviourEvents cmd, nint heTarget, nint he, nint reason, SciterValue data, string name ) {
        if ( cmd == BehaviourEvents.DOCUMENT_READY ) {
            var body = Host.MakeCssSelector ( "body" );
            var firstElement = body.First ();
            var childrens = Host.GetElementChildrens ( firstElement );
            if ( childrens.Any () ) {

            }
        }
    }

    /*public override nint SOMEventPassport () {
        return Host.CreateSomPassport ( "testpass" );
    }*/

}

public class TestGraphicsEventHandler : ElementEventHandler {

    public TestGraphicsEventHandler ( nint element, SciterAPIHost host ) : base ( element, host ) {
    }

    public override void DrawEvent ( DrawEvents command, nint gfx, SciterRectangle area, uint reserved ) {
        var color = Host.Graphics.RGBA ( 0, 204, 0, 255 );
        if ( command == DrawEvents.DRAW_BACKGROUND ) {
            Host.Graphics.gFillColor ( gfx, color );
            Host.Graphics.gRectangle ( gfx, area.Left, area.Top, area.Width, area.Height );
            /*
             ctx.fillStyle = "blue";
ctx.fillRect(10, 10, 100, 100);
             */
        }
    }

}

using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;

SciterLoader.Initialize ( Environment.CurrentDirectory );
var host = new SciterAPIHost ();
host.LoadAPI ();
host.PrepareGraphicsApi ();
host.PrepareRequestApi ();
var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.1.4/samples/html/details-summary.htm";
//var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.1.4/samples/vue/hello-vue.htm";
//var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.1.4/samples.sciter/audio/test-basic.htm";
//var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.1.4/samples.webgl/basic/1-cube-lights/index.htm";
//var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.1.4/samples.webgl/threejs/1-hello-world.htm";
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
            //var pizdaColor = Host.Graphics.RGBA ( 0, 0, 0, 255 );

            var elements = Host.MakeCssSelector ( "#test" );
            var appDiv = elements.First ();
            Host.ForceRenderElement ( appDiv );
            /*
            Host.SetElementHtml ( appDiv, "<b>Bold Text!!!!!!!!!!!!!!</b>", SetElementHtml.ReplaceContent );*/
        }
    }

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
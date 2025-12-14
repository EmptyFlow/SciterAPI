
using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;
using EmptyFlow.SciterAPI.Client.Models;
using EmptyFlow.SciterAPI.Enums;
using System.Numerics;

// These example developer used for experiment with some new features
// please use another examples in folder Examples. Thanks!

var pathToSciter = "C:/IDEs/sciter/sciter-js-sdk-6.0.3.2";

var host = new SciterAPIHost ( Path.Combine ( pathToSciter, "bin/windows/x64" ), true, true );
var path = "file://" + Path.Combine ( pathToSciter, "samples/html/details-summary.htm" );
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

}

public class TestGraphicsEventHandler : ElementEventHandler {

    public TestGraphicsEventHandler ( nint element, SciterAPIHost host ) : base ( element, host ) {
        Text = Host.GraphicsCreateTextForElement ( element, "test text!!!111", "test-class" );
        Text2 = Host.GraphicsCreateTextForElementWithStyle ( element, "test text!!!111", "font-size: 18px;color: green;" );
        Color1 = Host.GraphicsGetColor ( 0, 204, 0 );
        Color2 = Host.GraphicsGetColor ( 0, 0, 255 );
    }

    public uint Color1 { get; set; }

    public uint Color2 { get; set; }

    public GraphicsTextModel Text { get; set; }

    public GraphicsTextModel Text2 { get; set; }

    public override void DrawEvent ( DrawEvents command, nint gfx, SciterRectangle area, uint reserved ) {
        if ( command == DrawEvents.DRAW_CONTENT ) {
            Host.GraphicsSaveState ( gfx );
            Host.GraphicsFillColor ( gfx, Color1 );
            Host.GraphicsDrawRectangle ( gfx, area.Left, area.Top, area.Left + area.Width, area.Top + area.Height );
            Host.GraphicsDrawLine ( gfx, area.LeftTopCorner, area.RightBottomCorner, Color2, 10 );
            Host.GraphicsDrawText ( gfx, Text, new Vector2 ( area.Left, area.Top ), SciterTextPosition.TopLeft );
            Host.GraphicsDrawText ( gfx, Text2, new Vector2 ( area.Left, area.Top + 30 ), SciterTextPosition.TopLeft );
            Host.GraphicsRestoreState ( gfx );
        }
    }

}
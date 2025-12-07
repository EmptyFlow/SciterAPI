
using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;
using EmptyFlow.SciterAPI.Client.Models;
using EmptyFlow.SciterAPI.Enums;
using System.Numerics;

var pathToSciter = "C:/IDEs/sciter/sciter-js-sdk-6.0.2.30";

var host = new SciterAPIHost ( Path.Combine ( pathToSciter, "bin/windows/x64" ), true, true );
var path = "file://" + Path.Combine ( pathToSciter, "samples/html/details-summary.htm" );
host.Callbacks.AddAttachBehaviourFactory ( "testbehaviour", ( element ) => new TestGraphicsEventHandler ( element, host ) );
host.CreateMainWindow ( 0, 0, enableDebug: true, enableFeature: true );
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
    }

    public GraphicsTextModel? Text { get; set; }

    public GraphicsTextModel? Text2 { get; set; }

    public override void DrawEvent ( DrawEvents command, nint gfx, SciterRectangle area, uint reserved ) {
        var color = Host.Graphics.RGBA ( 0, 204, 0, 255 );
        var blue = Host.Graphics.RGBA ( 0, 0, 255, 255 );
        if ( Text == null ) Text = Host.GraphicsCreateTextForElement ( gfx, ProcessedElement, "test text!!!111", "test-class" );
        if ( Text2 == null ) Text2 = Host.GraphicsCreateTextForElementWithStyle ( gfx, ProcessedElement, "test text!!!111", "font-size: 18px;color: green;" );
        if ( command == DrawEvents.DRAW_CONTENT ) {
            Host.GraphicsSaveState ( gfx );
            Host.GraphicsFillColor ( gfx, color );
            Host.GraphicsDrawRectangle ( gfx, area.Left, area.Top, area.Left + area.Width, area.Top + area.Height );
            Host.GraphicsDrawLine ( gfx, area.LeftTopCorner, area.RightBottomCorner, blue, 10 );
            Host.GraphicsDrawText ( gfx, Text, new Vector2 ( area.Left, area.Top ), SciterTextPosition.TopLeft );
            Host.GraphicsDrawText ( gfx, Text2, new Vector2 ( area.Left, area.Top + 30 ), SciterTextPosition.TopLeft );
            Host.GraphicsRestoreState ( gfx );
        }
    }

}
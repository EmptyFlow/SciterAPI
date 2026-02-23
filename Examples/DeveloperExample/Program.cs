
using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;
using EmptyFlow.SciterAPI.Client.Models;
using EmptyFlow.SciterAPI.Client.PseudoSom;
using EmptyFlow.SciterAPI.Enums;
using System.Numerics;

// These example developer used for experiment with some new features
// please use another examples in folder Examples. Thanks!

var pathToSciter = "C:/IDEs/sciter/sciter-js-sdk-6.0.3.9";

var host = new SciterAPIHost ( Path.Combine ( pathToSciter, "bin/windows/x64" ), true, true );
var path = "file://" + Path.Combine ( pathToSciter, "samples/html/details-summary.htm" );
//host.Callbacks.AddAttachBehaviourFactory ( "testbehaviour", ( element ) => new TestGraphicsEventHandler ( element, host ) );
host.Callbacks.AddAttachBehaviourFactory ( "psom", ( element ) => new PseudoSomHandler ( element, host ) );
host.EnableDebugMode ();
host.EnableFeatures ( SciterRuntimeFeatures.ALLOW_SOCKET_IO );
host.CreateWindow ( asMain: true, debugOutput: true );
host.AddWindowEventHandler ( new MyWindowEventHandler ( host.MainWindow, host ) );
host.LoadFile ( path );
host.Process ();


public class MyWindowEventHandler : WindowEventHandler {

	public MyWindowEventHandler ( nint window, SciterAPIHost host ) : base ( window, host ) {
	}

	public override void BehaviourEvent ( BehaviourEvents cmd, nint heTarget, nint he, nint reason, SciterValue data, string name ) {
		if ( cmd == BehaviourEvents.DOCUMENT_READY ) {
			var value = Host.GetWindowParent ( Host.MainWindow );
			if ( value == 0 ) {

			}
		}
	}

}

public class TestModel : IPseudoSomModel {

	private readonly SciterAPIHost m_host;

	public TestModel ( SciterAPIHost host ) {
		m_host = host;
	}

	public SciterValue CallMethod ( string name, IEnumerable<SciterValue> parameters ) {
		return m_host.CreateNullValue ();
	}

	public HashSet<string> GetMethods () {
		return [];
	}

	public string GetModelName () => "mymodel";

	public HashSet<string> GetProperties () => ["counter"];

	public int Counter { get; set; } = 5;

	public SciterValue GetPropetyValue ( string name ) {
		return name switch {
			"counter" => m_host.CreateValue ( Counter ),
			_ => m_host.CreateNullValue ()
		};
	}

	public bool SetPropetyValue ( SciterValue value, string name ) {
		if ( name == "counter" ) {
			Counter = m_host.GetValueInt32 ( ref value );
			return true;
		}

		return false;
	}
}

public class PseudoSomHandler : ElementEventHandler {

	readonly TestModel _model;

	public PseudoSomHandler ( nint element, SciterAPIHost host ) : base ( element, host ) {
		_model = new TestModel ( host );
	}

	public override void AfterRegisterEvent () {
		PseudoSom.RegisterModel ( _model, Host, Host.MainWindow, m_subscribedElement );
	}

	public override (SciterValue? value, bool handled) ScriptMethodCall ( string name, IEnumerable<SciterValue> arguments ) {
		return PseudoSom.Handle ( _model, Host, name, arguments );
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
using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;

var host = new SciterAPIHost ( Environment.CurrentDirectory );
host.EnableDebugMode ();
host.CreateWindow ( 500, 500, asMain: true );
host.AddWindowEventHandler ( new Test1Handler ( host.MainWindow, host ) );
host.LoadFile ( "home://calljavascript.htm" );
host.Process ();

public class Test1Handler : ElementEventHandler {

	public Test1Handler ( nint element, SciterAPIHost host ) : base ( element, host ) {
	}

	public override EventBehaviourGroups BeforeRegisterEvent () => EventBehaviourGroups.HANDLE_BEHAVIOR_EVENT;

	public override void BehaviourEvent ( BehaviourEvents command, nint targetElement, nint element, nint reason, SciterValue data, string name ) {
		if ( command == BehaviourEvents.DOCUMENT_COMPLETE ) {
			// evaluate JavaScript in window context (in this case set new value to global variable)
			Host.ExecuteWindowEval ( Host.MainWindow, "globalValue2 = 5;", out _ );

			// call JavaScript function defined gin global scope in window
			var numberValue = Host.CreateValue ( 10 );
			Host.ExecuteWindowFunction ( Host.MainWindow, "changeValue", [numberValue], out var result );
			if ( result.IsBoolean && result.d == 1 ) Console.WriteLine ( "Completed!" );
		}
	}

}
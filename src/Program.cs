
using EmptyFlow.SciterAPI;

SciterLoader.Initialize ( Environment.CurrentDirectory );
var host = new SciterAPIHost ();
host.LoadAPI ();
var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.1.2/samples/vue/hello-vue.htm";
//var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.1.1/samples.sciter/audio/test-basic.htm";
//var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.0.4/samples.webgl/basic/1-cube-lights/index.htm";
//var path = "file://C:/IDEs/sciter/sciter-js-sdk-6.0.0.4/samples.webgl/threejs/1-hello-world.htm";
host.CreateMainWindow ( 300, 300, enableDebug: true, enableFeature: true );
host.AddWindowEventHandler ( new MyWindowEventHandler ( host ) );
host.LoadFile ( path );
host.Process ();


public class MyWindowEventHandler : SciterEventHandler {

    public MyWindowEventHandler ( SciterAPIHost host ) : base ( host.MainWindow, host, SciterEventHandlerMode.Window ) {
    }

    public override void BehaviourEvent ( BehaviourEvents cmd, nint heTarget, nint he, nint reason, SciterValue data, string name ) {
        if ( cmd == BehaviourEvents.DOCUMENT_READY ) {
            var elements = Host.MakeCssSelector ( "#app" );
            var appDiv = elements.First ();
            Host.SetElementHtml ( appDiv, "<b>Bold Text!!!!!!!!!!!!!!</b>", SetElementHtml.SIH_REPLACE_CONTENT );
        }
    }

    public override void MouseEvent ( MouseEvents command, SciterPoint elementRelated, SciterPoint ViewRelated, KeyboardStates keyboardStates, DraggingType draggingMode, CursorType cursorType, nint target, nint dragging, bool is_on_icon, uint button_state ) {
        if ( command == MouseEvents.MOUSE_CLICK ) {
            Console.WriteLine ( "Mouse Event!!!!" );
        }
    }

}

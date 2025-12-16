using EmptyFlow.SciterAPI;
using EmptyFlow.SciterAPI.Client;

var host = new SciterAPIHost ( Environment.CurrentDirectory );
host.CreateMainWindow ( 500, 500, enableDebug: true, enableFeature: true );
host.Callbacks.AddAttachBehaviourFactory ( "test1", ( element ) => new Test1Handler ( element, host ) );
host.Callbacks.AddAttachBehaviourFactory ( "test2", ( element ) => new Test2Handler ( element, host ) );
host.LoadFile ( "home://xcall.htm" );
host.Process ();

public class Test1Handler : ElementEventHandler {

    public Test1Handler ( nint element, SciterAPIHost host ) : base ( element, host ) {
    }

    public override (SciterValue? value, bool handled) ScriptMethodCall ( string name, IEnumerable<SciterValue> arguments ) {
        if ( name == "testMethod" ) return (TestMethod ( arguments.ElementAt ( 0 ), arguments.ElementAt ( 1 ), arguments.ElementAt ( 2 ) ), true);

        return (null, false);
    }

    public SciterValue TestMethod ( SciterValue boolean, SciterValue array, SciterValue stringValue ) {
        var boolValue = boolean.d == 1 ? "true" : "false";

        var arrayItems = new List<int> ();
        var count = Host.GetArrayOrMapCount ( ref array );
        for ( var i = 0; i < count; i++ ) {
            var element = Host.GetArrayItem ( ref array, i );
            arrayItems.Add ( Host.GetValueInt32 ( ref element ) );
        }

        var stringData = Host.GetValueString ( ref stringValue );

        return Host.CreateValue ( $"{boolValue}{string.Join ( ' ', arrayItems )}{stringData}" );
    }

}

public class Test2Handler : ElementEventHandler {

    public Test2Handler ( nint element, SciterAPIHost host ) : base ( element, host ) {
    }

    public override (SciterValue? value, bool handled) ScriptMethodCall ( string name, IEnumerable<SciterValue> arguments ) {
        if ( name == "anotherMethod" ) return (AnotherMethod ( arguments.ElementAt ( 0 ) ), true);

        return (null, false);
    }

    public SciterValue AnotherMethod ( SciterValue value ) {
        var argusValue = Host.GetMapItem ( ref value, "argus" );
        var argus = Host.GetValueInt32( ref argusValue );

        var burgusValue = Host.GetMapItem ( ref value, "burgus" );
        var burgus = Host.GetValueInt32 ( ref burgusValue );

        return Host.CreateValue ( $"{argus} {burgus}" );
    }

}
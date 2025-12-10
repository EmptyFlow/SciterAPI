
using EmptyFlow.SciterAPI;
using System.Text;

var host = new SciterAPIHost ( Environment.CurrentDirectory );
host.CreateMainWindow ( 500, 500, enableDebug: true, enableFeature: true );
host.Callbacks.AddProtocolHandler (
  "http://", // we will be capture http protocol
  (
  path => {
      if ( path != "http://data.json" ) return new byte[0]; // we handle only one address, others will be handled as usual

      return Encoding.UTF8.GetBytes (
""""
{
"items": [
    {"id": 20},
    {"id": 30}
]
}
""""
      );
  }
) );
host.LoadFile ( "home://eventProtocol.htm" );
host.Process ();
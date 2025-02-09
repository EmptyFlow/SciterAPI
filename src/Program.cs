
using SciterLibraryAPI;

SciterLoader.Initialize ( @"C:\IDEs\sciter\sciter-js-sdk-6.0.0.1\sciter-js-sdk-6.0.0.1\bin\windows\x64" );
var host = new SciterAPIHost ();
host.LoadAPI ();
host.CreateMainWindow ( 300, 300, enableDebug: true, enableFeature: true );
host.AddWindowEventHandler (new SciterEventHandler());
host.WindowsProcess ();

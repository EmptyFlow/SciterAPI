
using SciterLibraryAPI;

SciterLoader.Initialize ( @"C:\IDEs\sciter\sciter-js-sdk-6.0.0.1\sciter-js-sdk-6.0.0.1\bin\windows\x64" );
var host = new SciterAPIHost ();
host.LoadAPI ();
host.CreateMainWindow ( "file://C:/IDEs/sciter/sciter-js-sdk-6.0.0.1/sciter-js-sdk-6.0.0.1/samples/vue/hello-vue.htm", 300, 300, enableDebug: true, enableFeature: true );
host.AddWindowEventHandler (new SciterEventHandler());
Task.Run (
    async () => {
        await Task.Delay ( 2000 );

        var elements = host.MakeCssSelector ( "#app" );
        var appDiv = elements.First ();
        host.SetElementHtml ( appDiv, "<b>Bold Text!!!!!!!!!!!!!!</b>", SetElementHtml.SIH_REPLACE_CONTENT );
    }
);
host.Process ();

 [![nugeticon](https://img.shields.io/badge/nuget-available-blue)](https://www.nuget.org/packages/EmptyFlow.SciterAPI)

# SciterAPI
C# cross platform binding and low-level helpers for Sciter HTML/CSS/JS rendering library. You can check out more about Sciter [here](https://sciter.com/). 
Library support net8+, also trimming and compilation to NativeAot.

### Install instruction

```shell
Install-Package EmptyFlow.SciterAPI
```
To download sciter you need to open [gitlab](https://gitlab.com/sciter-engine/sciter-js-sdk/-/releases)

### Getting started

```csharp
using EmptyFlow.SciterAPI;

SciterLoader.Initialize ( Environment.CurrentDirectory ); // you need specify folder where will be located sciter library file (sciter.dll/libsciter.so/libsciter.dylib)
var host = new SciterAPIHost (); // create host
host.LoadAPI (); // load sciter API and make adjustments for further work
host.CreateMainWindow ( 300, 300, enableDebug: true, enableFeature: true ); // create main window and enable debug mode and sciter features (like access to system in JavaScript)
host.AddWindowEventHandler ( new MyWindowEventHandler ( host ) ); // create and register window Event Handler (via event handler you can handle events from windows or elements)
host.LoadFile ( "file://path/my.html" ); // load HTML page, path specified in first argument
host.Process (); // start sciter and run main loop for show main window

public class MyWindowEventHandler : SciterEventHandler {

    public MyWindowEventHandler ( SciterAPIHost host ) : base ( host.MainWindow, host, SciterEventHandlerMode.Window ) { // define event handler with mode Window (which mean events will be handled from all elements on page)
    }

    public override void BehaviourEvent ( BehaviourEvents cmd, nint heTarget, nint he, nint reason, SciterValue data, string name ) { // handle behaviour events
        if ( cmd == BehaviourEvents.DOCUMENT_READY ) { // if document become ready, which mean it fully loaded
            var appDiv = Host.MakeCssSelector ( "#app" ).First (); // find tag with attribute id=app
            Host.SetElementHtml ( appDiv, "<b>Bold Text!!!!!!!!!!!!!!</b>", SetElementHtml.SIH_REPLACE_CONTENT ); // change html in these tag
        }
    }
}
```

### Documentation

Now in progress and will be shortly

### Roadmap

* ~~Binding for core API~~
* ~~Create main window and start process~~
* ~~Behaviour Event Handler Factory (inject `behavior: name` from css or style attribute)~~
* ~~Add custom file protocol~~
* ~~Create/Delete Event Handler~~
* ~~Element(s) selector~~
* ~~Get/Set Element Text/Html~~
* ~~Event Handler as class with overrided methods~~
* ~~Get/Set Element Attributes~~
* Helpers for Sciter Node API
* Get/Set Variables
* Helpers for Scriter Value API
* Binding for Graphics API
* Binding for Request API
* Support SOM mapping

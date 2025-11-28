 [![nugeticon](https://img.shields.io/badge/nuget-available-blue)](https://www.nuget.org/packages/EmptyFlow.SciterAPI)
 [![CI and Tests](https://github.com/EmptyFlow/SciterAPI/actions/workflows/dotnet.yml/badge.svg)](https://github.com/EmptyFlow/SciterAPI/actions/workflows/dotnet.yml)
 [![nugeticon](https://img.shields.io/nuget/dt/emptyflow.sciterapi)](https://www.nuget.org/packages/EmptyFlow.SciterAPI)

# SciterAPI
C# cross platform binding and low-level helpers for Sciter HTML/CSS/JS rendering library. You can check out more about Sciter [here](https://sciter.com/). 
Library support net8+, also trimming and compilation to NativeAot. Supported Sciter from version 6.0.0.0+.

### Install instruction

```shell
Install-Package EmptyFlow.SciterAPI
```
To download sciter you need to open [gitlab](https://gitlab.com/sciter-engine/sciter-js-sdk/-/releases)

Important! Don't forget to select specific build and debug architecture, `Any CPU` is not allowed in this case. Also you need to select the corresponding sciter library, and it should be of the same architecture as in your project (if you select x64, it means you need to load sciter library from x64 folder).

### Getting started

```csharp
using EmptyFlow.SciterAPI;

var sciterFolder = Environment.CurrentDirectory; // you need specify folder where will be located sciter library file (sciter.dll/libsciter.so/libsciter.dylib)
var host = new SciterAPIHost ( sciterFolder ); // create host and load API
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

Latest version documentation can be found [there](https://emptyflow.github.io/SciterAPIDocs/).

### Table compatibility

In most cases, the old SciterAPI will be compatible with the new Sciter, but the new version of SciterAPI may not be compatible with old Sciter version.
You can check the table below to see which version of SciterAPI is compatible with Sciter.

| Minimal SciterAPI version | Minimal Sciter version |
| -------- | ------- |
| 1.0.4 | 6.0.1.8 |
| 1.0.0 | 6.0.0.0 |

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
* ~~Helpers for Scriter Value API~~
* ~~Binding for Graphics API~~
* ~~Binding for Request API~~
* ~~Get/Set Variables~~
* Support SOM mapping
* Helpers for Sciter Node API
* Helpers for Graphics API

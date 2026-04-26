 [![nugeticon](https://img.shields.io/badge/nuget-available-blue)](https://www.nuget.org/packages/EmptyFlow.SciterAPI)
 [![docslink](https://img.shields.io/badge/docs-latest-blue)](https://emptyflow.github.io/SciterAPIDocs/)
 [![CI and Tests](https://github.com/EmptyFlow/SciterAPI/actions/workflows/dotnet.yml/badge.svg)](https://github.com/EmptyFlow/SciterAPI/actions/workflows/dotnet.yml)
 [![nugeticon](https://img.shields.io/nuget/dt/emptyflow.sciterapi)](https://www.nuget.org/packages/EmptyFlow.SciterAPI)

# SciterAPI
C# cross platform binding and low-level helpers for Sciter HTML/CSS/JS rendering library. You can check out more about Sciter [here](https://sciter.com/). 
Library support net8+, also trimming and compilation to NativeAot. Supported Sciter from version 6.0.0.0+ (also known as SciterJS). Package is production ready.

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
host.EnableDebugMode(); // enable debug mode
host.EnableFeatures(); // enable all feature by default, or you can pass you set via parameter
host.CreateWindow ( asMain: true ); // create window (asMain mean pointer will be store in host.MainWindow property, instead you need to store it youself via returned variable)
host.AddWindowEventHandler ( new SciterEventHandler ( host.MainWindow, host ) ); // create and register window Event Handler (via event handler you can handle events from windows or elements)
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

### Production Ready and Current Progress

SciterAPI from version 1.2.7 can be used in production ready environment. Most of the library APIs have reached their final form, if something will be changes in future it will be introduced as new APIs.
Some of parts in progress:
- Graphics API implemented on 45%
- Request API not implemented (will be after Graphics API)
  
Some of parts not planned:
- Archive API, I don't think it required when .NET have it own way to store files inside Assemblies.

﻿using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Text;

[assembly: CollectionBehavior ( CollectionBehavior.CollectionPerClass, DisableTestParallelization = true )]

namespace EmptyFlow.SciterAPI.Tests {

    public class SciterAPIHostIntegrationTests : IClassFixture<DownloadLibraryFixture> {

        [Fact, Trait ( "Category", "Integration" )]
        public void InitializeLibraryAndLoadAPI () {
            //Act and Assert
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_MakeCssSelector () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            var path = Path.Combine ( Environment.CurrentDirectory, "SciterAPIHost_Completed_MakeCssSelector.html" );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( path );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var elements = host.MakeCssSelector ( "#textspan" );
                Assert.Single ( elements );
                host.CloseMainWindow ();
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_MakeCssSelector_FewItems () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            var path = Path.Combine ( Environment.CurrentDirectory, "SciterAPIHost_Completed_MakeCssSelectorFewItems.html" );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( path );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var elements = host.MakeCssSelector ( "span.item1" );
                Assert.Equal ( 3, elements.Count () );
                host.CloseMainWindow ();
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_GetElementText_FewItems () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            var path = Path.Combine ( Environment.CurrentDirectory, "SciterAPIHost_Completed_MakeCssSelector.html" );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( path );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );
            var result = "";

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "#textspan" ).First ();
                result = host.GetElementText ( element );
                host.CloseMainWindow ();
                Assert.Equal ( "Lalalala", result );
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_SetElementText_FewItems () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            var path = Path.Combine ( Environment.CurrentDirectory, "SciterAPIHost_Completed_MakeCssSelector.html" );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( path );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );
            var result = "";

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "#textspan" ).First ();
                host.SetElementText ( element, "new value11111" );
                result = host.GetElementText ( element );
                host.CloseMainWindow ();
                Assert.Equal ( "new value11111", result );
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_GetElementHtml_WithoutOuter () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            var path = Path.Combine ( Environment.CurrentDirectory, "SciterAPIHost_Completed_MakeCssSelector.html" );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( path );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "body" ).First ();
                var html = host.GetElementHtml ( element, false );
                host.CloseMainWindow ();
                Assert.Equal ( "\n    <span id=\"textspan\">Lalalala</span>\n", html );
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_GetElementHtml_WithOuter () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            var path = Path.Combine ( Environment.CurrentDirectory, "SciterAPIHost_Completed_MakeCssSelector.html" );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( path );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "body" ).First ();
                var html = host.GetElementHtml ( element, true );
                host.CloseMainWindow ();
                Assert.Equal ( "<body>\n    <span id=\"textspan\">Lalalala</span>\n</body>", html );
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_SetElementHtml_ReplaceContent () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            var path = Path.Combine ( Environment.CurrentDirectory, "SciterAPIHost_Completed_MakeCssSelector.html" );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( path );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "#textspan" ).First ();
                host.SetElementHtml ( element, "<b>Bold</b><i>Italic</i>", SetElementHtml.SIH_REPLACE_CONTENT );
                var html = host.GetElementHtml ( element, false );
                host.CloseMainWindow ();
                Assert.Equal ( "<b>Bold</b><i>Italic</i>", html );
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_Callbacks_RegisterProtocolHandler () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            host.CreateMainWindow ( 300, 300 );
            host.Callbacks.AddProtocolHandler (
                "embedded://",
                (
                    path => {
                        return Encoding.UTF8.GetBytes (
""""
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<html>
    <body>
        <span id="world">Hello world!!!</span>
    </body>
</html>

""""
                        );
                    }
                )
            );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( "embedded://test.html" );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                Assert.Single ( host.MakeCssSelector ( "#world" ) );
                host.CloseMainWindow ();
            }
        }

        public class SciterAPIHost_Completed_Callbacks_AttachCustomBehaviorHit ( IntPtr element, SciterAPIHost host ) : SciterEventHandler ( element, host, SciterEventHandlerMode.Element ) {

            public Action? AfterRegisterEventFired { get; set; }

            public override void AfterRegisterEvent () {
                AfterRegisterEventFired?.Invoke ();
            }

        }

        [Fact, Trait ( "Category", "Integration" )]
        public async Task SciterAPIHost_Completed_Callbacks_AttachCustomBehavior () {
            //Arrange
            var handlerFired = false;
            SciterAPIHost_Completed_Callbacks_AttachCustomBehaviorHit newSciterEventHandler;
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            host.CreateMainWindow ( 300, 300 );
            host.Callbacks.AddProtocolHandler (
                "embedded://",
                ( path ) => {
                    return Encoding.UTF8.GetBytes (
""""
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<html>
    <body>
        <span id="world" style="behavior: 'testcustombehaviour';">Hello world!!!</span>
    </body>
</html>

"""" );
                }
                    );
            host.Callbacks.AttachBehaviourAction = ( name, element ) => {
                newSciterEventHandler = new SciterAPIHost_Completed_Callbacks_AttachCustomBehaviorHit ( element, host );
                newSciterEventHandler.AfterRegisterEventFired = () => {
                    handlerFired = true;
                    host.CloseMainWindow ();
                };
                return newSciterEventHandler;
            };
            host.LoadFile ( "embedded://test.html" );

            //Act
            host.Process ();

            await Task.Delay ( 2000 );
            host.CloseMainWindow ();

            //Assert
            Assert.True ( handlerFired );
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_GetElementAttribute_Exists () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            host.CreateMainWindow ( 300, 300 );
            host.Callbacks.AddProtocolHandler (
                "embedded://",
                (
                    path => {
                        return Encoding.UTF8.GetBytes (
""""
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<html>
    <body>
        <span id="world">Hello world!!!</span>
    </body>
</html>

""""
                        );
                    }
                )
            );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( "embedded://test.html" );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "#world" ).Single ();
                var id = host.GetElementAttribute ( element, "id" );
                Assert.Equal ( "world", id );
                host.CloseMainWindow ();
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_GetElementAttribute_NotExists () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            host.CreateMainWindow ( 300, 300 );
            host.Callbacks.AddProtocolHandler (
                "embedded://",
                (
                    path => {
                        return Encoding.UTF8.GetBytes (
""""
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<html>
    <body>
        <span id="world">Hello world!!!</span>
    </body>
</html>

""""
                        );
                    }
                )
            );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( "embedded://test.html" );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "#world" ).Single ();
                var value = host.GetElementAttribute ( element, "notexists" );
                Assert.Empty ( value );
                host.CloseMainWindow ();
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_SetElementAttribute () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            host.CreateMainWindow ( 300, 300 );
            host.Callbacks.AddProtocolHandler (
                "embedded://",
                (
                    path => {
                        return Encoding.UTF8.GetBytes (
""""
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<html>
    <body>
        <span id="world" test="bruher">Hello world!!!</span>
    </body>
</html>

""""
                        );
                    }
                )
            );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( "embedded://test.html" );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "#world" ).Single ();
                var value = host.GetElementAttribute ( element, "test" );
                Assert.Equal ( "bruher", value );
                host.SetElementAttribute ( element, "test", "muher" );
                var newValue = host.GetElementAttribute ( element, "test" );
                Assert.Equal ( "muher", newValue );
                host.CloseMainWindow ();
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_GetElementAttributeNames () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            host.CreateMainWindow ( 300, 300 );
            host.Callbacks.AddProtocolHandler (
                "embedded://",
                (
                    path => {
                        return Encoding.UTF8.GetBytes (
""""
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<html>
    <body>
        <span id="world" attribute1 attribute2="2" attribute3 attribute4="4" attribute5>Hello world!!!</span>
    </body>
</html>

""""
                        );
                    }
                )
            );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( "embedded://test.html" );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "#world" ).Single ();
                var names = host.GetElementAttributeNames ( element );
                host.CloseMainWindow ();
                Assert.Equal ( 6, names.Count () );
                Assert.Equal ( "id", names.ElementAt ( 0 ) );
                Assert.Equal ( "attribute1", names.ElementAt ( 1 ) );
                Assert.Equal ( "attribute2", names.ElementAt ( 2 ) );
                Assert.Equal ( "attribute3", names.ElementAt ( 3 ) );
                Assert.Equal ( "attribute4", names.ElementAt ( 4 ) );
                Assert.Equal ( "attribute5", names.ElementAt ( 5 ) );
            }
        }

        [Fact, Trait ( "Category", "Integration" )]
        public void SciterAPIHost_Completed_GetElementAttributes () {
            //Arrange
            SciterLoader.Initialize ( "" );
            var host = new SciterAPIHost ();
            host.LoadAPI ();
            host.CreateMainWindow ( 300, 300 );
            host.Callbacks.AddProtocolHandler (
                "embedded://",
                (
                    path => {
                        return Encoding.UTF8.GetBytes (
""""
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<html>
    <body>
        <span id="world" attribute1 attribute2="2" attribute3 attribute4="4" attribute5>Hello world!!!</span>
    </body>
</html>

""""
                        );
                    }
                )
            );
            host.CreateMainWindow ( 300, 300 );
            host.LoadFile ( "embedded://test.html" );
            host.AddWindowEventHandler ( new DocumentReadyHandler ( ProcessCompleted, host ) );

            //Act
            host.Process ();

            //Assert
            void ProcessCompleted () {
                var element = host.MakeCssSelector ( "#world" ).Single ();
                var attributes = host.GetElementAttributes ( element );
                host.CloseMainWindow ();
                Assert.Equal ( 6, attributes.Count () );
                Assert.Equal ( "id", attributes.ElementAt ( 0 ).Key );
                Assert.Equal ( "world", attributes.ElementAt ( 0 ).Value );

                Assert.Equal ( "attribute1", attributes.ElementAt ( 1 ).Key );
                Assert.Equal ( "", attributes.ElementAt ( 1 ).Value );

                Assert.Equal ( "attribute2", attributes.ElementAt ( 2 ).Key );
                Assert.Equal ( "2", attributes.ElementAt ( 2 ).Value );

                Assert.Equal ( "attribute3", attributes.ElementAt ( 3 ).Key );
                Assert.Equal ( "", attributes.ElementAt ( 3 ).Value );

                Assert.Equal ( "attribute4", attributes.ElementAt ( 4 ).Key );
                Assert.Equal ( "4", attributes.ElementAt ( 4 ).Value );

                Assert.Equal ( "attribute5", attributes.ElementAt ( 5 ).Key );
                Assert.Equal ( "", attributes.ElementAt ( 5 ).Value );
            }
        }

    }

    public class DocumentReadyHandler : SciterEventHandler {

        private Action m_handler;

        public DocumentReadyHandler ( Action handler, SciterAPIHost host ) : base ( IntPtr.Zero, host, SciterEventHandlerMode.Window ) {
            m_handler = handler;
        }

        public override void BehaviourEvent ( BehaviourEvents cmd, nint heTarget, nint he, nint reason, SciterValue data, string name ) {
            if ( cmd == BehaviourEvents.DOCUMENT_READY ) {
                m_handler ();
            }
        }

    }

    public class DownloadLibraryFixture : IDisposable {

        public DownloadLibraryFixture () {
            if ( File.Exists ( "sciter.zip" ) ) return;

            var os = "linux";
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.Windows ) ) {
                os = "win";
            }
            if ( RuntimeInformation.IsOSPlatform ( OSPlatform.OSX ) ) {
                os = "mac";
            }

            var arch = "";
            var architecture = RuntimeInformation.ProcessArchitecture;
            switch ( architecture ) {
                case Architecture.X86:
                    arch = "x32";
                    break;
                case Architecture.X64:
                    arch = "x64";
                    break;
                case Architecture.Arm64:
                    arch = "arm64";
                    break;
                default: throw new NotSupportedException ( "Machine architecture not supported!" );
            }

            var url = $"https://github.com/EmptyFlow/SciterAPI/releases/download/0.0.0/sciter_{os}_{arch}.zip";
            var httpClient = new HttpClient ();
            var response = httpClient.Send (
                new HttpRequestMessage {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri ( url )
                }
            );
            using var file = File.Open ( "sciter.zip", FileMode.OpenOrCreate );
            response.Content.ReadAsStream ().CopyTo ( file );
            file.Close ();

            try {
                ZipFile.ExtractToDirectory ( "sciter.zip", ".", true );
            } catch ( Exception ex ) {
                if ( ex.Message != null ) {
                }
            }
        }

        public void Dispose () {
            try {
                File.Delete ( "sciter.zip" );
            } catch {
            }
        }

    }

}

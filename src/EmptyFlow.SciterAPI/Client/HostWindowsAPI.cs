using EmptyFlow.SciterAPI.Enums;

namespace EmptyFlow.SciterAPI {

    public partial class SciterAPIHost {

        public void CloseMainWindow () {
            if ( m_mainWindow == IntPtr.Zero ) return;

            m_basicApi.SciterWindowExec ( m_mainWindow, WindowCommand.SCITER_WINDOW_SET_STATE, (int) WindowState.SCITER_WINDOW_STATE_CLOSED, IntPtr.Zero );
        }

        public void CloseWindow ( IntPtr window ) {
            if ( window == IntPtr.Zero ) return;

            m_basicApi.SciterWindowExec ( window, WindowCommand.SCITER_WINDOW_SET_STATE, (int) WindowState.SCITER_WINDOW_STATE_CLOSED, IntPtr.Zero );
        }

        /// <summary>
        /// Get main window size and position.
        /// </summary>
        /// <param name="sizeMode">In which window size area will be result.</param>
        /// <param name="windowRelateMode">In which area relate to monitor or other window will be coordinates.</param>
        /// <param name="inPhisicalDevicePixels"> If true coordinates are in physical device pixels, if false in CSS pixels 1/96 of inch.</param>
        public SciterWindowInfo? GetWindowSizeAndPosition ( nint window, WindowSizeMode sizeMode, WindowRelateMode windowRelateMode, bool inPhisicalDevicePixels = false ) {
            var boxOf = sizeMode == WindowSizeMode.Border ? "border" : "client";
            var relTo = windowRelateMode switch {
                WindowRelateMode.Desktop => "desktop",
                WindowRelateMode.Monitor => "monitor",
                WindowRelateMode.Self => "self",
                _ => ""
            };
            var script = $"Window.this.box(\"xywh\",\"{boxOf}\", \"{relTo}\", {( inPhisicalDevicePixels ? "true" : "false" )})";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsArray && !result.IsArrayLike ) {
                    Console.WriteLine ( "GetMainWindowSizeAndPosition: box() return not array!" );
                    return null;
                }
                var count = GetArrayOrMapCount ( ref result );
                var size = new SciterWindowSize ( 0, 0 );
                var position = new SciterWindowPosition ( 0, 0 );
                for ( var i = 0; i < count; i++ ) {
                    var arrayItem = GetArrayItem ( ref result, i );
                    var value = (int) arrayItem.d;
                    if ( i == 0 ) position = position with { X = value };
                    if ( i == 1 ) position = position with { Y = value };
                    if ( i == 2 ) size = size with { Width = value };
                    if ( i == 3 ) size = size with { Height = value };
                }
                return new SciterWindowInfo ( size, position );
            }

            return null;
        }

        public bool GetWindowActive ( nint window ) {
            var script = $"Window.this.isActive";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsBoolean ) {
                    Console.WriteLine ( "GetWindowActive: isActive return not boolean!" );
                    return false;
                }

                return result.d == 1;
            }

            return false;
        }

        public bool MoveWindow ( nint window, SciterWindowPosition position ) {
            var script = $"Window.this.move({position.X}, {position.Y})";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( result.IsErrorString || result.IsObjectError ) {
                    Console.WriteLine ( "MoveWindow: window not moved!" );
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool GetWindowTopMost ( nint window ) {
            var script = $"Window.this.isTopmost";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsBoolean ) {
                    Console.WriteLine ( "GetWindowTopMost: isTopmost return not boolean!" );
                    return false;
                }

                return result.d == 1;
            }

            return false;
        }

        public bool SetWindowTopMost ( nint window, bool state ) {
            var script = $"Window.this.isTopmost = {( state == true ? "true" : "false" )}";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( result.IsErrorString || result.IsObjectError ) {
                    Console.WriteLine ( "SetWindowTopMost: value not changed! " );
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool GetWindowMaximizable ( nint window ) {
            var script = $"Window.this.isMaximizable";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsBoolean ) {
                    Console.WriteLine ( "GetWindowMaximizable: isMaximizable return not boolean!" );
                    return false;
                }

                return result.d == 1;
            }

            return false;
        }

        public bool SetWindowMaximizable ( nint window, bool state ) {
            var script = $"Window.this.isMaximizable = {( state == true ? "true" : "false" )}";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( result.IsErrorString || result.IsObjectError ) {
                    Console.WriteLine ( "SetWindowMaximizable: value not changed! " );
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool GetWindowMinimizable ( nint window ) {
            var script = $"Window.this.isMinimizable";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsBoolean ) {
                    Console.WriteLine ( "GetWindowMinimizable: isMinimizable return not boolean!" );
                    return false;
                }

                return result.d == 1;
            }

            return false;
        }

        public bool SetWindowMinimizable ( nint window, bool state ) {
            var script = $"Window.this.isMinimizable = {( state == true ? "true" : "false" )}";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( result.IsErrorString || result.IsObjectError ) {
                    Console.WriteLine ( "SetWindowMinimizable: value not changed! " );
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool GetWindowEnabled ( nint window ) {
            var script = $"Window.this.isEnabled";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsBoolean ) {
                    Console.WriteLine ( "GetWindowEnabled: isEnabled return not boolean!" );
                    return false;
                }

                return result.d == 1;
            }

            return false;
        }

        public bool SetWindowEnabled ( nint window, bool state ) {
            var script = $"Window.this.isEnabled = {( state == true ? "true" : "false" )}";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( result.IsErrorString || result.IsObjectError ) {
                    Console.WriteLine ( "SetWindowEnabled: value not changed! " );
                    return false;
                }

                return true;
            }

            return false;
        }

        public double? GetWindowAspectRatio ( nint window ) {
            var script = $"Window.this.aspectRatio";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsFloat ) {
                    Console.WriteLine ( "GetWindowAspectRatio: aspectRatio return not boolean!" );
                    return null;
                }

                return GetValueDouble ( ref result );
            }

            return null;
        }

        public bool SetWindowAspectRatio ( nint window, double value ) {
            var script = $"Window.this.aspectRatio = {value}";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( result.IsErrorString || result.IsObjectError ) {
                    Console.WriteLine ( "SetWindowAspectRatio: value not changed! " );
                    return false;
                }

                return true;
            }

            return false;
        }

        public string GetWindowCaption ( nint window ) {
            var script = $"Window.this.caption";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( !result.IsString ) {
                    Console.WriteLine ( "GetWindowCaption: caption return not string!" );
                    return "";
                }

                return GetValueString ( ref result );
            }

            return "";
        }

        public bool SetWindowCaption ( nint window, string value ) {
            var script = $"Window.this.caption = \"{value}\"";
            if ( m_basicApi.SciterEval ( m_mainWindow, script, (uint) script.Length, out var result ) ) {
                if ( result.IsErrorString || result.IsObjectError ) {
                    Console.WriteLine ( "SetWindowCaption: value not changed! " );
                    return false;
                }

                return true;
            }

            return false;
        }

    }

    public record SciterWindowInfo ( SciterWindowSize Size, SciterWindowPosition Position );

    public record SciterWindowSize ( int Width, int Height );

    public record SciterWindowPosition ( int X, int Y );

}


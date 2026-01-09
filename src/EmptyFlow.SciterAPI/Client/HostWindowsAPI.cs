using EmptyFlow.SciterAPI.Enums;
using EmptyFlow.SciterAPI.Structs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

	public partial class SciterAPIHost {

		[Obsolete ( "Please use CloseWindow instead" )]
		public void CloseMainWindow () {
			if ( m_mainWindow == IntPtr.Zero ) return;

			m_basicApi.SciterWindowExec ( m_mainWindow, WindowCommand.SCITER_WINDOW_SET_STATE, (int) WindowState.SCITER_WINDOW_STATE_CLOSED, IntPtr.Zero );
		}

		public void CloseWindow ( IntPtr window ) {
			if ( window == IntPtr.Zero ) return;

			m_basicApi.SciterWindowExec ( window, WindowCommand.SCITER_WINDOW_SET_STATE, (int) WindowState.SCITER_WINDOW_STATE_CLOSED, IntPtr.Zero );
		}

		/// <summary>
		/// Create window.
		/// </summary>
		/// <param name="width">Width, if omit will be half of curent screen.</param>
		/// <param name="height">Height, if omit will be half of curent screen.</param>
		/// <param name="flags">Windows flags.</param>
		/// <param name="debugOutput">It will be showed output in stout or not.</param>
		/// <param name="hostCallback">Host callback, can be remake all default events like bahaviour, create window or so on. If omit will be apply default just like in main window.</param>
		/// <param name="parent">Can be specified parent window, can be useful in some cases.</param>
		/// <returns>Pointer to created window.</returns>
		public nint CreateWindow ( int width = 0, int height = 0, WindowsFlags? flags = default, bool debugOutput = false, SciterHostCallback? hostCallback = default, nint parent = default ) {
			var rectangePointer = nint.Zero;
			if ( width > 0 && height > 0 ) {
				var rectangle = new SciterRectangle ( 0, 0, width, height );
				rectangePointer = Marshal.AllocHGlobal ( Marshal.SizeOf<SciterRectangle> () );
				Marshal.StructureToPtr ( rectangle, rectangePointer, false );
			}

			var windowPointer = m_basicApi.SciterCreateWindow (
				flags == null ? WindowsFlags.Resizeable | WindowsFlags.Titlebar | WindowsFlags.Controls : flags.Value,
				rectangePointer,
				nint.Zero,
				nint.Zero,
				parent
			);

			if ( rectangePointer != nint.Zero ) Marshal.FreeHGlobal ( rectangePointer );

			if ( debugOutput ) {
				m_basicApi.SciterSetupDebugOutput (
					windowPointer,
					1,
					( IntPtr param, uint subsystem, uint severity, IntPtr text_ptr, uint text_length ) => {
						Console.WriteLine ( Marshal.PtrToStringUni ( text_ptr, (int) text_length ) );
						return IntPtr.Zero;
					}
				);
			}

			m_callbacks.RegisterWindowCallback ( windowPointer, hostCallback );

			return windowPointer;
		}

		public void ShowWindow ( nint windowPointer ) {
			//activate window
			m_basicApi.SciterWindowExec ( windowPointer, WindowCommand.SCITER_WINDOW_ACTIVATE, 1, nint.Zero );

			//expand window
			m_basicApi.SciterWindowExec ( windowPointer, WindowCommand.SCITER_WINDOW_SET_STATE, 1, nint.Zero );
		}

		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public bool ExecuteWindowEval ( nint window, string script, out SciterValue result ) => m_basicApi.SciterEval ( window, script, (uint) script.Length, out result );

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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
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
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
				if ( result.IsErrorString || result.IsObjectError ) {
					Console.WriteLine ( "SetWindowCaption: value not changed! " );
					return false;
				}

				return true;
			}

			return false;
		}

		public SciterWindowFrameType GetWindowFrameType ( nint window ) {
			var script = $"Window.this.frameType";
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
				if ( !result.IsString ) {
					Console.WriteLine ( "GetWindowFrameType: caption return not string!" );
					return SciterWindowFrameType.Unknown;
				}

				var value = GetValueString ( ref result );
				return value switch {
					"standard" => SciterWindowFrameType.Standart,
					"transparent" => SciterWindowFrameType.Transparent,
					"solid" => SciterWindowFrameType.Solid,
					"solid-with-shadow" => SciterWindowFrameType.SolidWithShadows,
					"extended" => SciterWindowFrameType.Extended,
					_ => SciterWindowFrameType.Unknown
				};
			}

			return SciterWindowFrameType.Unknown;
		}

		public bool SetWindowFrameType ( nint window, SciterWindowFrameType value ) {
			if ( value == SciterWindowFrameType.Unknown ) throw new Exception ( "Frame type SciterWindowFrameType.Unknown not supported!" );

			var stringValue = value switch {
				SciterWindowFrameType.Standart => "standard",
				SciterWindowFrameType.Transparent => "transparent",
				SciterWindowFrameType.Solid => "solid",
				SciterWindowFrameType.SolidWithShadows => "solid-with-shadow",
				SciterWindowFrameType.Extended => "extended",
				_ => ""
			};

			var script = $"Window.this.frameType = \"{stringValue}\"";
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
				if ( result.IsErrorString || result.IsObjectError ) {
					Console.WriteLine ( "SetWindowFrameType: value not changed! " );
					return false;
				}

				return true;
			}

			return false;
		}

		public bool GetWindowResizable ( nint window ) {
			var script = $"Window.this.isResizable";
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
				if ( !result.IsBoolean ) {
					Console.WriteLine ( "GetWindowResizable: isResizable return not boolean!" );
					return false;
				}

				return result.d == 1;
			}

			return false;
		}

		public bool SetWindowResizable ( nint window, bool state ) {
			var script = $"Window.this.isResizable = {( state == true ? "true" : "false" )}";
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
				if ( result.IsErrorString || result.IsObjectError ) {
					Console.WriteLine ( "SetWindowResizable: value not changed! " );
					return false;
				}

				return true;
			}

			return false;
		}

		public bool UpdateWindow ( nint window ) {
			var script = $"Window.this.update()";
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) return false;

			return false;
		}

		public IEnumerable<string>? ShowWindowSelectFileDialog ( nint window, SciterSelectFileDialogMode mode, IDictionary<string, string> filters, string defaultExtension, string? caption = default, string? path = default ) {
			var keys = new Dictionary<string, string> ();
			var scriptMode = mode switch {
				SciterSelectFileDialogMode.Save => "save",
				SciterSelectFileDialogMode.Open => "open",
				SciterSelectFileDialogMode.OpenMultiple => "open-multiple",
				_ => "open"
			};
			keys.Add ( "mode", scriptMode );
			keys.Add ( "filter", string.Join ( '|', filters.Select ( a => string.Join ( '|', a.Key, a.Value ) ) ) );
			var scriptCaption = mode switch {
				SciterSelectFileDialogMode.Save => "Save As",
				SciterSelectFileDialogMode.Open => "Open File",
				SciterSelectFileDialogMode.OpenMultiple => "Open Files",
				_ => "Open File"
			};
			keys.Add ( "caption", scriptCaption );
			keys.Add ( "extension", defaultExtension );
			if ( path != default ) keys.Add ( "path", path );

			var properties = "{" + string.Join ( ",", keys.Select ( a => a.Key + ": \"" + a.Value + "\"" ) ) + "}";

			var script = $"Window.this.selectFile({properties})";
			if ( m_basicApi.SciterEval ( window, script, (uint) script.Length, out var result ) ) {
				if ( result.IsErrorString || result.IsObjectError ) {
					Console.WriteLine ( "ShowWindowSelectFileDialog: error occurs when calling! " );
					return null;
				}

				if ( result.IsString ) return [GetValueString ( ref result )];
				if ( result.IsArray || result.IsArrayLike ) {
					List<string> files = [];
					var count = GetArrayOrMapCount ( ref result );
					for ( var i = 0; i < count; i++ ) {
						var element = GetArrayItem ( ref result, i );
						files.Add ( GetValueString ( ref element ) );
					}
					return files;
				}
				if ( result.IsNull ) return null;

				return null;
			}

			return null;
		}

	}

	public enum SciterSelectFileDialogMode {

		Save = 0,

		Open = 1,

		OpenMultiple = 2

	};

	public enum SciterWindowFrameType {

		Unknown = 0,

		Standart = 1,

		Transparent = 2,

		Solid = 3,

		SolidWithShadows = 4,

		Extended = 5

	};

	public record SciterWindowInfo ( SciterWindowSize Size, SciterWindowPosition Position );

	public record SciterWindowSize ( int Width, int Height );

	public record SciterWindowPosition ( int X, int Y );

}


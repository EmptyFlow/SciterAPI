using EmptyFlow.SciterAPI.Client.Models;
using EmptyFlow.SciterAPI.Enums;
using EmptyFlow.SciterAPI.Structs;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

	public partial class SciterAPIHost {

		/// <summary>
		/// Redraw element.
		/// </summary>
		/// <param name="element">Element.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void RedrawElement ( nint element ) => m_basicApi.SciterUpdateElement ( element, false );

		/// <summary>
		/// Force rendering element.
		/// </summary>
		/// <param name="element">Element.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void ForceRenderElement ( nint element ) => m_basicApi.SciterUpdateElement ( element, true );

		private bool CheckGraphics () {
			if ( !m_graphicsApiLoaded ) {
				Console.WriteLine ( "Try to using graphics API but graphics API not loaded! You need to load graphics API by call host.PrepareGraphicsApi() method!" );
				return false;
			}

			return true;
		}

		/// <summary>
		/// Graphics save state.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		public void GraphicsSaveState ( nint hgfx ) {
			if ( !CheckGraphics () ) return;

			m_graphicsApi.gStateSave ( hgfx );
		}

		/// <summary>
		/// Graphics resore state.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		public void GraphicsRestoreState ( nint hgfx ) {
			if ( !CheckGraphics () ) return;

			m_graphicsApi.gStateRestore ( hgfx );
		}

		public void GraphicsPerformInState ( nint hgfx, Action callback ) {
			if ( !CheckGraphics () ) return;

			m_graphicsApi.gStateSave ( hgfx );

			callback ();

			m_graphicsApi.gStateRestore ( hgfx );
		}

		/// <summary>
		/// Graphics set color for further line.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="color">Line color. You need to get color from GraphicsGetColor method.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsLineColor ( nint hgfx, uint color ) {
			m_graphicsApi.gLineColor ( hgfx, color );
		}

		/// <summary>
		/// Setup parameters of linear gradient of lines.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="x2">X2 coordinate.</param>
		/// <param name="y2">Y2 coordinate.</param>
		/// <param name="stops">Color stops.</param>
		/// <param name="nstops">No idea what is it.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsLineGradientLinear ( nint hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops ) {
			m_graphicsApi.gLineGradientLinear ( hgfx, x1, y1, x2, y2, stops, nstops );
		}

		/// <summary>
		/// Setup parameters of linear gradient of lines.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="x2">X2 coordinate.</param>
		/// <param name="y2">Y2 coordinate.</param>
		/// <param name="stops">Color stops.</param>
		/// <param name="nstops">No idea what is it.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsLineGradientRadial ( nint hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops ) {
			m_graphicsApi.gLineGradientRadial ( hgfx, x1, y1, x2, y2, stops, nstops );
		}

		/// <summary>
		/// Graphics set color for further line.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="color">Line color. You need to get color from GraphicsGetColor method.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsFillColor ( nint hgfx, uint color ) {
			m_graphicsApi.gFillColor ( hgfx, color );
		}

		/// <summary>
		/// Setup parameters of linear gradient of fills.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="x2">X2 coordinate.</param>
		/// <param name="y2">Y2 coordinate.</param>
		/// <param name="stops">Color stops.</param>
		/// <param name="nstops">No idea what is it.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsFillGradientLinear ( nint hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops ) {
			m_graphicsApi.gFillGradientLinear ( hgfx, x1, y1, x2, y2, stops, nstops );
		}

		/// <summary>
		/// Setup parameters of gradient radial fills.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="x2">X2 coordinate.</param>
		/// <param name="y2">Y2 coordinate.</param>
		/// <param name="stops">Color stops.</param>
		/// <param name="nstops">No idea what is it.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsFillGradientRadial ( nint hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops ) {
			m_graphicsApi.gFillGradientRadial ( hgfx, x1, y1, x2, y2, stops, nstops );
		}

		/// <summary>
		/// Set line width.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="width">New width for the line.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsSetLineWidth ( nint hgfx, float width ) {
			m_graphicsApi.gLineWidth ( hgfx, width );
		}

		/// <summary>
		/// Draws line from x1,y1 to x2,y2 using current GraphicsSetLineColor and lineGradient.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="x2">X2 coordinate.</param>
		/// <param name="y2">Y2 coordinate.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsDrawLine ( nint hgfx, float x1, float y1, float x2, float y2 ) {
			m_graphicsApi.gLine ( hgfx, x1, y1, x2, y2 );
		}

		public nint GraphicsPath ( Action<nint> callback ) {
			var result = m_graphicsApi.pathCreate ( out var path );
			if ( result != GraphInResult.Ok ) return path;

			callback ( path );

			m_graphicsApi.pathClosePath ( path );

			return nint.Zero;
		}

		public bool GraphicsDrawPath ( nint hgfx, nint path, DrawPathMode pathMode ) {
			var result = m_graphicsApi.gDrawPath ( hgfx, path, pathMode );
			if ( result == GraphInResult.Ok ) return true;

			return false;
		}

		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsPathLineTo ( nint path, SciterPoint position, bool relative ) {
			m_graphicsApi.pathLineTo ( path, position.X, position.Y, relative );
		}

		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsPathMoveTo ( nint path, SciterPoint position, bool relative ) {
			m_graphicsApi.pathMoveTo ( path, position.X, position.Y, relative );
		}

		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsPathArcTo ( nint path, SciterPoint position, float angle, SciterPoint radius, bool largeArc, bool clockwise, bool relative ) {
			m_graphicsApi.pathArcTo ( path, position.X, position.Y, angle, radius.X, radius.Y, largeArc, clockwise, relative );
		}

		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsPathQuadraticCurveTo ( nint path, float xc, float yc, float x, float y, bool relative ) {
			m_graphicsApi.pathQuadraticCurveTo ( path, xc, yc, x, y, relative );
		}

		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsPathQuadraticCurveTo ( nint path, float xc1, float yc1, float xc2, float yc2, float x, float y, bool relative ) {
			m_graphicsApi.pathBezierCurveTo ( path, xc1, yc1, xc2, yc2, x, y, relative );
		}

		/// <summary>
		/// Draws line from x1,y1 to x2,y2 with width and color.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="x2">X2 coordinate.</param>
		/// <param name="y2">Y2 coordinate.</param>
		public void GraphicsDrawLine ( nint hgfx, Vector2 start, Vector2 end, uint color, float width ) {
			m_graphicsApi.gLineColor ( hgfx, color );
			m_graphicsApi.gLineWidth ( hgfx, width );
			m_graphicsApi.gLine ( hgfx, start.X, start.Y, end.X, end.Y );
		}

		/// <summary>
		/// Draw ellipse.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="rx">Radius X.</param>
		/// <param name="ry">Radius Y.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsDrawEllipse ( nint hgfx, float x1, float y1, float rx, float ry ) {
			m_graphicsApi.gEllipse ( hgfx, x1, y1, rx, ry );
		}

		/// <summary>
		/// Draws rectangle using current GraphicsSetLineColor/lineGradient and GraphicsSetFillColor/fillGradient with (optional) rounded corners.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="x2">X2 coordinate.</param>
		/// <param name="y2">Y2 coordinate.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsDrawRectangle ( nint hgfx, float x1, float y1, float x2, float y2 ) {
			m_graphicsApi.gRectangle ( hgfx, x1, y1, x2, y2 );
		}

		/// <summary>
		/// Draw rectangle.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="x1">X1 coordinate.</param>
		/// <param name="y1">Y1 coordinate.</param>
		/// <param name="x2">X2 coordinate.</param>
		/// <param name="y2">Y2 coordinate.</param>
		/// <param name="radii8">8 pair array.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsDrawRoundedRectangle ( nint hgfx, float x1, float y1, float x2, float y2, float[] radii8 ) {
			m_graphicsApi.gRoundedRectangle ( hgfx, x1, y1, x2, y2, radii8 );
		}

		public GraphicsTextModel GraphicsCreateTextForElement ( nint he, string text, string className ) {
			if ( !CheckGraphics () ) return new GraphicsTextModel { Element = nint.Zero, Id = nint.Zero };

			nint textPointer;
			var graphResult = m_graphicsApi.textCreateForElement ( out textPointer, text, (uint) text.Length, he, className );
			if ( graphResult != GraphInResult.Ok ) {
				Console.WriteLine ( "GraphicsCreateTextForElement resulted with error, actual result - " + graphResult );
				return new GraphicsTextModel { Id = nint.Zero, Element = nint.Zero };
			}
			return new GraphicsTextModel { Id = textPointer, Element = he };
		}

		public GraphicsTextModel GraphicsCreateTextForElementWithStyle ( nint he, string text, string style ) {
			if ( !CheckGraphics () ) return new GraphicsTextModel { Element = nint.Zero, Id = nint.Zero };

			nint textPointer;
			var graphResult = m_graphicsApi.textCreateForElementAndStyle ( out textPointer, text, (uint) text.Length, he, style, (uint) style.Length );
			if ( graphResult != GraphInResult.Ok ) {
				Console.WriteLine ( "GraphicsCreateTextForElementWithStyle resulted with error, actual result - " + graphResult );
				return new GraphicsTextModel { Id = nint.Zero, Element = nint.Zero };
			}
			return new GraphicsTextModel { Id = textPointer, Element = he };
		}

		public void GraphicsReleaseText ( GraphicsTextModel text ) {
			if ( !CheckGraphics () ) return;

			var graphResult = m_graphicsApi.textRelease ( text.Id );
			if ( graphResult == GraphInResult.Ok ) Marshal.FreeHGlobal ( text.Id );
		}

		/// <summary>
		/// Draw text.
		/// </summary>
		/// <param name="hgfx">Pointer on graphics surface.</param>
		/// <param name="text">Text for draw.</param>
		/// <param name="x">X coordinate.</param>
		/// <param name="y">Y coordinate.</param>
		/// <param name="coordinates">Position (1..9).</param>
		public void GraphicsDrawText ( nint hgfx, GraphicsTextModel text, Vector2 coordinates, SciterTextPosition position ) {
			var graphResult = m_graphicsApi.gDrawText ( hgfx, text.Id, coordinates.X, coordinates.Y, (uint) position );
			if ( graphResult != GraphInResult.Ok ) Console.WriteLine ( "GraphicsDrawText resulted with error, actual result - " + graphResult );
		}

		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsDrawImage ( nint hgfx, nint image, float x, float y, float w, float h, uint ix, uint iy, uint iw, uint ih, float opacity ) {
			m_graphicsApi.gDrawImage ( hgfx, image, x, y, w, h, ix, iy, iw, ih, opacity );
		}

		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public void GraphicsDrawImage ( nint hgfx, nint image, Vector2 position, Vector2 size, uint ix, uint iy, uint iw, uint ih, float opacity ) {
			m_graphicsApi.gDrawImage ( hgfx, image, position.X, position.Y, position.X, position.Y, ix, iy, iw, ih, opacity );
		}

		/// <summary>
		/// Graphics get color from different channel components.
		/// </summary>
		/// <param name="r">Red.</param>
		/// <param name="g">Green.</param>
		/// <param name="b">Blue.</param>
		/// <param name="a">Alpha, be default is 255.</param>
		[MethodImpl ( MethodImplOptions.AggressiveInlining )]
		public uint GraphicsGetColor ( uint r, uint g, uint b, uint a = 255 ) {
			return m_graphicsApi.RGBA ( r, g, b, a );
		}

	}

}

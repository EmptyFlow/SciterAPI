using EmptyFlow.SciterAPI.Client.Models;
using EmptyFlow.SciterAPI.Structs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI {

    public partial class SciterAPIHost {

        /// <summary>
        /// Redraw element.
        /// </summary>
        /// <param name="element">Element.</param>
        public void RedrawElement ( nint element ) => m_basicApi.SciterUpdateElement ( element, false );

        /// <summary>
        /// Force rendering element.
        /// </summary>
        /// <param name="element">Element.</param>
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
        public void GraphicsLineColor ( nint hgfx, uint color ) {
            if ( !CheckGraphics () ) return;

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
        public void GraphicsLineGradientLinear ( nint hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops ) {
            if ( !CheckGraphics () ) return;

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
        public void GraphicsLineGradientRadial ( nint hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops ) {
            if ( !CheckGraphics () ) return;

            m_graphicsApi.gLineGradientRadial ( hgfx, x1, y1, x2, y2, stops, nstops );
        }

        /// <summary>
        /// Graphics set color for further line.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="color">Line color. You need to get color from GraphicsGetColor method.</param>
        public void GraphicsFillColor ( nint hgfx, uint color ) {
            if ( !CheckGraphics () ) return;

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
        public void GraphicsFillGradientLinear ( nint hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops ) {
            if ( !CheckGraphics () ) return;

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
        public void GraphicsFillGradientRadial ( nint hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops ) {
            if ( !CheckGraphics () ) return;

            m_graphicsApi.gFillGradientRadial ( hgfx, x1, y1, x2, y2, stops, nstops );
        }

        /// <summary>
        /// Set line width.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="width">New width for the line.</param>
        public void GraphicsSetLineWidth ( nint hgfx, float width ) {
            if ( !CheckGraphics () ) return;

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
        public void GraphicsDrawLine ( nint hgfx, float x1, float y1, float x2, float y2 ) {
            if ( !CheckGraphics () ) return;

            m_graphicsApi.gLine ( hgfx, x1, y1, x2, y2 );
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
            if ( !CheckGraphics () ) return;

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
        public void GraphicsDrawEllipse ( nint hgfx, float x1, float y1, float rx, float ry ) {
            if ( !CheckGraphics () ) return;

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
        public void GraphicsDrawRectangle ( nint hgfx, float x1, float y1, float x2, float y2 ) {
            if ( !CheckGraphics () ) return;

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
        public void GraphicsDrawRoundedRectangle ( nint hgfx, float x1, float y1, float x2, float y2, float[] radii8 ) {
            if ( !CheckGraphics () ) return;

            m_graphicsApi.gRoundedRectangle ( hgfx, x1, y1, x2, y2, radii8 );
        }

        public GraphicsTextModel GraphicsCreateTextForElement ( nint hgfx, nint he, string text, string className ) {
            if ( !CheckGraphics () ) return new GraphicsTextModel { Element = nint.Zero, Id = nint.Zero };

            var textPointer = Marshal.AllocHGlobal ( Marshal.SizeOf<int> () );
            var result = new GraphicsTextModel { Id = textPointer, Element = he };
            m_graphicsApi.textCreateForElement ( textPointer, text, (uint) text.Length, he, className );

            return result;
        }

        public void GraphicsReleaseText( GraphicsTextModel text) {
            if ( !CheckGraphics () ) return;

            m_graphicsApi.textRelease ( text.Id );
            Marshal.FreeHGlobal ( text.Id );
        }

        /// <summary>
        /// Draw text.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="text">Text for draw.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="coordinates">Position (1..9).</param>
        public void GraphicsDrawText ( nint hgfx, GraphicsTextModel text, Vector2 coordinates, uint position ) {
            if ( !CheckGraphics () ) return;

            m_graphicsApi.gDrawText ( hgfx, text.Id, coordinates.X, coordinates.Y, position );
        }

        public void GraphicsDrawImage ( nint hgfx, nint image, float x, float y, float w, float h, uint ix, uint iy, uint iw, uint ih, float opacity ) {
            if ( !CheckGraphics () ) return;

            m_graphicsApi.gDrawImage ( hgfx, image, x, y, w, h, ix, iy, iw, ih, opacity );
        }

        public void GraphicsDrawImage ( nint hgfx, nint image, Vector2 position, Vector2 size, uint ix, uint iy, uint iw, uint ih, float opacity ) {
            if ( !CheckGraphics () ) return;

            m_graphicsApi.gDrawImage ( hgfx, image, position.X, position.Y, position.X, position.Y, ix, iy, iw, ih, opacity );
        }

        /// <summary>
        /// Graphics get color from different channel components.
        /// </summary>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        /// <param name="a">Alpha.</param>
        public uint GraphicsGetColor ( uint r, uint g, uint b, uint a ) {
            if ( CheckGraphics () ) return m_graphicsApi.RGBA ( r, g, b, a );

            return 0;
        }

    }

}

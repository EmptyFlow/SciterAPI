using EmptyFlow.SciterAPI.Structs;
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

        private void TryToExecuteGraphics ( Action<GraphicsApiStruct> callback ) {
            if ( !m_graphicsApiLoaded ) {
                Console.WriteLine ( "Try to using graphics API but graphics API not loaded! You need to load graphics API by call host.PrepareGraphicsApi() method!" );
                return;
            }

            callback ( m_graphicsApi );
        }

        /// <summary>
        /// Graphics save state.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        public void GraphicsSaveState ( nint hgfx ) => TryToExecuteGraphics ( ( api ) => api.gStateSave ( hgfx ) );

        /// <summary>
        /// Graphics resore state.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        public void GraphicsRestoreState ( nint hgfx ) => TryToExecuteGraphics ( ( api ) => api.gStateRestore ( hgfx ) );

        /// <summary>
        /// Graphics set color for further line.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="color">Line color. You need to get color from GraphicsGetColor method.</param>
        public void GraphicsLineColor ( nint hgfx, uint color ) => TryToExecuteGraphics ( ( api ) => api.gLineColor ( hgfx, color ) );

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
            TryToExecuteGraphics ( ( api ) => api.gLineGradientLinear ( hgfx, x1, y1, x2, y2, stops, nstops ) );
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
            TryToExecuteGraphics ( ( api ) => api.gLineGradientRadial ( hgfx, x1, y1, x2, y2, stops, nstops ) );
        }

        /// <summary>
        /// Graphics set color for further line.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="color">Line color. You need to get color from GraphicsGetColor method.</param>
        public void GraphicsFillColor ( nint hgfx, uint color ) => TryToExecuteGraphics ( ( api ) => api.gFillColor ( hgfx, color ) );

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
            TryToExecuteGraphics ( ( api ) => api.gFillGradientLinear ( hgfx, x1, y1, x2, y2, stops, nstops ) );
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
            TryToExecuteGraphics ( ( api ) => api.gFillGradientRadial ( hgfx, x1, y1, x2, y2, stops, nstops ) );
        }

        /// <summary>
        /// Set line width.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="width">New width for the line.</param>
        public void GraphicsSetLineWidth ( nint hgfx, float width ) => TryToExecuteGraphics ( ( api ) => api.gLineWidth ( hgfx, width ) );

        /// <summary>
        /// Draws line from x1,y1 to x2,y2 using current GraphicsSetLineColor and lineGradient.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="x1">X1 coordinate.</param>
        /// <param name="y1">Y1 coordinate.</param>
        /// <param name="x2">X2 coordinate.</param>
        /// <param name="y2">Y2 coordinate.</param>
        public void GraphicsDrawLine ( nint hgfx, float x1, float y1, float x2, float y2 ) => TryToExecuteGraphics ( ( api ) => api.gLine ( hgfx, x1, y1, x2, y2 ) );

        /// <summary>
        /// Draw ellipse.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="x1">X1 coordinate.</param>
        /// <param name="y1">Y1 coordinate.</param>
        /// <param name="rx">Radius X.</param>
        /// <param name="ry">Radius Y.</param>
        public void GraphicsDrawEllipse ( nint hgfx, float x1, float y1, float rx, float ry ) => TryToExecuteGraphics ( ( api ) => api.gEllipse ( hgfx, x1, y1, rx, ry ) );

        /// <summary>
        /// Draws rectangle using current GraphicsSetLineColor/lineGradient and GraphicsSetFillColor/fillGradient with (optional) rounded corners.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="x1">X1 coordinate.</param>
        /// <param name="y1">Y1 coordinate.</param>
        /// <param name="x2">X2 coordinate.</param>
        /// <param name="y2">Y2 coordinate.</param>
        public void GraphicsDrawRectangle ( nint hgfx, float x1, float y1, float x2, float y2 ) => TryToExecuteGraphics ( ( api ) => api.gRectangle ( hgfx, x1, y1, x2, y2 ) );

        /// <summary>
        /// Draw rectangle.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="x1">X1 coordinate.</param>
        /// <param name="y1">Y1 coordinate.</param>
        /// <param name="x2">X2 coordinate.</param>
        /// <param name="y2">Y2 coordinate.</param>
        /// <param name="radii8">8 pair array.</param>
        public void GraphicsDrawRoundedRectangle ( nint hgfx, float x1, float y1, float x2, float y2, float[] radii8 ) => TryToExecuteGraphics ( ( api ) => api.gRoundedRectangle ( hgfx, x1, y1, x2, y2, radii8 ) );

        /// <summary>
        /// Draw rectangle.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="text">Text for draw.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="position">Position (1..9).</param>
        public void GraphicsDrawText ( nint hgfx, string text, float x, float y, uint position ) {
            var textPointer = Marshal.StringToHGlobalUni ( text );
            TryToExecuteGraphics ( ( api ) => api.gDrawText ( hgfx, textPointer, x, y, position ) );
            Marshal.FreeHGlobal ( textPointer );
        }

        public void GraphicsDrawImage ( nint hgfx, nint image, float x, float y, float w, float h, uint ix, uint iy, uint iw, uint ih, float opacity ) {
            TryToExecuteGraphics ( ( api ) => api.gDrawImage ( hgfx, image, x, y, w, h, ix, iy,iw, ih, opacity) );
        }

        /// <summary>
        /// Graphics get color from different channel components.
        /// </summary>
        /// <param name="r">Red.</param>
        /// <param name="g">Green.</param>
        /// <param name="b">Blue.</param>
        /// <param name="a">Alpha.</param>
        public uint GraphicsGetColor ( uint r, uint g, uint b, uint a ) {
            uint result = 0;
            TryToExecuteGraphics ( ( api ) => { result = api.RGBA ( r, g, b, a ); } );
            return result;
        }

    }

}

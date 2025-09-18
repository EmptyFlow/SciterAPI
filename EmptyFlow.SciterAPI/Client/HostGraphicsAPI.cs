using EmptyFlow.SciterAPI.Structs;

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
        public void GraphicsSetLineColor ( nint hgfx ) => TryToExecuteGraphics ( ( api ) => api.gLineColor ( hgfx, 0 ) );

        /// <summary>
        /// Graphics draw line.
        /// </summary>
        /// <param name="hgfx">Pointer on graphics surface.</param>
        /// <param name="x1">X1 coordinate.</param>
        /// <param name="y1">Y1 coordinate.</param>
        /// <param name="x2">X2 coordinate.</param>
        /// <param name="y2">Y2 coordinate.</param>
        public void GraphicsDrawLine ( nint hgfx, float x1, float y1, float x2, float y2 ) => TryToExecuteGraphics ( ( api ) => api.gLine ( hgfx, x1, y1, x2, y2 ) );

    }

}

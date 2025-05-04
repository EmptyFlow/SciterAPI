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

    }

}

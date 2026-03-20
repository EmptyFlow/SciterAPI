namespace EmptyFlow.SciterAPI {

	public partial class SciterAPIHost {

		/// <summary>
		/// Remove node/element.
		/// </summary>
		/// <param name="node">Node or element.</param>
		/// <param name="finalize">Finalize operation.</param>
		/// <returns>Operation was sucessfully.</returns>
		public bool NodeRemove ( nint node, bool finalize ) {
			var domResult = m_basicApi.SciterNodeRemove ( node, finalize );
			return domResult == DomResult.SCDOM_OK;
		}

		/// <summary>
		/// Get next sibling of node/element.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Next sibling node/element or zero.</returns>
		public nint NodeNextSibling ( nint node ) {
			var domResult = m_basicApi.SciterNodeNextSibling ( node, out var nextNode );
			if ( domResult == DomResult.SCDOM_OK ) return nextNode;

			return nint.Zero;
		}

		/// <summary>
		/// Get previous sibling of node/element.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Previous sibling node/element or zero.</returns>
		public nint NodePreviousSibling ( nint node ) {
			var domResult = m_basicApi.SciterNodePrevSibling ( node, out var nextNode );
			if ( domResult == DomResult.SCDOM_OK ) return nextNode;

			return nint.Zero;
		}

		/// <summary>
		/// Get next sibling of node/element.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Next sibling node/element or zero.</returns>
		public nint NodeFirstChild ( nint node ) {
			var domResult = m_basicApi.SciterNodeFirstChild ( node, out var nextNode );
			if ( domResult == DomResult.SCDOM_OK ) return nextNode;

			return nint.Zero;
		}

		/// <summary>
		/// Get previous sibling of node/element.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Previous sibling node/element or zero.</returns>
		public nint NodeLastChild ( nint node ) {
			var domResult = m_basicApi.SciterNodeLastChild ( node, out var nextNode );
			if ( domResult == DomResult.SCDOM_OK ) return nextNode;

			return nint.Zero;
		}

	}

}

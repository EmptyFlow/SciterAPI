using EmptyFlow.SciterAPI.Enums;
using EmptyFlow.SciterAPI.Structs;
using System.Runtime.InteropServices;
using System.Text;

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
			var domResult = m_basicApi.SciterNodePrevSibling ( node, out var previousNode );
			if ( domResult == DomResult.SCDOM_OK ) return previousNode;

			return nint.Zero;
		}

		/// <summary>
		/// Get next sibling of node/element.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Next sibling node/element or zero.</returns>
		public nint NodeFirstChild ( nint node ) {
			var domResult = m_basicApi.SciterNodeFirstChild ( node, out var firstChild );
			if ( domResult == DomResult.SCDOM_OK ) return firstChild;

			return nint.Zero;
		}

		/// <summary>
		/// Get previous sibling of node/element.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Previous sibling node/element or zero.</returns>
		public nint NodeLastChild ( nint node ) {
			var domResult = m_basicApi.SciterNodeLastChild ( node, out var lastChild );
			if ( domResult == DomResult.SCDOM_OK ) return lastChild;

			return nint.Zero;
		}

		/// <summary>
		/// Get node type.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Node type or null if dom result not success.</returns>
		public NodeType? GetNodeType ( nint node ) {
			var domResult = m_basicApi.SciterNodeType ( node, out var type );
			if ( domResult == DomResult.SCDOM_OK ) return type;

			return null;
		}

		/// <summary>
		/// Get node text.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Node text.</returns>
		public string GetNodeText ( nint node ) {
			var resultString = new StringBuilder ();
			lpcwstReceiver callback = ( IntPtr bytes, uint num_bytes, IntPtr param ) => {
				resultString.Append ( Marshal.PtrToStringUni ( bytes, Convert.ToInt32 ( num_bytes ) ) );
			};
			var domResult = m_basicApi.SciterNodeGetText ( node, callback, nint.Zero );
			if ( domResult == DomResult.SCDOM_OK ) return resultString.ToString ();

			return "";
		}

		/// <summary>
		/// Set node text.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <param name="text">Text which will be inserted into node.</param>
		/// <returns>Operation was successed?</returns>
		public bool SetNodeText ( nint node, string text ) {
			var domResult = m_basicApi.SciterNodeSetText ( node, text, (uint) text.Length );
			return domResult == DomResult.SCDOM_OK;
		}

		/// <summary>
		/// Get node/element parent.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Parent or null.</returns>
		public nint NodeParent ( nint node ) {
			var domResult = m_basicApi.SciterNodeParent ( node, out var parent );
			if ( domResult == DomResult.SCDOM_OK ) return parent;

			return nint.Zero;
		}

		/// <summary>
		/// Get node/element child.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <returns>Child or null.</returns>
		public nint NodeNthChild ( nint node, int index ) {
			var domResult = m_basicApi.SciterNodeNthChild ( node, (uint) index, out var child );
			if ( domResult == DomResult.SCDOM_OK ) return child;

			return nint.Zero;
		}

		/// <summary>
		/// Get node/element childrens count.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		public int NodeChildrenCount ( nint node ) {
			var domResult = m_basicApi.SciterNodeChildrenCount ( node, out var count );
			if ( domResult == DomResult.SCDOM_OK ) return (int) count;

			return 0;
		}

		/// <summary>
		/// Cast node from element.
		/// </summary>
		/// <param name="element">Element.</param>
		public nint NodeCastFromElement ( nint element ) {
			var domResult = m_basicApi.SciterNodeCastFromElement ( element, out var node );
			if ( domResult == DomResult.SCDOM_OK ) return node;

			return 0;
		}

		/// <summary>
		/// Insert node to another node.
		/// </summary>
		/// <param name="node">Node/Element.</param>
		/// <param name="target">Target location.</param>
		/// <param name="what">What node/element need to insert.</param>
		public bool NodeInsert ( nint node, NodeInsertTarget target, nint what ) {
			var domResult = m_basicApi.SciterNodeInsert ( node, (uint) target, what );
			if ( domResult == DomResult.SCDOM_OK ) return true;

			return false;
		}

		/// <summary>
		/// Create new text node.
		/// </summary>
		/// <param name="element">Element.</param>
		public nint NodeCreateTextNode ( string content ) {
			var domResult = m_basicApi.SciterCreateTextNode ( content, (uint)content.Length, out var node );
			if ( domResult == DomResult.SCDOM_OK ) return node;

			return nint.Zero;
		}

		/// <summary>
		/// Create new comment node.
		/// </summary>
		/// <param name="element">Element.</param>
		public nint NodeCreateCommentNode ( string content ) {
			var domResult = m_basicApi.SciterCreateCommentNode ( content, (uint) content.Length, out var node );
			if ( domResult == DomResult.SCDOM_OK ) return node;

			return nint.Zero;
		}

	}

}

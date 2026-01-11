using EmptyFlow.SciterAPI.Structs;

namespace EmptyFlow.SciterAPI {

	/// <summary>
	/// Class contains minimal scaffold for prepare for handling events from Sciter.
	/// </summary>
	public class SciterEventHandlerRaw {

		private readonly ElementEventProc m_innerDelegate;

		protected nint m_subscribedElement = IntPtr.Zero;

		/// <summary>
		/// Inner delegate for handle events from Sciter.
		/// </summary>
		public ElementEventProc InnerDelegate => m_innerDelegate;

		/// <summary>
		/// Element on which event subscribed this handler.
		/// </summary>
		public nint SubscribedElement => m_subscribedElement;

		public SciterEventHandlerRaw ( nint subscribedElement ) {
			m_innerDelegate = SciterHandleEvent;
			m_subscribedElement = subscribedElement;
		}

		private bool SciterHandleEvent ( IntPtr tag, IntPtr he, uint evtg, IntPtr prms ) {
			return EventHandler ( he, (EventBehaviourGroups) evtg, prms );
		}

		public virtual bool EventHandler ( nint processedElement, EventBehaviourGroups eventBehaviourGroup, nint parameters ) {
			return false;
		}

	}

}

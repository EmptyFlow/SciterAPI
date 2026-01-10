namespace EmptyFlow.SciterAPI {

	public partial class SciterAPIHost {

		/// <summary>
		/// Add <see cref="EventHandler"/> related with window.
		/// All events from all elements will be fired in this event handler.
		/// </summary>
		/// <param name="handler">Event Handler.</param>
		/// <exception cref="ArgumentException">Event handler must be with mode <see cref="SciterEventHandlerMode.Window"/>.</exception>
		public void AddWindowEventHandler ( SciterEventHandler handler, nint windowPointer = default ) {
			if ( handler.Mode != SciterEventHandlerMode.Window ) throw new ArgumentException ( "Passed EventHandler must be with mode = SciterEventHandlerMode.Window!" );

			m_basicApi.SciterWindowAttachEventHandler ( windowPointer == default ? m_mainWindow : windowPointer, handler.InnerDelegate, 1, (uint) EventBehaviourGroups.HandleAll );
		}

		private bool AddToEventHandlers ( SciterEventHandler handler ) {
			m_eventHandlerMap.Add ( handler.SubscribedElement, handler );
			var unique = handler.GetUnique ();
			if ( string.IsNullOrEmpty ( unique ) ) return false;
			if ( m_eventHandlerUniqueMap.ContainsKey ( unique ) ) {
				Console.WriteLine ( $"EventHandler with unique value: {unique} is already registered! It is not without reason that this is called a UNIQUE value." );
				return false;
			}
			m_eventHandlerUniqueMap.Add ( unique, handler.SubscribedElement );

			return true;
		}

		/// <summary>
		/// Add event handler and optionally attach to element.
		/// </summary>
		/// <param name="handler">Handler that will be attached to element.</param>
		/// <param name="fromFactory">If parameter is true which mean </param>
		/// <exception cref="ArgumentException"></exception>
		internal void AddEventHandlerWithoutAttaching ( SciterEventHandler handler ) {
			if ( handler.Mode != SciterEventHandlerMode.Element ) throw new ArgumentException ( "Passed EventHandler must be with mode = SciterEventHandlerMode.Element!" );

			AddToEventHandlers ( handler );
		}

		/// <summary>
		/// Add event handler and optionally attach to element.
		/// </summary>
		/// <param name="handler">Handler that will be attached to element.</param>
		/// <param name="fromFactory">If parameter is true which mean </param>
		/// <exception cref="ArgumentException"></exception>
		public void AddEventHandler ( SciterEventHandler handler ) {
			if ( handler.Mode != SciterEventHandlerMode.Element ) throw new ArgumentException ( "Passed EventHandler must be with mode = SciterEventHandlerMode.Element!" );

			if ( AddToEventHandlers ( handler ) ) m_basicApi.SciterAttachEventHandler ( handler.SubscribedElement, handler.InnerDelegate, 1 );
		}

		/// <summary>
		/// Remove from handler but you need to be sure it event handler actually detached from Sciter.
		/// By default it behaviout happened only in SciterEventHandler.HandleInitializationEvent.
		/// </summary>
		/// <param name="handler">The handler to be removed.</param>
		public void RemoveEventHandler ( SciterEventHandler handler ) {
			var key = m_eventHandlerMap
				.Where ( a => a.Value == handler )
				.Select ( a => a.Key )
				.FirstOrDefault ();
			if ( key == default ) return;
			m_eventHandlerMap.Remove ( key );

			var keyUnique = m_eventHandlerUniqueMap
				.Where ( a => a.Value == key )
				.Select ( a => a.Key )
				.FirstOrDefault ();
			if ( keyUnique == default ) return;
			m_eventHandlerUniqueMap.Remove ( keyUnique );
		}

		/// <summary>
		/// Get registered event handler which have unique passed in parameter.
		/// Unique for event handler must be defined as override GetUnique method and return some value.
		/// If it will be two or more event handlers with same unique value you get first and order is not guaranteed.
		/// </summary>
		/// <param name="unique">Event handler with unique value.</param>
		public SciterEventHandler? GetEventHandlerByUnique ( string unique ) {
			if ( !m_eventHandlerUniqueMap.ContainsKey ( unique ) ) return null;

			return GetEventHandlerByPointer ( m_eventHandlerUniqueMap[unique] );
		}

		/// <summary>
		/// Get a registered event handler whose SubscribedElement is equal to the passed pointer parameter.
		/// </summary>
		/// <param name="pointer">The pointer for which to find the event handler.</param>
		public SciterEventHandler? GetEventHandlerByPointer ( nint pointer ) {
			if ( !m_eventHandlerMap.ContainsKey ( pointer ) ) return null;

			return m_eventHandlerMap[pointer];
		}

		/// <summary>
		/// Get registered event handlers which have prefix passed in parameter inside unique value.
		/// </summary>
		/// <param name="uniquePrefix">Prefix which need to matches.</param>
		public IEnumerable<SciterEventHandler> GetEventHandlerByUniquePrefix ( string uniquePrefix ) {
			var foundedItems = m_eventHandlerUniqueMap.Keys
				.Where ( a => a.StartsWith ( uniquePrefix ) )
				.ToList ();
			if ( !foundedItems.Any () ) return [];

			return foundedItems
				.Select ( a => GetEventHandlerByPointer ( m_eventHandlerUniqueMap[a] ) )
				.Where ( a => a != null )
				.Cast<SciterEventHandler> ()
				.ToList ();
		}

	}

}

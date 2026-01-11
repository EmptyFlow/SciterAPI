namespace EmptyFlow.SciterAPI {

	/// <summary>
	/// Generate unique keys for event handlers.
	/// </summary>
	public interface IUniqueEventHandler {

		/// <summary>
		/// Get unique key for handler.
		/// </summary>
		string GetUnique ();

	}

}

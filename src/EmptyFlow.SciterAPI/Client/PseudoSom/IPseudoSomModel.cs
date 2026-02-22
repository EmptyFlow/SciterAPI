namespace EmptyFlow.SciterAPI.Client.PseudoSom {
	public interface IPseudoSomModel {

		SciterValue GetPropetyValue ( string name );

		bool SetPropetyValue ( SciterValue value, string name );

		SciterValue CallMethod ( string name, IEnumerable<SciterValue> parameters );

		HashSet<string> GetProperties ();

		HashSet<string> GetMethods ();

		string GetModelName ();

	}

}

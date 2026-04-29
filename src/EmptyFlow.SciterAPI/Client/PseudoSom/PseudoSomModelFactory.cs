using System.Diagnostics.CodeAnalysis;

namespace EmptyFlow.SciterAPI.Client.PseudoSom {

	public static class PseudoSomModelFactory {

		public static InnerPseudoSomModel<T> Inner<[DynamicallyAccessedMembers ( DynamicallyAccessedMemberTypes.All )] T> ( T model, string modelName, SciterAPIHost host ) where T : class {
			return new InnerPseudoSomModel<T> ( model, modelName, host );
		}

	}

}

using System.Diagnostics.CodeAnalysis;

namespace EmptyFlow.SciterAPI.Client.PseudoSom {

	public class InnerPseudoSomModelHandler<[DynamicallyAccessedMembers ( DynamicallyAccessedMemberTypes.All )] T> : PseudoSomModelHandler where T : class {

		public InnerPseudoSomModelHandler ( T model, nint relatedThing, nint window, SciterAPIHost sciterAPIHost, string modelName ) :
			base ( PseudoSomModelFactory.Inner ( model, modelName, sciterAPIHost ), relatedThing, window, sciterAPIHost ) {
		}

	}

}

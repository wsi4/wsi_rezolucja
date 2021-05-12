using Resolution.Sentences;

namespace Resolution.Visitors.ConjunctionExFSM
{
    public class RootConjunctionExclusionState : IConjunctionExclusionState
    {
        private readonly IConjunctionExclusionFSM fsm;

        public RootConjunctionExclusionState(IConjunctionExclusionFSM fsm)
        {
            this.fsm = fsm;
        }

        public void ParseComplexSentence(ComplexSentence sentence)
        {
            throw new System.NotImplementedException();
        }

        public void ParseLiteral(Literal literal)
        {
            throw new System.NotImplementedException();
        }
    }
}
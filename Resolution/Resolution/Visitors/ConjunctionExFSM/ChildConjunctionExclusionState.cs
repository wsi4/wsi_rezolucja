using Resolution.Sentences;

namespace Resolution.Visitors.ConjunctionExFSM
{
    public class ChildConjunctionExclusionState : IConjunctionExclusionState
    {
        private readonly IConjunctionExclusionFSM fsm;

        public ChildConjunctionExclusionState(IConjunctionExclusionFSM fsm)
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
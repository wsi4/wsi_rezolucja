using Resolution.Sentences;

namespace Resolution.Visitors.ConjunctionExclusion
{
    public abstract class ConjunctionExclusionState
    {
        protected readonly IConjunctionExclusionFSM fsm;

        protected ConjunctionExclusionState(IConjunctionExclusionFSM fsm)
        {
            this.fsm = fsm;
        }

        public abstract void ParseComplexSentence(ComplexSentence sentence);

        public void ParseLiteral(Literal literal)
        {
            // literals don't cause state transition
            fsm.State = this;
        }
    }
}
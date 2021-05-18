using Resolution.Clauses;
using Resolution.Sentences;

namespace Resolution.Visitors.ClauseMaker
{
    public abstract class ClauseMakerState
    {
        protected readonly IClauseMakerFSM fsm;
        protected readonly ClauseCollectionBuilder clauseCollectionBuilder;

        protected ClauseMakerState(IClauseMakerFSM fsm, ClauseCollectionBuilder clauseCollectionBuilder)
        {
            this.fsm = fsm;
            this.clauseCollectionBuilder = clauseCollectionBuilder;
        }

        public abstract void ProcessComplexSentence(ComplexSentence sentence);

        public abstract void ProcessLiteral(Literal literal);
    }
}
using System;
using Resolution.Clauses;
using Resolution.Sentences;

namespace Resolution.Visitors.ClauseMaker
{
    public class LiteralLevelClauseMakerState : ClauseMakerState
    {
        private readonly ClauseMakerState parentState;

        // a number of literals to process, after which a parent state of fsm should be restored
        private readonly int literalCount;
        private int processedLiterals = 0;

        public LiteralLevelClauseMakerState(IClauseMakerFSM fsm, ClauseMakerState parentState,
            ClauseCollectionBuilder clauseCollectionBuilder, int literalCount)
            : base(fsm, clauseCollectionBuilder)
        {
            this.parentState = parentState;
            this.literalCount = literalCount;
        }

        public override void ProcessComplexSentence(ComplexSentence sentence)
        {
            throw new ArgumentException("Invalid sentence: expected literal");
        }

        public override void ProcessLiteral(Literal literal)
        {
            clauseCollectionBuilder.AddLiteral(literal);
            processedLiterals++;

            if (processedLiterals >= literalCount)
            {
                clauseCollectionBuilder.EndClause();
                fsm.State = parentState;
            }
            else
            {
                fsm.State = this;
            }
        }
    }
}
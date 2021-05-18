using System;
using Resolution.Clauses;
using Resolution.Sentences;

namespace Resolution.Visitors.ClauseMaker
{
    public class LiteralLevelClauseMakerState : ClauseMakerState
    {
        private readonly ClauseCollectionBuilder clauseCollectionBuilder;
        private readonly ClauseMakerState parentState;
        private readonly int literalCount;
        private int processedLiterals = 0;

        public LiteralLevelClauseMakerState(IClauseMakerFSM fsm, ClauseMakerState parentState,
            ClauseCollectionBuilder clauseCollectionBuilder, int literalCount)
            : base(fsm)
        {
            this.parentState = parentState;
            this.literalCount = literalCount;
            this.clauseCollectionBuilder = clauseCollectionBuilder;
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
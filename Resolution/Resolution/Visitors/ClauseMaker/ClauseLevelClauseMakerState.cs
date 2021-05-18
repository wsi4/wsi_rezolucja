using System;
using Resolution.Clauses;
using Resolution.Sentences;

namespace Resolution.Visitors.ClauseMaker
{
    public class ClauseLevelClauseMakerState : ClauseMakerState
    {
        private readonly ClauseMakerState parentState;
        private readonly ClauseCollectionBuilder clauseCollectionBuilder;
        private readonly int clauseCount;
        private int processedClauses = 0;

        public ClauseLevelClauseMakerState(IClauseMakerFSM fsm, ClauseMakerState parentState,
            ClauseCollectionBuilder clauseCollectionBuilder, int clauseCount)
            : base(fsm)
        {
            this.parentState = parentState;
            this.clauseCollectionBuilder = clauseCollectionBuilder;
            this.clauseCount = clauseCount;
        }

        public override void ProcessComplexSentence(ComplexSentence sentence)
        {
            CheckClauseLimit();

            if (sentence.Connective != Connective.OR)
                throw new ArgumentException("Invalid sentence: expected alternative");

            processedClauses++;
            fsm.State = new LiteralLevelClauseMakerState(fsm, this, clauseCollectionBuilder, sentence.Sentences.Length);
        }

        public override void ProcessLiteral(Literal literal)
        {
            CheckClauseLimit();
            clauseCollectionBuilder.AddClause(new Clause(new[] { literal }));
            processedClauses++;
            fsm.State = this;
        }

        private void CheckClauseLimit()
        {
            if (processedClauses >= clauseCount)
                fsm.State = parentState;
        }
    }
}
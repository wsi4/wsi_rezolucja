using System;
using Resolution.Clauses;
using Resolution.Sentences;

namespace Resolution.Visitors.ClauseMaker
{
    public class ClauseLevelClauseMakerState : ClauseMakerState
    {
        private readonly ClauseMakerState parentState;

        // a number of clauses to process, after which a parent state of fsm should be restored
        private readonly int clauseCount;
        private int processedClauses = 0;

        public ClauseLevelClauseMakerState(IClauseMakerFSM fsm, ClauseMakerState parentState,
            ClauseCollectionBuilder clauseCollectionBuilder, int clauseCount)
            : base(fsm, clauseCollectionBuilder)
        {
            this.parentState = parentState;
            this.clauseCount = clauseCount;
        }

        public override void ProcessComplexSentence(ComplexSentence sentence)
        {
            if (sentence.Connective != Connective.OR)
                throw new ArgumentException("Invalid sentence: expected alternative");

            processedClauses++;
            if (processedClauses > clauseCount)
            {
                fsm.State = parentState;
            }
            else
            {
                fsm.State = new LiteralLevelClauseMakerState(
                    fsm, this, clauseCollectionBuilder, sentence.Sentences.Length
                );
            }
        }

        // a case with a single literal
        public override void ProcessLiteral(Literal literal)
        {
            if (processedClauses >= clauseCount)
            {
                fsm.State = parentState;
                return;
            }

            clauseCollectionBuilder.AddClause(new Clause(new[] { literal }));
            processedClauses++;
            fsm.State = this;
        }
    }
}
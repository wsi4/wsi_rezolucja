using System;
using Resolution.Clauses;
using Resolution.Sentences;

namespace Resolution.Visitors.ClauseMaker
{
    public class ConjunctionLevelClauseMakerState : ClauseMakerState
    {
        public ConjunctionLevelClauseMakerState(IClauseMakerFSM fsm, ClauseCollectionBuilder clauseCollectionBuilder)
            : base(fsm, clauseCollectionBuilder)
        {
        }

        public override void ProcessComplexSentence(ComplexSentence sentence)
        {
            if (sentence.Connective == Connective.OR)
            {
                // the case with a single clause, we can use ClauseLevelClauseMakerState to do the job
                var clauseLevelState = new ClauseLevelClauseMakerState(fsm, this, clauseCollectionBuilder, 1);
                clauseLevelState.ProcessComplexSentence(sentence);
                return;
            }

            if (sentence.Connective != Connective.AND)
                throw new ArgumentException("Invalid sentence: expected conjunction of clauses or single clause");

            fsm.State = new ClauseLevelClauseMakerState(fsm, this, clauseCollectionBuilder, sentence.Sentences.Length);
        }

        public override void ProcessLiteral(Literal literal)
        {
            clauseCollectionBuilder.AddClause(new Clause(new[] {literal}));
        }
    }
}
using System;
using Resolution.Clauses;
using Resolution.Sentences;

namespace Resolution.Visitors.ClauseMaker
{
    public class ConjunctionLevelClauseMakerState : ClauseMakerState
    {
        private readonly ClauseCollectionBuilder clauseCollectionBuilder;

        public ConjunctionLevelClauseMakerState(IClauseMakerFSM fsm, ClauseCollectionBuilder clauseCollectionBuilder)
            : base(fsm)
        {
            this.clauseCollectionBuilder = clauseCollectionBuilder;
        }

        public override void ProcessComplexSentence(ComplexSentence sentence)
        {
            if (sentence.Connective != Connective.AND)
                throw new ArgumentException("Invalid sentence: expected conjunction");

            fsm.State = new ClauseLevelClauseMakerState(fsm, this, clauseCollectionBuilder, sentence.Sentences.Length);
        }

        public override void ProcessLiteral(Literal literal)
        {
            throw new ArgumentException("Invalid sentence: expected conjunction");
        }
    }
}
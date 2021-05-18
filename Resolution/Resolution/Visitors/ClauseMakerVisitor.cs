using System.Collections.Generic;
using Resolution.Clauses;
using Resolution.Sentences;
using Resolution.Visitors.ClauseMaker;

namespace Resolution.Visitors
{
    // this class is implemented using state pattern with 3 states:
    // 1) ConjunctionLevelClauseMakerState: expecting a sentence that is a conjunction of clauses or a single clause
    // 2) ClauseLevelClauseMakerState: expecting a sentence that is a single clause (may be a single literal)
    // 3) LiteralLevelClauseMakerState: expecting a single literal
    public class ClauseMakerVisitor : AbstractVisitor, IClauseMakerFSM
    {
        // this object is shared between all states, so that they build common clause collection
        private readonly ClauseCollectionBuilder clauseCollectionBuilder = new ClauseCollectionBuilder();

        public ClauseMakerState State { private get; set; }

        // this method is supposed to be used by a client in order to parse sentence to a list of clauses
        public List<Clause> CreateClauses(Sentence sentence)
        {
            clauseCollectionBuilder.Clear();
            State = new ConjunctionLevelClauseMakerState(this, clauseCollectionBuilder);

            Visit(sentence);
            return clauseCollectionBuilder.Build();
        }

        public override void VisitLiteral(Literal literal)
        {
            State.ProcessLiteral(literal);
        }

        public override void VisitComplex(ComplexSentence complex)
        {
            State.ProcessComplexSentence(complex);

            foreach (var sentence in complex.Sentences)
            {
                Visit(sentence);
            }
        }
    }
}
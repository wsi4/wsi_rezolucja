using System;
using System.Collections.Generic;
using Resolution.Clauses;
using Resolution.Sentences;
using Resolution.Visitors.ClauseMaker;

namespace Resolution.Visitors
{
    public class ClauseMakerVisitor : AbstractVisitor, IClauseMakerFSM
    {
        private readonly ClauseCollectionBuilder clauseCollectionBuilder = new ClauseCollectionBuilder();

        public ClauseMakerState State { private get; set; }

        public List<Clause> CreateClause(Sentence sentence)
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
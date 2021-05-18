using System;
using Resolution.Clauses;
using Resolution.Sentences;
using Resolution.Visitors;
using System.Collections.Generic;

namespace Resolution
{
    public class CNFConverter
    {
        public List<Clause> ConvertToCNF(Sentence sentence)
        {
            var removeImpl = new ImplicationRemovalVisitor();
            var conjunctionExcl = new ConjunctionExclusionVisitor();
            var clauseMaker = new ClauseMakerVisitor();

            removeImpl.Visit(sentence);
            Console.WriteLine(sentence);

            conjunctionExcl.Visit(sentence);
            Console.WriteLine( sentence );

            return clauseMaker.CreateClause(sentence);
        }
    }
}
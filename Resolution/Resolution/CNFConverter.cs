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
            var negationInwards = new MoveNegationInwardsVisitor();
            var conjunctionExcl = new ConjunctionExclusionVisitor();
            var unnest = new UnnestingVisitor();
            var clauseMaker = new ClauseMakerVisitor();

            removeImpl.Visit(sentence);
            unnest.Visit(sentence);
            Console.WriteLine(sentence);

            negationInwards.Visit(sentence);
            unnest.Visit(sentence);
            Console.WriteLine(sentence);

            conjunctionExcl.Visit(sentence);
            Console.WriteLine(sentence);

            return clauseMaker.CreateClauses(sentence);
        }
    }
}
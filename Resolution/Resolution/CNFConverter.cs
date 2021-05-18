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
            var clauseMaker = new ClauseMakerVisitor();

            removeImpl.Visit(sentence);
            negationInwards.Visit(sentence);
            conjunctionExcl.Visit(sentence);

            return clauseMaker.CreateClauses(sentence);
        }
    }
}
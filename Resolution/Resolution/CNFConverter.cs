using Resolution.Clauses;
using Resolution.Sentences;
using Resolution.Visitors;
using System;
using System.Collections.Generic;

namespace Resolution
{
    class CNFConverter
    {
        public List<Clause> ConvertToCNF(Sentence sentence)
        {
            var removeImpl = new ImplicationRemovalVisitor();
            var conjunctionExcl = new ConjunctionRemovalVisitor();

            removeImpl.Visit(sentence);
            conjunctionExcl.Visit(sentence);

            return ConvertSentenceToClauses(sentence);
        }

        private List<Clause> ConvertSentenceToClauses(Sentence s)
        {
            throw new NotImplementedException();
        }
    }
}

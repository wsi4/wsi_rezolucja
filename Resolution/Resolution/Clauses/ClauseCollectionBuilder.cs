using System.Collections.Generic;
using Resolution.Sentences;

namespace Resolution.Clauses
{
    public class ClauseCollectionBuilder
    {
        private readonly List<Clause> clauses = new List<Clause>();
        private readonly List<Literal> currentClauseLiterals = new List<Literal>();

        // add literal to a clause that is currently being constructed
        public virtual ClauseCollectionBuilder AddLiteral(Literal literal)
        {
            currentClauseLiterals.Add(literal);
            return this;
        }

        // build a clause from collected literals
        public virtual ClauseCollectionBuilder EndClause()
        {
            clauses.Add(new Clause(currentClauseLiterals));
            currentClauseLiterals.Clear();
            return this;
        }

        public virtual ClauseCollectionBuilder AddClause(Clause clause)
        {
            if (currentClauseLiterals.Count > 0)
                EndClause();

            clauses.Add(clause);
            return this;
        }

        public virtual List<Clause> Build()
        {
            if (currentClauseLiterals.Count > 0)
                clauses.Add(new Clause(currentClauseLiterals));
            return new List<Clause>(clauses);
        }

        public virtual void Clear()
        {
            clauses.Clear();
            currentClauseLiterals.Clear();
        }
    }
}
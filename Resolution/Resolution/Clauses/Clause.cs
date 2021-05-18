using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resolution.Sentences;

namespace Resolution.Clauses
{
    public class Clause
    {
        public List<Literal> PositiveLiterals { get; }

        public List<Literal> NegativeLiterals { get; }

        public Clause(IReadOnlyCollection<Literal> literals)
        {
            PositiveLiterals = new List<Literal>(literals.Where(l => !l.Negated));
            NegativeLiterals = new List<Literal>(literals.Where(l => l.Negated));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            PositiveLiterals.ForEach(l => builder.Append(l).Append(" | "));
            NegativeLiterals.ForEach(l => builder.Append(l).Append(" | "));
            builder.Remove(builder.Length - 3, 3); // remove last 'or'
            return builder.ToString();
        }
    }
}
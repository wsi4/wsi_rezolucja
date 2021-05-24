using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resolution.Sentences;

namespace Resolution.Clauses
{
    public class Clause : IEquatable<Clause>
    {
        public HashSet<Literal> PositiveLiterals { get; }
        public HashSet<Literal> NegativeLiterals { get; }

        public IEnumerable<Literal> Literals
        {
            get => PositiveLiterals.Concat(NegativeLiterals);
        }

        public bool Empty
        {
            get => PositiveLiterals.Count + NegativeLiterals.Count == 0;
        }

        public Clause()
        {
            PositiveLiterals = new HashSet<Literal>();
            NegativeLiterals = new HashSet<Literal>();
        }

        public Clause(IReadOnlyCollection<Literal> literals)
        {
            PositiveLiterals = new HashSet<Literal>(literals.Where(l => !l.Negated));
            NegativeLiterals = new HashSet<Literal>(literals.Where(l => l.Negated));
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            foreach (var positiveLiteral in PositiveLiterals)
            {
                hash.Add(positiveLiteral);
            }
            foreach (var negativeLiteral in NegativeLiterals)
            {
                hash.Add(negativeLiteral);
            }
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var l in PositiveLiterals)
            {
                builder.Append(l).Append(" | ");
            }
            foreach (var l in NegativeLiterals)
            {
                builder.Append(l).Append(" | ");
            }
            builder.Remove(builder.Length - 3, 3); // remove last 'or'
            return builder.ToString();
        }

        public bool Equals(Clause other)
        {
            return PositiveLiterals.SetEquals(other.PositiveLiterals)
                && NegativeLiterals.SetEquals(other.NegativeLiterals);
        }

        public override bool Equals(object obj)
        {
            return obj is Clause clauseObj && Equals(clauseObj);
        }
    }
}

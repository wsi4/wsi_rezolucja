using Resolution.Visitors;
using System;

namespace Resolution.Sentences
{
    public class Literal : Sentence
    {
        public Literal(string symbol, bool negated = false)
        {
            Symbol = symbol;
            Negated = negated;
        }

        public string Symbol { get; }

        public override void Accept(AbstractVisitor visitor)
        {
            visitor.VisitLiteral(this);
        }

        public override object Clone()
        {
            Literal literal = new(Symbol);

            if (literal.Negated != Negated)
            {
                literal.Negate();
            }

            return literal;
        }

        public override bool Equals(Sentence other)
        {
            if (other is not Literal x)
            {
                return false;
            }

            return x.Symbol == Symbol && x.Negated == Negated;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Symbol, Negated);
        }

        public override string ToString()
        {
            return $"{(Negated ? "~" : "")}{Symbol}";
        }
    }
}
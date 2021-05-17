using Resolution.Visitors;

namespace Resolution.Sentences
{
    public class Literal : Sentence
    {
        public Literal(string symbol)
        {
            Symbol = symbol;
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

        public override string ToString()
        {
            return $"{(Negated ? "~" : "")}{Symbol}";
        }
    }
}
namespace Resolution.Sentences
{
    public class Connective
    {
        public int Precedence { get; }
        public string Symbol { get; }

        private Connective(int precedence, string symbol)
        {
            Precedence = precedence;
            Symbol = symbol;
        }

        public readonly static Connective NOT = new(10, "~");
        public readonly static Connective AND = new(8, "&");
        public readonly static Connective OR = new(6, "|");
        public readonly static Connective IMPLICATION = new(4, "=>");
        public readonly static Connective BICONDITIONAL = new(2, "<=>");

        public override string ToString()
        {
            return Symbol;
        }
    }
}

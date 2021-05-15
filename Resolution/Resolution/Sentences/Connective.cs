using System;

namespace Resolution.Sentences
{
    public class Connective : IEquatable<Connective>
    {
        public int Precedence { get; }
        public string Symbol { get; }

        private Connective(int precedence, string symbol)
        {
            Precedence = precedence;
            Symbol = symbol;
        }

        public readonly static Connective AND = new(8, "&");
        public readonly static Connective OR = new(6, "|");
        public readonly static Connective IMPLICATION = new(4, "=>");
        public readonly static Connective BICONDITIONAL = new(2, "<=>");


        public static bool operator ==(Connective first, Connective second)
        {
            if (first is null && second is null) return true;
            if (first is null || second is null) return false;
            return first.Symbol == second.Symbol && first.Precedence == second.Precedence;
        }

        public bool Equals(Connective other) => this == other;
        public override int GetHashCode() => base.GetHashCode();
        public static bool operator !=(Connective first, Connective second) => !(first == second);

        public override bool Equals(object obj)
        {
            return Equals(obj as Connective);
        }
    }
}
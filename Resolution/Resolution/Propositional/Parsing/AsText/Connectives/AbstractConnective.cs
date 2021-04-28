using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Propositional.Parsing.AsText.Connectives
{
    public abstract class AbstractConnective
    {
        public string Symbol { get; }
        public int Precedence { get; }

        public AbstractConnective(string symbol, int precedence)
        {
            Symbol = symbol;
            Precedence = precedence;
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}

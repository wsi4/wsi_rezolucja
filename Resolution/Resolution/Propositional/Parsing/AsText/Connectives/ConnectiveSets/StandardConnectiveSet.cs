using System;
using System.Collections.Generic;

namespace Resolution.Propositional.Parsing.AsText.Connectives.ConnectiveSets
{
    public class StandardConnectiveSet : IConnectiveSet
    {
        public StandardConnectiveSet()
        {
            var and = new And();
            var or = new Or();
            var not = new Not();
            var imp = new Implication();
            var bicond = new Biconditional();

            Connectives = new Dictionary<string, AbstractConnective>()
            {
                { and.Symbol, and },
                { or.Symbol, or },
                { not.Symbol, not },
                { imp.Symbol, imp },
                { bicond.Symbol, bicond }
            };
        }

        public IDictionary<string, AbstractConnective> Connectives { get; }

        public AbstractConnective Get(string symbol)
        {
            try
            {
                return Connectives[symbol];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException($"Not a valid symbol for a connective: {symbol}");
            }
        }

        public bool IsConnective(string symbol)
        {
            return Connectives.ContainsKey(symbol);
        }
    }
}

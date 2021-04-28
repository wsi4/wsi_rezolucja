using System.Collections.Generic;

namespace Resolution.Propositional.Parsing.AsText.Connectives.ConnectiveSets
{
    public interface IConnectiveSet
    {
        IDictionary<string, AbstractConnective> Connectives { get; }
        bool IsConnective(string symbol);
        AbstractConnective Get(string symbol);
    }
}

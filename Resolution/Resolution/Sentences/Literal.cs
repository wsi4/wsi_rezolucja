using Resolution.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            visitor.Visit(this);
        }
    }
}

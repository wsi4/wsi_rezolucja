using Resolution.Sentences;
using System;

namespace Resolution.Visitors
{
    class ConjunctionRemovalVisitor : AbstractVisitor
    {
        public override void Visit(Literal literal)
        {
            throw new NotImplementedException();
        }

        public override void Visit(ComplexSentence complex)
        {
            throw new NotImplementedException();
        }
    }
}

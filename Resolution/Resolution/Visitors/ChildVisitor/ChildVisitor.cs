using Resolution.Sentences;
using System.Collections.Generic;

namespace Resolution.Visitors
{
    class ChildVisitor : AbstractVisitor
    {
        public ComplexSentence Parent { get; }

        public ChildVisitor(ComplexSentence parent)
        {
            Parent = parent;
        }
        public override void VisitLiteral(Literal literal)
        {
            return;
        }

        public override void VisitComplex(ComplexSentence complex)
        {
            if (complex.Connective == Parent.Connective)
            {
                List<Sentence> connectedList = new(Parent.Sentences);
                connectedList.AddRange(complex.Sentences);
                connectedList.Remove(complex);
                Parent.Sentences = connectedList.ToArray();
            }
        }
    }
}

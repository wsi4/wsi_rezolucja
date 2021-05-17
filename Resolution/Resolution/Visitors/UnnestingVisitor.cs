using Resolution.Sentences;

namespace Resolution.Visitors
{
    public class UnnestingVisitor : AbstractVisitor
    {
        public override void VisitLiteral(Literal literal)
        {
            return;
        }

        public override void VisitComplex(ComplexSentence complex)
        {
            foreach (var sentence in complex.Sentences)
            {
                Visit(sentence);
            }

            if (complex.Connective == Connective.IMPLICATION)
            {
                return;
            }

            ChildVisitor childVisitor = new(complex);

            foreach (var sentence in complex.Sentences)
            {
                childVisitor.Visit(sentence);
            }

        }
    }
}

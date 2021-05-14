using Resolution.Sentences;

namespace Resolution.Visitors
{
    public class UnnestingVisitor : AbstractVisitor
    {
        public override void Visit(Literal literal)
        {
            return;
        }

        public override void Visit(ComplexSentence complex)
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

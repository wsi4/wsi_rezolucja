using Resolution.Sentences;

namespace Resolution.Visitors
{
    public class ImplicationRemovalVisitor : AbstractVisitor
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

            if (complex.Connective != Connective.IMPLICATION)
            {
                return;
            }

            complex.Connective = Connective.OR;

            for (int i = 0; i < complex.Sentences.Length - 1; i++)
            {
                complex.Sentences[i].Negate();
            }
        }
    }
}

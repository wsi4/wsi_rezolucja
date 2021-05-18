using Resolution.Sentences;
using System;

namespace Resolution.Visitors
{
    public class MoveNegationInwardsVisitor : AbstractVisitor
    {
        public override void VisitLiteral(Literal literal)
        {
            return;
        }

        public override void VisitComplex(ComplexSentence complex)
        {
            if(complex.Negated)
            {
                if(complex.Connective == Connective.IMPLICATION)
                {
                    throw new ApplicationException("Implication should be removed at this point");
                }
                else if(complex.Connective == Connective.AND)
                {
                    complex.Connective = Connective.OR;
                    foreach (var sentence in complex.Sentences)
                    {
                        sentence.Negate();
                    }
                }
                else if(complex.Connective == Connective.OR)
                {
                    complex.Connective = Connective.AND;
                    foreach (var sentence in complex.Sentences)
                    {
                        sentence.Negate();
                    }
                }
                else if (complex.Connective == Connective.BICONDITIONAL) // ~(a <=> b <=> c)    =     ~(a^b^c)^(avbvc)
                {
                    var clonedSentences = new Sentence[complex.Sentences.Length];
                    for (int i = 0; i < complex.Sentences.Length; i++)
                    {
                        clonedSentences[i] = complex.Sentences[i].Clone() as Sentence;
                    }
                    var first = new ComplexSentence(Connective.AND, clonedSentences);
                    first.Negate();
                    var second = new ComplexSentence(Connective.OR, complex.Sentences);
                    complex.Sentences = new[] { first, second };
                    complex.Connective = Connective.AND;
                }
                complex.Negate();

            }
            foreach (var sentence in complex.Sentences)
            {
                Visit(sentence);
            }
        }
    }
}

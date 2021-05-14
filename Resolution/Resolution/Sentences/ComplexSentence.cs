using Resolution.Visitors;
using System.Collections.Generic;

namespace Resolution.Sentences
{
    public class ComplexSentence : Sentence
    {
        public ComplexSentence(Connective connective, params Sentence[] sentences)
        {
            Sentences = sentences.Clone() as Sentence[];
            Connective = connective;
        }
        public Sentence[] Sentences { get; set; }
        public Connective Connective { get; set; }
        public override void Accept(AbstractVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override object Clone()
        {
            return new ComplexSentence(Connective, Sentences);
        }

        public override bool Equals(Sentence other)
        {
            if (other is not ComplexSentence x || Connective != x.Connective)
            {
                return false;
            }

            if (Connective == Connective.IMPLICATION)
            {
                return Negated == x.Negated && x.Sentences.Equals(Sentences);
            }
            else
            {
                var asList = new List<Sentence>(x.Sentences);

                foreach (var element in Sentences)
                {
                    if (!asList.Contains(element))
                    {
                        return false;
                    }
                }

                return Negated == x.Negated;
            }
        }
    }
}

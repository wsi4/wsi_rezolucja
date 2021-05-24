using Resolution.Visitors;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            visitor.VisitComplex(this);
        }

        public override object Clone()
        {
            var clonedSectences = new Sentence[Sentences.Length];
            for (int i = 0; i < Sentences.Length; i++)
            {
                clonedSectences[i] = Sentences[i].Clone() as Sentence;
            }
            var ret = new ComplexSentence(Connective, clonedSectences);

            if (ret.Negated != Negated)
            {
                ret.Negate();
            }

            return ret;
        }

        public override bool Equals(Sentence other)
        {
            if (other is not ComplexSentence x || Connective != x.Connective)
            {
                return false;
            }

            foreach (var element in Sentences)
            {
                if (!x.Sentences.Contains(element))
                {
                    return false;
                }
            }

            return Negated == x.Negated;
        }

        public override string ToString()
        {
            if (Sentences.Length == 0)
                return "";

            var strBuilder = new StringBuilder( Sentences[0].ToString() );
            for (int i = 1; i < Sentences.Length; i++)
            {
                strBuilder.Append( $" {Connective} {Sentences[i]}" );
            }

            return $"{(Negated ? "~" : "")}({strBuilder})";
        }
    }
}
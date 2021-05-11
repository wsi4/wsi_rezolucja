using Resolution.Visitors;

namespace Resolution.Sentences
{
    public class ComplexSentence : Sentence
    {
        public ComplexSentence(Connective connective, params Sentence[] sentences)
        {
            Sentences = sentences.Clone() as Sentence[];
            Connective = connective;
        }
        public Sentence[] Sentences { get; }
        public Connective Connective { get; }
        public override void Accept(AbstractVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

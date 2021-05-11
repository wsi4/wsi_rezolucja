using Resolution.Visitors;

namespace Resolution.Sentences
{
    public abstract class Sentence
    {
        public bool Negated { get; set; }
        public abstract void Accept(AbstractVisitor visitor);
    }
}

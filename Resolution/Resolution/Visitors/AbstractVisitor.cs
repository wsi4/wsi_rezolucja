using Resolution.Sentences;

namespace Resolution.Visitors
{
    public abstract class AbstractVisitor
    {
        public virtual void Visit(Sentence sentence)
        {
            sentence.Accept(this);
        }

        public abstract void VisitLiteral(Literal literal);
        public abstract void VisitComplex(ComplexSentence complex);
    }
}

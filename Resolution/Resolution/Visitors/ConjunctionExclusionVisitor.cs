using Resolution.Sentences;
using Resolution.Visitors.ConjunctionExFSM;

namespace Resolution.Visitors
{
    // Sentences that need changing here are of type `s = (p AND q) OR r`,
    // any other form means that the sentence does not require any more processing or is invalid.
    // Let's call `s` a 'root' sentence and `(p AND q)` - a 'child' sentence.
    // 'root' and 'child' have to be parsed differently.
    // The 'root-child' relation is recursive (`p` and `q` in 'child' may be 'roots' as well).
    // Therefore, this class is implemented as a state machine with two states:
    // 1) expecting 'root' sentence
    // 2) expecting 'child' sentence
    class ConjunctionRemovalVisitor : AbstractVisitor, IConjunctionExclusionFSM
    {
        public IConjunctionExclusionState State { private get; set; }

        public ConjunctionRemovalVisitor()
        {
            State = new RootConjunctionExclusionState(this);
        }

        public override void Visit(Literal literal)
        {
            State.ParseLiteral(literal);
        }

        public override void Visit(ComplexSentence complex)
        {
            foreach (var subSentence in complex.Sentences)
            {
                Visit(subSentence);
            }

            State.ParseComplexSentence(complex);
        }
    }
}
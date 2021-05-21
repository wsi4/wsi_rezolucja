using Resolution.Sentences;
using Resolution.Visitors.ConjunctionExclusion;

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
    public class ConjunctionExclusionVisitor : AbstractVisitor, IConjunctionExclusionFSM
    {
        private readonly ConjunctionDetectionVisitor conjunctionDetector = new ConjunctionDetectionVisitor();
        private readonly UnnestingVisitor unnestingVisitor = new UnnestingVisitor();

        public ConjunctionExclusionState State { private get; set; }

        // this method is only called by the client;
        public override void Visit(Sentence sentence)
        {
            // ensure that before parsing new sentence the state is 'child'
            State = new ChildConjunctionExclusionState(this);
            unnestingVisitor.Visit(sentence);

            while (conjunctionDetector.DetectConjunction(sentence))
            {
                base.Visit(sentence);
                unnestingVisitor.Visit(sentence);
            }
        }

        public override void VisitLiteral(Literal literal)
        {
            State.ParseLiteral(literal);
        }

        public override void VisitComplex(ComplexSentence complex)
        {
            foreach (var subSentence in complex.Sentences)
            {
                VisitInternal(subSentence);
            }

            State.ParseComplexSentence(complex);
        }

        // this method is used to perform recursive visiting,
        // so that the `VisitLiteral` method can reset state every time client wants to parse new sentence
        private void VisitInternal(Sentence sentence)
        {
            base.Visit(sentence);
        }
    }
}
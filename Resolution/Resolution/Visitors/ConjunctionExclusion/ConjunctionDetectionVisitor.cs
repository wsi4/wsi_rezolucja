using Resolution.Sentences;

namespace Resolution.Visitors.ConjunctionExclusion
{
    public class ConjunctionDetectionVisitor : AbstractVisitor
    {
        private bool conjunctionDetected = false;
        private bool isRootSentence;

        public bool DetectConjunction(Sentence sentence)
        {
            isRootSentence = true;
            conjunctionDetected = false;
            Visit(sentence);
            return conjunctionDetected;
        }

        public override void Visit(Literal literal) {}

        public override void Visit(ComplexSentence complex)
        {
            // allow conjunction in root level sentence, cause we want conjnunction of clauses
            if (!isRootSentence && complex.Connective == Connective.AND)
            {
                conjunctionDetected = true;
                return;
            }

            isRootSentence = false;

            foreach (var sentence in complex.Sentences)
            {
                Visit(sentence);
            }
        }
    }
}
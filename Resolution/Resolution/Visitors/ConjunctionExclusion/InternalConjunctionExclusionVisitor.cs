﻿using Resolution.Sentences;

namespace Resolution.Visitors.ConjunctionExclusion
{
    // Sentences that need changing here are of type `s = (p AND q) OR r`,
    // any other form means that the sentence does not require any more processing or is invalid.
    // Let's call `s` a 'root' sentence and `(p AND q)` - a 'child' sentence.
    // 'root' and 'child' have to be parsed differently.
    // The 'root-child' relation is recursive (`p` and `q` in 'child' may be 'roots' as well).
    // Therefore, this class is implemented as a state machine with two states:
    // 1) expecting 'root' sentence
    // 2) expecting 'child' sentence
    public class InternalConjunctionExclusionVisitor : AbstractVisitor, IConjunctionExclusionFSM
    {
        public ConjunctionExclusionState State { private get; set; }

        // this method is only called by the client;
        public override void Visit(Sentence sentence)
        {
            // ensure that before parsing new sentence the state is 'child'
            State = new ChildConjunctionExclusionState(this);
            base.Visit(sentence);
        }

        public override void Visit(Literal literal)
        {
            State.ParseLiteral(literal);
        }

        public override void Visit(ComplexSentence complex)
        {
            foreach (var subSentence in complex.Sentences)
            {
                VisitInternal(subSentence);
            }

            State.ParseComplexSentence(complex);
        }

        // this method is used to perform recursive visiting,
        // so that the `Visit` method can reset state every time client wants to parse new sentence
        private void VisitInternal(Sentence sentence)
        {
            base.Visit(sentence);
        }
    }
}
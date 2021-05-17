using System;
using Resolution.Sentences;

namespace Resolution.Visitors.ConjunctionExclusion
{
    public class ChildConjunctionExclusionState : ConjunctionExclusionState
    {
        public ChildConjunctionExclusionState(IConjunctionExclusionFSM fsm)
            : base(fsm) {}

        public override void ParseComplexSentence(ComplexSentence sentence)
        {
            if (sentence.Connective == Connective.OR)
            {
                // sentence does not need parsing
                fsm.State = this;
                return;
            }

            if (sentence.Connective == Connective.AND)
            {
                // pass sentence to root state
                fsm.State = new RootConjunctionExclusionState(fsm, sentence);
                return;
            }

            // 'child' sentence has to be conjunction
            throw new ArgumentException($"Expecting conjunction, got '{sentence.Connective}' sentence");
        }
    }
}
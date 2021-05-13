using System;
using System.Collections.Generic;
using System.Linq;
using Resolution.Sentences;

namespace Resolution.Visitors.ConjunctionExclusion
{
    public class RootConjunctionExclusionState : ConjunctionExclusionState
    {
        private readonly ComplexSentence subConjunction;

        public RootConjunctionExclusionState(IConjunctionExclusionFSM fsm, ComplexSentence subConjunction)
            : base(fsm)
        {
            this.subConjunction = subConjunction;
        }

        public override void ParseComplexSentence(ComplexSentence sentence)
        {
            if (sentence.Connective == Connective.AND)
            {
                // ignore any sub-conjunction after one was detected - the one passed in constructor
                fsm.State = this;
                return;
            }

            if (sentence.Connective != Connective.OR)
            {
                // 'root' sentence has to be alternative
                throw new ArgumentException($"Excpecting alternative, got '{sentence.Connective}' sentence");
            }

            var filteredSubSentences = sentence.Sentences.Where(s => !s.Equals(subConjunction));
            var modifiesSentences = subConjunction.Sentences.Select(s =>
            {
                var sentences = new List<Sentence>() { s };
                sentences.AddRange(filteredSubSentences);
                return new ComplexSentence(Connective.OR, sentences.ToArray());
            });

            sentence.Connective = Connective.AND;
            sentence.Sentences = modifiesSentences.ToArray();

            fsm.State = new ChildConjunctionExclusionState(fsm);
        }
    }
}
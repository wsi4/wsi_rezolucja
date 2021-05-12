using Resolution.Sentences;

namespace Resolution.Visitors.ConjunctionExFSM
{
    public interface IConjunctionExclusionState
    {
        void ParseComplexSentence(ComplexSentence sentence);

        void ParseLiteral(Literal literal);
    }
}
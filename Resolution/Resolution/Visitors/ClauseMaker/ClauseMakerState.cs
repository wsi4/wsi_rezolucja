using Resolution.Sentences;

namespace Resolution.Visitors.ClauseMaker
{
    public abstract class ClauseMakerState
    {
        protected readonly IClauseMakerFSM fsm;

        protected ClauseMakerState(IClauseMakerFSM fsm)
        {
            this.fsm = fsm;
        }

        public abstract void ProcessComplexSentence(ComplexSentence sentence);

        public abstract void ProcessLiteral(Literal literal);
    }
}
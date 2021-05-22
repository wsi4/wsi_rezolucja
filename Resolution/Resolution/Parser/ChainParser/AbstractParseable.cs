namespace Resolution.Parser.ChainParser
{
    public abstract class AbstractParseable : IParseable
    {
        private IParseable next { get; set; } = null;
        public IParseable Next(IParseable next)
        {
            this.next = next;
            return this.next;
        }

        protected abstract ParsedValue CheckRecognision(string text);
        public ParsedValue Recognise(string text)
        {
            var tmp = CheckRecognision(text);
            if (tmp.Recognised == RecognisedValue.NotRecognised &&
                this.next is not null)
                return this.next.Recognise(text);
            return tmp;
        }
    }
}

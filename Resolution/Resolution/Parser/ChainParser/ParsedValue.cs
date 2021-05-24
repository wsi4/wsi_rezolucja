namespace Resolution.Parser.ChainParser
{
    public class ParsedValue
    {
        public RecognisedValue Recognised { get; set; } = RecognisedValue.NotRecognised;
        public Sentences.Sentence Identifier { get; set; } = null;
        public Sentences.Connective Connective { get; set; } = null;
        public string Text { get; set; }
        public ParsedValue(string text)
        {
            this.Text = text;
        }
    }
}

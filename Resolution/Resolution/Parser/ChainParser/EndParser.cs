using Resolution.Parser.Exceptions;
using Resolution.Sentences;

namespace Resolution.Parser.ChainParser.Keywords
{
    internal class EndParser : AbstractParseable
    {
        protected override ParsedValue CheckRecognision(string text)
        {
            text = text.Trim();
            if (!text.StartsWith(";"))
                return new ParsedValue(text);

            if (text.Length > 1)
                throw new ParsingException("sentence should ended with single ';' ");
            
            return new ParsedValue(string.Empty)
            {
                Recognised = RecognisedValue.EndOfSentence
            };   
        }
    }
}

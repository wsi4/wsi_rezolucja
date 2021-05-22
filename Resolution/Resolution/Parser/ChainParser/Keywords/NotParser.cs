using Resolution.Parser.Exceptions;
using Resolution.Sentences;

namespace Resolution.Parser.ChainParser.Keywords
{
    internal class NotParser : AbstractParseable
    {
        protected override ParsedValue CheckRecognision(string text)
        {
            string checkedValue = "not";
            text = text.Trim();
            if (!text.StartsWith(checkedValue))
                return new ParsedValue(text);
            text = text.Substring(checkedValue.Length);
            int tmp = text.IndexOf(")");
            if (!(text.StartsWith("(") && tmp < text.Length && tmp > 1))
                throw new ParsingException(" 'not' have to be followed with not empty or whitespaced identifier");
            string identifierSymbol = text.Substring(1, tmp - 1).Trim();
            if(string.IsNullOrEmpty(identifierSymbol) || string.IsNullOrWhiteSpace(identifierSymbol))
                throw new ParsingException(" 'not' have to be followed with not empty or whitespaced identifier");
            var identifier = new Literal(identifierSymbol);
            identifier.Negate();
            return new ParsedValue(text.Substring(tmp+1))
            {
                Recognised = RecognisedValue.SentenceTyped,
                Identifier = identifier
            };   
        }
    }
}

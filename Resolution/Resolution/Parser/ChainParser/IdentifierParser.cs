using Resolution.Parser.Exceptions;
using Resolution.Sentences;

namespace Resolution.Parser.ChainParser.Keywords
{
    internal class IdentifierParser : AbstractParseable
    {
        protected override ParsedValue CheckRecognision(string text)
        {
            text = text.Trim();
            if (!text.StartsWith("("))
                return new ParsedValue(text);

            int tmp = text.IndexOf(")");
            if (!(text.StartsWith("(") && tmp < text.Length && tmp > 1))
                throw new ParsingException("identifier need to be not empty or whitespaced");
            string identifierSymbol = text.Substring(1, tmp - 1).Trim();

            if(string.IsNullOrEmpty(identifierSymbol) || string.IsNullOrWhiteSpace(identifierSymbol))
                throw new ParsingException("identifier need to be not empty or whitespaced");

            return new ParsedValue(text.Substring(tmp+1))
            {
                Recognised = RecognisedValue.SentenceTyped,
                Identifier = new Literal(identifierSymbol)
            };   
        }
    }
}

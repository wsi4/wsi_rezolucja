using Resolution.Sentences;

namespace Resolution.Parser.ChainParser.Keywords
{
    public class BiconParser : AbstractParseable
    {
        protected override ParsedValue CheckRecognision(string text)
        {
            string checkedValue = "bicon";
            text = text.Trim();
            if (!text.StartsWith(checkedValue))
                return new ParsedValue(text);
            return new ParsedValue(text.Substring(checkedValue.Length))
            {
                Recognised = RecognisedValue.Keyword,
                Connective = Connective.BICONDITIONAL
            };
        }
    }
}

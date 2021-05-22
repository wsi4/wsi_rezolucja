using Resolution.Sentences;

namespace Resolution.Parser.ChainParser.Keywords
{
    public class ImpParser : AbstractParseable
    {
        protected override ParsedValue CheckRecognision(string text)
        {
            string checkedValue = "imp";
            text = text.Trim();
            if (!text.StartsWith(checkedValue))
                return new ParsedValue(text);
            return new ParsedValue(text.Substring(checkedValue.Length))
            {
                Recognised = RecognisedValue.Keyword,
                Connective = Connective.IMPLICATION
            };
        }
    }
}

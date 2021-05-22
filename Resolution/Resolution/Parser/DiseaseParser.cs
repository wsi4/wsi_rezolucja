using Resolution.Parser.ChainParser;
using Resolution.Sentences;

namespace Resolution.Parser
{
    public static class DiseaseParser
    {
        public static Sentence Parse(string text)
        {
            ISentenceParseable mainParser = new RecurSentenceParser();
            return mainParser.ParseSentence(text);
        }
    }
}

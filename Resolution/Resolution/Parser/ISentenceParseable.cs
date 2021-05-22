using Resolution.Sentences;

namespace Resolution.Parser
{
    interface ISentenceParseable
    {
        Sentence ParseSentence(string text);
    }
}

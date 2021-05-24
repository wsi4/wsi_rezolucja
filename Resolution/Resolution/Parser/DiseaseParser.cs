using Resolution.Parser.ChainParser;
using Resolution.Parser.Exceptions;
using Resolution.Sentences;
using System.Collections.Generic;
using System.Text;

namespace Resolution.Parser
{
    public static class DiseaseParser
    {
        public static Sentence Parse(string text)
        {
            ISentenceParseable mainParser = new RecursiveSentenceParser();
            return mainParser.ParseSentence(text);
        }

        public static IEnumerable<Sentence> SetParser(string text)
        {
            var sentences = text.Split(';');
            var parsedSentences = new List<Sentence>();
            StringBuilder errors = new StringBuilder();
            for (int i = 0; i < sentences.Length; i++)
            {
                if (string.IsNullOrEmpty(sentences[i]))
                    continue;
                Sentence tmpParsed = null;
                try
                {
                    tmpParsed = Parse(sentences[i] + ';');
                    parsedSentences.Add(tmpParsed);
                }
                catch (ParsingException e)
                {
                    errors.Append(e.Message);
                }
            }
            if (errors.Length > 1)
                throw new System.Exception(errors.ToString());

            return parsedSentences;
        }
    }
}

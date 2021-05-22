using Resolution.Parser.ChainParser;
using Resolution.Parser.Exceptions;
using Resolution.Sentences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Parser
{
    static class DiseaseParser
    {
        public static Sentence Parse(string text)
        {
            // keyword
            // identifier
            // get disease name
            IParseable parsingChain = DiseaseParsingChain.GetChain();

            List<ParsedValue> parsedText = new List<ParsedValue>();
            ParsedValue tmp = new ParsedValue(string.Empty);
            while(tmp.Recognised != RecognisedValue.EndOfSentence)
            {
                tmp = parsingChain.Recognise(text);
                if(tmp.Recognised == RecognisedValue.NotRecognised)
                    throw new ParsingException("not recognised formula");

                text = tmp.Text;
                if(tmp.Recognised != RecognisedValue.EndOfSentence)
                    parsedText.Add(tmp);
            }

            if (parsedText.Count < 1)
                return null;

            if (parsedText[0].Recognised == RecognisedValue.Keyword)
                throw new ParsingException("sentence cannot start with non-unary keyword");
            if (parsedText.Count < 2)
                return parsedText[0].Identifier;

            List<Sentence> tmpList = new List<Sentence>();
            tmpList.Add(parsedText[0].Identifier);

            Connective currentConnective = null;
            for (int i = 2; i < parsedText.Count; i++)
            {
                var tmpFor = parsedText[i];
                if (tmpFor.Recognised == RecognisedValue.Keyword &&
                    currentConnective == null)
                {
                    currentConnective = tmpFor.Connective;
                    continue;
                }

                if (tmpFor.Recognised == RecognisedValue.Keyword &&
                    currentConnective != tmpFor.Connective)
                {
                    if (tmpList.Count < 2)
                        throw new ParsingException("There cannot be 2 non-unary keywords after each other");
                    ComplexSentence complexSentence = new ComplexSentence(currentConnective, tmpList.ToArray());
                    tmpList.Clear();
                    tmpList.Add(complexSentence);
                    currentConnective = tmpFor.Connective;
                    continue;
                }

                if(tmpFor.Recognised == RecognisedValue.SentenceTyped)
                    tmpList.Add(tmpList[i]);
            }

            if(tmpList.Count >= 2)
            {
                ComplexSentence complexSentence = new ComplexSentence(currentConnective, tmpList.ToArray());
                return complexSentence;
            }

            if (tmpList.Count < 2 && tmpList.Count != 0)
                throw new ParsingException("sentence cannot end with non-unary keyword");

            return null;
        }
    }
}

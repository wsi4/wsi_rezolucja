using Resolution.Parser.Exceptions;
using Resolution.Parser;
using Resolution.Sentences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Parser.ChainParser
{
    class RecurSentenceParser : AbstractParseable, ISentenceParseable
    {
        private char start = '<';
        private char end = '>';

        private int? PairsClosure(string text)
        {
            int unclosed = 0;
            int? indexOfClosed = null;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == this.start)
                    unclosed++;

                if (text[i] == this.end)
                {
                    unclosed--;
                    if (unclosed == 0)
                        indexOfClosed = i;
                }
            }

            return indexOfClosed;
        }
        public Sentence ParseSentence(string text)
        {
            IParseable parsingChain = DiseaseParsingChain.GetChain();

            List<ParsedValue> parsedText = new List<ParsedValue>();
            ParsedValue tmp = new ParsedValue(string.Empty);
            while (tmp.Recognised != RecognisedValue.EndOfSentence)
            {
                tmp = parsingChain.Recognise(text);
                if (tmp.Recognised == RecognisedValue.NotRecognised)
                    throw new ParsingException("not recognised formula");

                text = tmp.Text;
                if (tmp.Recognised != RecognisedValue.EndOfSentence)
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

                if (tmpFor.Recognised == RecognisedValue.SentenceTyped)
                    tmpList.Add(tmpList[i]);
            }

            if (tmpList.Count >= 2)
                return new ComplexSentence(currentConnective, tmpList.ToArray());

            if (tmpList.Count < 2 && tmpList.Count != 0)
                throw new ParsingException("sentence cannot end with non-unary keyword");

            return null;
        }

        protected override ParsedValue CheckRecognision(string text)
        {
            text = text.Trim();
            if (!text.StartsWith(this.start))
                return new ParsedValue(text);

            int? closingBrackets = this.PairsClosure(text);
            if (closingBrackets is null)
                throw new ParsingException("recursive sentences do not close up");
            string subSentence = text.Substring(1, closingBrackets.Value-2);
            return new ParsedValue(text.Substring(closingBrackets.Value + 1))
            {
                Recognised = RecognisedValue.SentenceTyped,
                Identifier = this.ParseSentence(subSentence + ";")
            };
        }



    }
    
}

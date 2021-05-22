using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Parser.ChainParser
{
    enum RecognisedValue
    {
        NotRecognised,
        Keyword,
        SentenceTyped,
        EndOfSentence
    }

    internal class ParsedValue
    {
        public RecognisedValue Recognised { get; set; } = RecognisedValue.NotRecognised;
        public Sentences.Sentence Identifier { get; set; } = null;
        public Sentences.Connective Connective { get; set; } = null;
        public string Text { get; set; }
        public ParsedValue(string text)
        {
            this.Text = text;
        }
    }

    interface IParseable
    {
        IParseable Next(IParseable next);
        ParsedValue Recognise(string text);
    }
}

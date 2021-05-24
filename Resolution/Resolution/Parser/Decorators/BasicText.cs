using System;

namespace Resolution.Parser.Decorators
{
    public class BasicText : ITextDecorator
    {
        private string _text;
        public string Text { get => this._text; }
        public BasicText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text cannot be null or empty");
            this._text = text;
        }
    }
}

using System;

namespace Resolution.Parser.Decorators
{
    public class BasicText : ITextDecorator
    {
        private string Text { get; set; }
        public BasicText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text cannot be null or empty");
            this.Text = text;
        }
        public string GetText() => this.Text;
    }
}

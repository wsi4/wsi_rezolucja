namespace Resolution.Parser.Decorators
{
    class EndlinesDecorator : AbstractTextDecorator
    {
        public EndlinesDecorator(ITextDecorator component) : base(component)
        {
        }

        protected override string Decorate(string text)
        {
            return text.Replace('\n', ' ').Replace('\r', ' ').Replace('\t', ' ').Trim();
        }
    }
}

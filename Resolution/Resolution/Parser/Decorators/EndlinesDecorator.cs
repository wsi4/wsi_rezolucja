namespace Resolution.Parser.Decorators
{
    class EndlinesDecorator : AbstractTextDecorator
    {
        public EndlinesDecorator(ITextDecorator component) : base(component)
        {
        }

        public override string Decorate(string text)
        {
            return text.Replace('\n', ' ').Replace('\r', ' ').Trim();
        }
    }
}

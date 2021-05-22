namespace Resolution.Parser.Decorators
{
    internal abstract class AbstractTextDecorator : ITextDecorator
    {
        private ITextDecorator component;

        public AbstractTextDecorator(ITextDecorator component)
        {
            this.component = component;
        }
        protected abstract string Decorate(string text);
        public string GetText()
        {
            return this.Decorate(this.component.GetText());
        }
    }
}

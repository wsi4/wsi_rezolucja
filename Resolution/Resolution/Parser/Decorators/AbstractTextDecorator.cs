namespace Resolution.Parser.Decorators
{
    public abstract class AbstractTextDecorator : ITextDecorator
    {
        private ITextDecorator component;

        public string Text 
        { 
            get
            {
                return this.Decorate(this.component.Text);
            }
        }

        public AbstractTextDecorator(ITextDecorator component)
        {
            this.component = component;
        }
        protected abstract string Decorate(string text);
    }
}

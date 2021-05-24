using Resolution.Parser.Exceptions;

namespace Resolution.Parser.Decorators
{
    class DiseasesDeclarationSetDecorator : AbstractTextDecorator
    {
        private static string DiseasesFileDefinition = "Choroby";
        public DiseasesDeclarationSetDecorator(ITextDecorator component) : base(component)
        {
        }

        protected override string Decorate(string text)
        {
            if (!text.StartsWith(DiseasesDeclarationSetDecorator.DiseasesFileDefinition))
                throw new ParsingException("Text do not starts with proper keyword" + DiseasesFileDefinition);
            
            var tmp = text.Substring(FileReader.DiseasesFileDefinition.Length).Trim();
            if(!(tmp.StartsWith("{") && tmp.EndsWith("}")))
                throw new ParsingException("Text do not contains body definition {}");
            return tmp.Remove(tmp.Length - 1).Substring(1).Trim();
        }
    }
}

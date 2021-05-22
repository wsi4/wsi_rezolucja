using Resolution.Parser.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Parser.Decorators
{
    class DiseasesDeclarationSetDecorator : AbstractTextDecorator
    {
        private static string DiseasesFileDefinition = "Choroby";
        public DiseasesDeclarationSetDecorator(ITextDecorator component) : base(component)
        {
        }

        public override string Decorate(string text)
        {
            // maybve add some chain in here
            if (!text.StartsWith(DiseasesDeclarationSetDecorator.DiseasesFileDefinition))
                throw new ParsingException("Text do not starts with proper keyword" + DiseasesFileDefinition);
            
            var tmp = text.Substring(FileReader.DiseasesFileDefinition.Length).Trim();
            if(!(tmp.StartsWith("{") && tmp.EndsWith("}")))
                throw new ParsingException("Text do not contains body definition {}");
            
            return tmp.Remove(tmp.Length - 1).Remove(0).Trim();
        }
    }
}

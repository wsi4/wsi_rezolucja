using Resolution.Parser.Decorators;
using Resolution.Sentences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Parser
{
    public static class FileReader
    {
        public static string DiseasesFileDefinition = "Choroby";
        public static IEnumerable<Sentence> ReadFileX(string pathToFile = @"d:\repos\io2_dektop\testowaBaza.txt")
        {
            using (var file = new StreamReader(pathToFile))
            {
                ITextDecorator decorator = new DiseasesDeclarationSetDecorator(
                    new EndlinesDecorator(
                    new BasicText(file.ReadToEnd())));
                string tmp = decorator.GetText();
                return DiseaseParser.SetParser(decorator.GetText());
            }
        }
    }
}

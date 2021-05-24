using Resolution.Parser.Decorators;
using Resolution.Sentences;
using System.Collections.Generic;
using System.IO;

namespace Resolution.Parser
{
    public static class FileReader
    {
        public static string DiseasesFileDefinition = "Choroby";
        public static IEnumerable<Sentence> ReadFileX(string pathToFile)
        {
            using (var file = new StreamReader(pathToFile))
            {
                ITextDecorator decorator = new DiseasesDeclarationSetDecorator(
                    new EndlinesDecorator(
                    new BasicText(file.ReadToEnd())));
                return DiseaseParser.SetParser(decorator.Text);
            }
        }
    }
}

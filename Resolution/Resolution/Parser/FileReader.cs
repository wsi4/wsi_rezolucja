using Resolution.Parser.Decorators;
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
        public static void ReadFileX(string pathToFile = @"d:\repos\io2_dektop\testowaBaza.txt")
        {
            using (var file = new StreamReader(pathToFile))
            {
                ITextDecorator decorator = new DiseasesDeclarationSetDecorator(
                    new EndlinesDecorator(
                    new BasicText(file.ReadToEnd())));
                
                
            }
        }
    }
}

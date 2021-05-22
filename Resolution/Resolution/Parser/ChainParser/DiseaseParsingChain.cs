using Resolution.Parser.ChainParser.Keywords;

namespace Resolution.Parser.ChainParser
{
    internal static class DiseaseParsingChain
    {
        private static IParseable chain { get; set; } = null;

        private static void CreateChain()
        {
            DiseaseParsingChain.chain = new AndParser();
        }

        public static IParseable GetChain()
        {
            if(chain == null)
                CreateChain();
            return chain;
        }
    }
}

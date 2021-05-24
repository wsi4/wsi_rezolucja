using Resolution.Parser.ChainParser.Keywords;

namespace Resolution.Parser.ChainParser
{
    public static class DiseaseParsingChain
    {
        private static IParseable chain { get; set; } = null;

        private static void CreateChain()
        {
            DiseaseParsingChain.chain = new AndParser();

            chain.Next(new OrParser())
                .Next(new ImpParser())
                .Next(new BiconParser())
                .Next(new NotParser())
                .Next(new IdentifierParser())
                .Next(new RecursiveSentenceParser())
                .Next(new EndParser());
        }

        public static IParseable GetChain()
        {
            if(chain == null)
                CreateChain();
            return chain;
        }
    }
}

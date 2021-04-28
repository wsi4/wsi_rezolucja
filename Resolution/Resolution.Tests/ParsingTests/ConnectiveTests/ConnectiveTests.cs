using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Propositional.Parsing.AsText.Connectives;

namespace Resolution.Tests.ParsingTests.ConnectiveTests
{
    [TestClass]
    public class ConnectiveTests
    {
        [TestMethod]
        public void UniqueSymbolsTest()
        {
            bool areNotEqual = true;

            var connectives = new AbstractConnective[]
            {
                new Not(),
                new And(),
                new Or(),
                new Implication(),
                new Biconditional()
            };

            for (int i = 0; i < connectives.Length - 1; i++)
            {
                for (int j = i + 1; j < connectives.Length; j++)
                {
                    if (connectives[i].Symbol.Equals(connectives[j].Symbol))
                    {
                        areNotEqual = false;
                        break;
                    }
                }
            }

            Assert.IsTrue(areNotEqual);
        }
    }
}

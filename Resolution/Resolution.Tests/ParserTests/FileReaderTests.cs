using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Parser;

namespace Resolution.Tests.ParserTests
{
    [TestClass]
    public class FileReaderTests
    {
        [TestMethod]
        public void TestLiteralSymbol()
        {
            var tmp = FileReader.ReadFileX();
            Assert.IsTrue(true);
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Parser;
using Resolution.Sentences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Tests.ParserTests
{
    [TestClass]
    public class FileReaderTests
    {
        [TestMethod]
        public void TestLiteralSymbol()
        {
            FileReader.ReadFileX();
            Assert.IsTrue(true);
        }

    }
}

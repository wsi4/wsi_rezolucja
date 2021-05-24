using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Parser.ChainParser.Keywords;
using Resolution.Parser.Exceptions;
using Resolution.Sentences;

namespace Resolution.Tests.ParserTests.KeywordsTests
{
    [TestClass]
    public class NegationTests
    {
        [TestMethod]
        public void NotTestTypeAndValue()
        {
            var tmp = new NotParser();
            var result = tmp.Recognise("not(te st1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.SentenceTyped
                && (result.Identifier as Literal).Symbol == "te st1"
                && (result.Identifier as Literal).Negated);
        }

        [TestMethod]
        public void NotTrimmedTest()
        {
            var tmp = new NotParser();
            var result = tmp.Recognise("   not(   te st1 ) and something");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.SentenceTyped
                && (result.Identifier as Literal).Symbol == "te st1"
                && (result.Identifier as Literal).Negated
                && result.Text == " and something");
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void NotNullIdentifier()
        {
            var tmp = new NotParser();
            tmp.Recognise("not(  )");

        }
    }
}

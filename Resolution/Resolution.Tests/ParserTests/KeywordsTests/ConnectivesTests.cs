using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Parser.ChainParser.Keywords;
using Resolution.Sentences;

namespace Resolution.Tests.ParserTests.KeywordsTests
{
    [TestClass]
    public class ConnectivesTests
    {
        [TestMethod]
        public void OrTestTypeAndValue()
        {
            OrParser tmp = new OrParser();
            var result = tmp.Recognise("or (test1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.Keyword
                && result.Connective == Connective.OR
                && result.Text == " (test1)");
        }

        [TestMethod]
        public void OrTestWrongTypeAndValue()
        {
            OrParser tmp = new OrParser();
            var result = tmp.Recognise("Or (test1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.NotRecognised
                && result.Connective is null);
        }

        [TestMethod]
        public void AndTestTypeAndValue()
        {
            var tmp = new AndParser();
            var result = tmp.Recognise("   and (test1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.Keyword
                && result.Connective == Connective.AND);
        }

        [TestMethod]
        public void AndTestWrongTypeAndValue()
        {
            var tmp = new AndParser();
            var result = tmp.Recognise("And (test1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.NotRecognised
                && result.Connective is null);
        }

        [TestMethod]
        public void ImpTestTypeAndValue()
        {
            var tmp = new ImpParser();
            var result = tmp.Recognise("imp (test1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.Keyword
                && result.Connective == Connective.IMPLICATION);
        }

        [TestMethod]
        public void ImpTestWrongTypeAndValue()
        {
            var tmp = new ImpParser();
            var result = tmp.Recognise("Imp (test1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.NotRecognised
                && result.Connective is null);
        }

        [TestMethod]
        public void BiconTestTypeAndValue()
        {
            var tmp = new BiconParser();
            var result = tmp.Recognise("bicon (test1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.Keyword
                && result.Connective == Connective.BICONDITIONAL);
        }

        [TestMethod]
        public void BiconTestWrongTypeAndValue()
        {
            var tmp = new ImpParser();
            var result = tmp.Recognise("Bicon (test1)");
            Assert.IsTrue(result.Recognised == Parser.ChainParser.RecognisedValue.NotRecognised
                && result.Connective is null);
        }
    }
}

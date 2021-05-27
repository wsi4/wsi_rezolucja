using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Parser;
using Resolution.Parser.Exceptions;
using Resolution.Sentences;

namespace Resolution.Tests.ParserTests
{
    [TestClass]
    public class DiseaseParserTests
    {
        [TestMethod]
        public void BasicSentence()
        {
            string text = "  ( test);";
            Sentence expected_result = new Literal("test");
            Sentence result = DiseaseParser.Parse(text);
            Assert.AreEqual(expected_result, result);
        }

        [TestMethod]
        public void ConnectiveSentence()
        {
            string text = "  ( test) or (test2);";

            Sentence expected_result = new ComplexSentence(Connective.OR, 
                new Literal("test"), 
                new Literal("test2"));
            Sentence result = DiseaseParser.Parse(text);
            Assert.AreEqual(expected_result, result);
        }


        [TestMethod]
        public void ManyConnectivesSentence()
        {
            string text = "  ( test) or (test2) and (test3);";

            Sentence expected_result = new ComplexSentence(Connective.AND, new ComplexSentence(Connective.OR,
                new Literal("test"),
                new Literal("test2")),
                new Literal("test3"));
            Sentence result = DiseaseParser.Parse(text);
            Assert.AreEqual(expected_result, result);
        }

        [TestMethod]
        public void ManyConnectivesWithnegationSentence()
        {
            string text = "  ( test) or (test2) and (test3) and not(test4);";

            var tmp = new Literal("test4");
            tmp.Negate();
            Sentence expected_result = new ComplexSentence(Connective.AND, new ComplexSentence(Connective.OR,
                new Literal("test"),
                new Literal("test2")),
                new Literal("test3"),
                tmp);
            Sentence result = DiseaseParser.Parse(text);
            Assert.AreEqual(expected_result, result);
        }

        [TestMethod]
        public void OneConnectiveWithnegationSentence()
        {
            string text = "  ( test) and (test2) and (test3) and not(test4);";

            var tmp = new Literal("test4");
            tmp.Negate();
            Sentence expected_result = new ComplexSentence(Connective.AND,
                new Literal("test"),
                new Literal("test2"),
                new Literal("test3"),
                tmp);
            Sentence result = DiseaseParser.Parse(text);
            Assert.AreEqual(expected_result, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void InvalidEnding()
        {
            string text = "  ( test) and (test2) and (test3) and not(test4)";
            Sentence result = DiseaseParser.Parse(text);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void InvalidConnectivesEnding()
        {
            string text = "  ( test) and (test2) and (test3) and or";
            Sentence result = DiseaseParser.Parse(text);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void InvalidLiteralsEnding()
        {
            string text = "  ( test) and (test2)(test4) and (test3) and or";
            Sentence result = DiseaseParser.Parse(text);
        }

        [TestMethod]
        public void RecursiveSentence()
        {
            string text = "  ( test) and <(test2) imp (test3)> and not(test4);";

            var tmp = new Literal("test4");
            tmp.Negate();
            Sentence expected_result = new ComplexSentence(Connective.AND,
                new Literal("test"),
                new ComplexSentence(Connective.IMPLICATION,
                new Literal("test2"),
                new Literal("test3")),
                tmp);
            Sentence result = DiseaseParser.Parse(text);
            Assert.AreEqual(expected_result, result);
        }

        [TestMethod]
        public void RecursiveSentence2()
        {
            string text = "  ( test) and <(test2) imp (test3)> and not(test4) and <(test2) imp (test3)>;";

            var tmp = new Literal("test4");
            tmp.Negate();
            Sentence expected_result = new ComplexSentence(Connective.AND,
                new Literal("test"),
                new ComplexSentence(Connective.IMPLICATION,
                new Literal("test2"),
                new Literal("test3")),
                tmp,
                new ComplexSentence(Connective.IMPLICATION,
                new Literal("test2"),
                new Literal("test3")));
            Sentence result = DiseaseParser.Parse(text);
            Assert.AreEqual(expected_result, result);
        }

        [TestMethod]
        public void Recursive2Sentence2()
        {
            string text = "  ( test) and < <(test2) imp (test3)> bicon  <(test2) imp (test3)>> and not(test4) and <(test2) imp (test3)>;";

            var tmp = new Literal("test4");
            tmp.Negate();
            Sentence expected_result = new ComplexSentence(Connective.AND,
                new Literal("test"),
                new ComplexSentence(Connective.BICONDITIONAL,
                    new ComplexSentence(Connective.IMPLICATION,
                        new Literal("test2"),
                        new Literal("test3")),
                    new ComplexSentence(Connective.IMPLICATION,
                        new Literal("test2"),
                        new Literal("test3"))),
                tmp,
                new ComplexSentence(Connective.IMPLICATION,
                new Literal("test2"),
                new Literal("test3")));
            Sentence result = DiseaseParser.Parse(text);
            Assert.AreEqual(expected_result, result);
        }
    }
}

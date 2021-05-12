using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;

namespace Resolution.Tests.SentencesTests
{
    [TestClass]
    public class ComplexSentenceTests
    {
        [TestMethod]
        public void TestComplexSentenceNegation()
        {
            Literal l1 = new("1"), l2 = new("2");
            ComplexSentence complex = new(Connective.AND, l1, l2);

            bool negated = complex.Negated;
            complex.Negate();

            Assert.AreNotEqual(negated, complex.Negated);
        }

        [TestMethod]
        public void TestComplexSentenceEquality()
        {
            string symbol = "sentence";
            Literal literal1 = new(symbol);
            Literal literal2 = new(symbol);

            ComplexSentence complex1 = new(Connective.OR, literal1, literal2);
            ComplexSentence complex2 = new(Connective.OR, literal1, literal2);

            Assert.AreEqual(complex1, complex2);
        }

        [TestMethod]
        public void TestComplexSentenceEqualityReversed()
        {
            string symbol = "sentence";
            Literal literal1 = new(symbol);
            Literal literal2 = new(symbol);

            ComplexSentence complex1 = new(Connective.OR, literal1, literal2);
            ComplexSentence complex2 = new(Connective.OR, literal2, literal1);

            Assert.AreEqual(complex1, complex2);
        }

        [TestMethod]
        public void TestComplexSentenceInequality()
        {
            string symbol = "sentence";
            Literal literal1 = new(symbol);
            Literal literal2 = new(symbol);

            ComplexSentence complex1 = new(Connective.OR, literal1, literal2);
            ComplexSentence complex2 = new(Connective.AND, literal1, literal2);

            Assert.AreNotEqual(complex1, complex2);
        }

        [TestMethod]
        public void TestComplexSentenceCloneEquality()
        {
            string symbol = "sentence";
            Literal literal = new(symbol);
            ComplexSentence complex1 = new(null, literal);
            ComplexSentence complex2 = complex1.Clone() as ComplexSentence;

            Assert.AreEqual(complex1, complex2);
        }

        [TestMethod]
        public void TestComplexSentenceCloneNegatedEquality()
        {
            string symbol = "sentence";
            Literal literal = new(symbol);
            ComplexSentence complex1 = new(null, literal);
            ComplexSentence complex2 = complex1.Clone() as ComplexSentence;
            complex2.Negate();

            Assert.AreNotEqual(complex1, complex2);
        }
    }
}

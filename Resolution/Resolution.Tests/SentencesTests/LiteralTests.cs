using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Tests.SentencesTests
{
    [TestClass]
    public class LiteralTests
    {
        [TestMethod]
        public void TestLiteralSymbol()
        {
            string symbol = "sentence";
            Literal literal = new(symbol);

            Assert.AreEqual(symbol, literal.Symbol);
        }

        [TestMethod]
        public void TestLiteralNegation()
        {
            string symbol = "sentence";
            Literal literal = new(symbol);

            bool negated = literal.Negated;
            literal.Negate();

            Assert.AreNotEqual(negated, literal.Negated);
        }

        [TestMethod]
        public void TestLiteralEquality()
        {
            string symbol = "sentence";
            Literal literal1 = new(symbol);
            Literal literal2 = new(symbol);

            Assert.AreEqual(literal1, literal2);
        }

        [TestMethod]
        public void TestLiteralCloneEquality()
        {
            string symbol = "sentence";
            Literal literal1 = new(symbol);
            Literal literal2 = literal1.Clone() as Literal;

            Assert.AreEqual(literal1, literal2);
        }

        [TestMethod]
        public void TestLiteralCloneNegatedEquality()
        {
            string symbol = "sentence";
            Literal literal1 = new(symbol);
            Literal literal2 = literal1.Clone() as Literal;
            literal2.Negate();

            Assert.AreNotEqual(literal1, literal2);
        }

        [TestMethod]
        public void TestLiteralAndComplexInequality()
        {
            string symbol = "sentence";
            Literal literal = new(symbol);
            ComplexSentence complex = new(null, literal);

            Assert.AreNotEqual(literal, complex);
        }
    }
}

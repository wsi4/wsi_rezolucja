using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using Resolution.Visitors;

namespace Resolution.Tests.VisitorsTests
{
    [TestClass]
    public class UnnestingVisitorTests
    {
        [TestMethod]
        public void UnnestingLiteralTest()
        {
            Literal literal = new("sentence");
            Literal cloned = literal.Clone() as Literal;

            UnnestingVisitor visitor = new();
            visitor.Visit(literal);

            Assert.AreEqual(literal, cloned);
        }

        [TestMethod]
        public void UnnestingComplexWithLiteralsTest()
        {
            Literal a = new("a"), b = new("b");
            ComplexSentence complex = new(Connective.OR, a, b);
            ComplexSentence cloned = complex.Clone() as ComplexSentence;

            UnnestingVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(complex, cloned);
        }

        [TestMethod]
        public void UnnestingComplexWithoutNestedTest()
        {
            Literal a = new("a"), b = new("b");
            ComplexSentence c = new(Connective.AND, b);
            ComplexSentence complex = new(Connective.OR, c, a);
            ComplexSentence cloned = complex.Clone() as ComplexSentence;

            UnnestingVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(complex, cloned);
        }

        [TestMethod]
        public void UnnestingComplexWithANDNestedOneLevelTest()
        {
            Literal a = new("a"), b = new("b");
            ComplexSentence c1 = new(Connective.AND, a);
            ComplexSentence c2 = new(Connective.AND, c1, b);

            ComplexSentence expected = new(Connective.AND, a, b);

            UnnestingVisitor visitor = new();
            visitor.Visit(c2);

            Assert.AreEqual(c2, expected);
        }

        [TestMethod]
        public void UnnestingComplexWithANDNestedOneLevelReverseOrderTest()
        {
            Literal a = new("a"), b = new("b");
            ComplexSentence c1 = new(Connective.AND, a);
            ComplexSentence c2 = new(Connective.AND, b, c1);

            ComplexSentence expected = new(Connective.AND, b, a);

            UnnestingVisitor visitor = new();
            visitor.Visit(c2);

            Assert.AreEqual(c2, expected);
        }

        [TestMethod]
        public void UnnestingComplexWithBICONDITIONALNestedOneLevelTest()
        {
            Literal a = new("a"), b = new("b");
            ComplexSentence c1 = new(Connective.BICONDITIONAL, a);
            ComplexSentence c2 = new(Connective.BICONDITIONAL, c1, b);

            ComplexSentence expected = new(Connective.BICONDITIONAL, a, b);

            UnnestingVisitor visitor = new();
            visitor.Visit(c2);

            Assert.AreEqual(c2, expected);
        }

        [TestMethod]
        public void UnnestingComplexWithORNestedOneLevel()
        {
            Literal a = new("a"), b = new("b");
            ComplexSentence c1 = new(Connective.OR, a);
            ComplexSentence c2 = new(Connective.OR, c1, b);

            ComplexSentence expected = new(Connective.OR, a, b);

            UnnestingVisitor visitor = new();
            visitor.Visit(c2);

            Assert.AreEqual(c2, expected);
        }

        [TestMethod]
        public void UnnestingComplexWithORNestedTwoLevels()
        {
            Literal a = new("a"), b = new("b"), c = new("c");
            ComplexSentence c1 = new(Connective.OR, a);
            ComplexSentence c2 = new(Connective.OR, c1, b);
            ComplexSentence c3 = new(Connective.OR, c2, c);

            ComplexSentence expected = new(Connective.OR, a, b, c);

            UnnestingVisitor visitor = new();
            visitor.Visit(c3);

            Assert.AreEqual(c3, expected);
        }

        [TestMethod]
        public void UnnestingComplexWithORNestedThreeLevels()
        {
            Literal a = new("a"), b = new("b"), c = new("c"), d = new("d");
            ComplexSentence c1 = new(Connective.OR, a);
            ComplexSentence c2 = new(Connective.OR, c1, b);
            ComplexSentence c3 = new(Connective.OR, c2, c);
            ComplexSentence c4 = new(Connective.OR, c3, d);

            ComplexSentence expected = new(Connective.OR, a, b, c, d);

            UnnestingVisitor visitor = new();
            visitor.Visit(c4);

            Assert.AreEqual(c4, expected);
        }

        [TestMethod]
        public void UnnestingComplexWithORNestedManyLevels()
        {
            Literal a = new("a"), b = new("b"), c = new("c"), d = new("d"), e = new("e");
            ComplexSentence c1 = new(Connective.OR, a);
            ComplexSentence c2 = new(Connective.OR, b, b, b);
            ComplexSentence c3 = new(Connective.OR, c1, c, c, c);
            ComplexSentence c4 = new(Connective.OR, d, d);
            ComplexSentence c5 = new(Connective.OR, c3, c4, e, c2);

            ComplexSentence expected = new(Connective.OR, a, b, b, b, c, c, c, d, e, e);

            UnnestingVisitor visitor = new();
            visitor.Visit(c5);

            Assert.AreEqual(c5, expected);
        }

        [TestMethod]
        public void UnnestingComplexConnectiveMushup()
        {
            Literal a = new("a"), b = new("b"), c = new("c"), d = new("d"), e = new("e");
            ComplexSentence c1 = new(Connective.OR, a);
            ComplexSentence c2 = new(Connective.OR, b, b, b);
            ComplexSentence c3 = new(Connective.AND, c1, c, c, c);
            ComplexSentence c4 = new(Connective.OR, d, d);
            ComplexSentence c5 = new(Connective.OR, c3, c4, e, c2);

            ComplexSentence expected = new(Connective.OR, c3, a, b, b, b, d, e, e);

            UnnestingVisitor visitor = new();
            visitor.Visit(c5);

            Assert.AreEqual(c5, expected);
        }

        [TestMethod]
        public void UnnestingComplexConnectiveMushup2()
        {
            Literal a = new("a"), b = new("b"), c = new("c"), d = new("d"), e = new("e");
            ComplexSentence c1 = new(Connective.AND, a);
            ComplexSentence cImp = new(Connective.IMPLICATION, a, b);
            ComplexSentence cBic = new(Connective.BICONDITIONAL, a, cImp);
            ComplexSentence c3 = new(Connective.OR, c1, c, c, c);
            ComplexSentence c4 = new(Connective.OR, d, c3);
            ComplexSentence c5 = new(Connective.OR, c4, cBic);

            ComplexSentence expected = new(Connective.OR, cBic, d, c, c, c, c1);

            UnnestingVisitor visitor = new();
            visitor.Visit(c5);

            Assert.AreEqual(c5, expected);
        }
    }
}

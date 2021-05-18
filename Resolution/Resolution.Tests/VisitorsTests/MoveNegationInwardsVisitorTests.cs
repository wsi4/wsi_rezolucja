using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using Resolution.Visitors;

namespace Resolution.Tests.VisitorsTests
{
    [TestClass]
    public class MoveNegationInwardsVisitorTests
    {
        [TestMethod]
        public void MoveNegationLiteralTest()
        {
            Literal literal = new("sentence");
            Literal cloned = literal.Clone() as Literal;

            MoveNegationInwardsVisitor visitor = new();
            visitor.Visit(literal);

            Assert.AreEqual(cloned, literal);
        }

        [TestMethod]
        public void MoveNegationNegatedLiteralTest()
        {
            Literal literal = new("sentence");
            literal.Negate();
            Literal cloned = literal.Clone() as Literal;

            MoveNegationInwardsVisitor visitor = new();
            visitor.Visit(literal);

            Assert.AreEqual(cloned,literal);
        }

        [TestMethod]
        public void MoveNegationComplexNotNegatedTest()
        {
            Literal a = new("a"), b = new("b");
            ComplexSentence complex = new(Connective.OR, a, b);
            ComplexSentence cloned = complex.Clone() as ComplexSentence;

            MoveNegationInwardsVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(cloned, complex);
        }

        [TestMethod]
        public void MoveNegationComplexORWithLiteralsTest()
        {
            Literal a = new("a"), b = new("b");
            Literal aN = a.Clone() as Literal, bN = a.Clone() as Literal;
            aN.Negate(); bN.Negate();
            ComplexSentence complex = new(Connective.OR, a, b);
            complex.Negate();
            ComplexSentence expected = new(Connective.AND,aN,bN);

            MoveNegationInwardsVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(expected, complex);
        }

        [TestMethod]
        public void MoveNegationComplexANDWithLiteralsTest()
        {
            Literal a = new("a"), b = new("b");
            Literal aN = a.Clone() as Literal, bN = a.Clone() as Literal;
            aN.Negate(); bN.Negate();
            ComplexSentence complex = new(Connective.AND, a, b);
            complex.Negate();
            ComplexSentence expected = new(Connective.OR, aN, bN);

            MoveNegationInwardsVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(expected, complex);
        }

        [TestMethod]
        public void MoveNegationComplexBICONDITIONALWithLiteralsTest()
        {
            Literal a = new("a"), b = new("b");
            Literal aC = a.Clone() as Literal, bC = b.Clone() as Literal;
            Literal aN = a.Clone() as Literal, bN = b.Clone() as Literal;
            aN.Negate(); bN.Negate();
            ComplexSentence complex = new(Connective.BICONDITIONAL, a, b);
            complex.Negate();

            var first = new ComplexSentence(Connective.OR, aN,bN);
            var second = new ComplexSentence(Connective.OR, aC, bC);
            ComplexSentence expected = new(Connective.AND, first, second);

            MoveNegationInwardsVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(expected, complex);
        }

        [TestMethod]
        public void MoveNegationComplexBICONDITIONALNested()
        {
            Literal a = new("a"), b = new("b"), c = new("c"), d = new("d");
            Literal aC = new("a"), bC = new("b"), cC = new("c"), dC = new("d");
            Literal aN = new("a"), bN = new("b"), cN = new("c"), dN = new("d");
            aN.Negate(); bN.Negate(); cN.Negate(); dN.Negate();

            ComplexSentence ab = new ComplexSentence(Connective.OR, a, b);
            ComplexSentence cd = new ComplexSentence(Connective.AND, c, d);
            ab.Negate();
            ComplexSentence complex = new(Connective.BICONDITIONAL, ab, cd);
            complex.Negate(); // ~(~(a v b) <=> (c ^ d))   ==     ((a v b) v (~c v ~d)) ^ ((~a ^ ~b) v (c ^ d))         ((a v b) v (-c v -d))

            ComplexSentence exAB = new ComplexSentence(Connective.OR, aC, bC);
            ComplexSentence exCNDN = new ComplexSentence(Connective.OR, cN, dN);
            ComplexSentence exANBN = new ComplexSentence(Connective.AND, aN, bN);
            ComplexSentence exCD = new ComplexSentence(Connective.AND, cC, dC);

            var first = new ComplexSentence(Connective.OR, exAB, exCNDN);
            var second = new ComplexSentence(Connective.OR, exANBN, exCD);
            ComplexSentence expected = new(Connective.AND, first, second);

            MoveNegationInwardsVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(expected, complex);
        }


        [TestMethod]
        public void MoveNegationComplexANDNested()
        {
            Literal a = new("a"), b = new("b"), c = new("c"), d = new("d");
            Literal aC = new("a"), bC = new("b");
            Literal cN = new("c"), dN = new("d");
            cN.Negate(); dN.Negate();

            ComplexSentence ab = new ComplexSentence(Connective.BICONDITIONAL, a, b);
            ComplexSentence cd = new ComplexSentence(Connective.AND, c, d);
            ab.Negate();
            ComplexSentence complex = new(Connective.OR, ab, cd);
            complex.Negate(); // ~(~(a <=> b) v (c ^ d))   ==    (a <=> b) ^ (~c v ~d)

           
            var first = new ComplexSentence(Connective.BICONDITIONAL, aC, bC);
            var second = new ComplexSentence(Connective.OR, cN, dN);
            ComplexSentence expected = new(Connective.AND, first, second);

            MoveNegationInwardsVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(expected, complex);
        }

    }
}

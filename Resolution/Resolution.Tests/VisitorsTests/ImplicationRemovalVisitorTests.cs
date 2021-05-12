using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using Resolution.Visitors;

namespace Resolution.Tests.VisitorsTests
{
    [TestClass]
    public class ImplicationRemovalVisitorTests
    {
        [TestMethod]
        public void TestImplicationRemovalLiteral()
        {
            Literal literal = new("sentence");
            Literal cloned = literal.Clone() as Literal;

            ImplicationRemovalVisitor visitor = new();
            visitor.Visit(literal);

            Assert.AreEqual(literal, cloned);
        }

        [TestMethod]
        public void TestImplicationRemovalComplexSentenceOr()
        {
            Literal a = new("a"), b = new("b");
            ComplexSentence complex = new(Connective.OR, a, b);
            ComplexSentence cloned = complex.Clone() as ComplexSentence;

            ImplicationRemovalVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(complex, cloned);
        }

        [TestMethod]
        public void TestImplicationRemovalComplexSentenceImpl()
        {
            Literal a = new("a"), b = new("b");

            ComplexSentence complex = new(Connective.IMPLICATION, a, b);
            
            Literal aCloned = a.Clone() as Literal;
            aCloned.Negate();
            ComplexSentence transformed = new(Connective.OR, aCloned, b);

            ImplicationRemovalVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(complex, transformed);
        }

        [TestMethod]
        public void TestImplicationRemovalNestedSentences()
        {
            Literal a = new("a"), b = new("b"), c = new("c");

            ComplexSentence complex = new(Connective.IMPLICATION, b, c);
            ComplexSentence root = new(Connective.AND, a, complex);

            Literal bCloned = b.Clone() as Literal;
            bCloned.Negate();
            ComplexSentence transformed = new(Connective.OR, bCloned, c);
            ComplexSentence newRoot = new(Connective.AND, a, transformed);
            

            ImplicationRemovalVisitor visitor = new();
            visitor.Visit(root);

            Assert.AreEqual(root, newRoot);
        }

        [TestMethod]
        public void TestImplicationRemovalMultipleSentences()
        {
            Literal a = new("a"), b = new("b"), c = new("c");

            ComplexSentence complex = new(Connective.IMPLICATION, a, b, c);

            Literal aCloned = a.Clone() as Literal;
            aCloned.Negate();
            Literal bCloned = b.Clone() as Literal;
            bCloned.Negate();
            ComplexSentence transformed = new(Connective.OR, aCloned, bCloned, c);


            ImplicationRemovalVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreEqual(complex, transformed);
        }

        [TestMethod]
        public void TestImplicationRemovalComplexSentenceWrongResult()
        {
            Literal a = new("a"), b = new("b");

            ComplexSentence complex = new(Connective.IMPLICATION, a, b);

            Literal aCloned = a.Clone() as Literal;
            aCloned.Negate();
            ComplexSentence transformed = new(Connective.AND, aCloned, b);

            ImplicationRemovalVisitor visitor = new();
            visitor.Visit(complex);

            Assert.AreNotEqual(complex, transformed);
        }
    }
}

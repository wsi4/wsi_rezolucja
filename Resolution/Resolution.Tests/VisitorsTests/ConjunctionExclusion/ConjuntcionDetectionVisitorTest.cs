using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using Resolution.Visitors.ConjunctionExclusion;

namespace Resolution.Tests.VisitorsTests.ConjunctionExclusion
{
    [TestClass]
    public class ConjuntcionDetectionVisitorTest
    {
        [TestMethod]
        public void DetectConjunction_ReturnsFalse_WhenNoConjunction()
        {
            var visitor = new ConjunctionDetectionVisitor();
            var sentence = new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q"));
            bool conjunctionDetected = visitor.DetectConjunction(sentence);
            Assert.IsFalse(conjunctionDetected);
        }

        [TestMethod]
        public void DetectConjunction_ReturnsFalse_WhenConjunctionInRootLevel()
        {
            var visitor = new ConjunctionDetectionVisitor();
            var sentence = new ComplexSentence(
                Connective.AND,
                new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q")),
                new Literal("r")
            );

            bool conjunctionDetected = visitor.DetectConjunction(sentence);
            Assert.IsFalse(conjunctionDetected);
        }

        [TestMethod]
        public void DetectConjunction_ReturnsTrue_WhenConjunctionInChildSentence()
        {
            var visitor = new ConjunctionDetectionVisitor();
            var sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND,
                    new Literal("p"),
                    new Literal("q")
                ),
                new Literal("r")
            );

            bool conjunctionDetected = visitor.DetectConjunction(sentence);
            Assert.IsTrue(conjunctionDetected);
        }

        [TestMethod]
        public void DetectConcjunction_ReturnsTrue_WhenNestedConjunction()
        {
            var visitor = new ConjunctionDetectionVisitor();
            var sentence = new ComplexSentence(
                Connective.AND,
                new ComplexSentence(
                    Connective.OR,
                    new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q")),
                    new Literal("r")
                ),
                new Literal("s")
            );

            bool conjunctionDetected = visitor.DetectConjunction(sentence);
            Assert.IsTrue(conjunctionDetected);
        }
    }
}
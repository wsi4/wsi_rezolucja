using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using Resolution.Visitors.ConjunctionExclusion;

namespace Resolution.Tests.VisitorsTests
{
    [TestClass]
    public class ConjuntcionDetectionVisitorTest
    {
        [TestMethod]
        public void DetectConcjunction_ReturnsTrue_WhenNestedConjunction()
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
    }
}
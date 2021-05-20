using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using Resolution.Visitors;

namespace Resolution.Tests.VisitorsTests
{
    [TestClass]
    public class ClauseMakerVisitorTests
    {
        [TestMethod]
        public void SingleLiteralTest()
        {
            var sentence = new Literal("p");
            var testedVisitor = new ClauseMakerVisitor();
            var clauseCollection = testedVisitor.CreateClauses(sentence);

            Assert.AreEqual(1, clauseCollection.Count);
            Assert.AreEqual(1, clauseCollection[0].PositiveLiterals.Count);
            Assert.AreEqual(sentence, clauseCollection[0].PositiveLiterals[0]);
        }

        [TestMethod]
        public void SingleClauseTest()
        {
            var p = new Literal("p");
            var q = new Literal("q");
            var r = new Literal("r", negated: true);
            var sentence = new ComplexSentence(Connective.OR, p, q, r);

            var testedVisitor = new ClauseMakerVisitor();
            var clauseCollection = testedVisitor.CreateClauses(sentence);

            Assert.AreEqual(1, clauseCollection.Count);
            Assert.AreEqual(2, clauseCollection[0].PositiveLiterals.Count);
            Assert.AreEqual(p, clauseCollection[0].PositiveLiterals[0]);
            Assert.AreEqual(q, clauseCollection[0].PositiveLiterals[1]);

            Assert.AreEqual(1, clauseCollection[0].NegativeLiterals.Count);
            Assert.AreEqual(r, clauseCollection[0].NegativeLiterals[0]);
        }

        [TestMethod]
        public void ConjunctionOfClausesTest()
        {
            var p = new Literal("p");
            var q = new Literal("q", negated: true);
            var r = new Literal("r", negated: true);
            var s = new Literal("s");
            var t = new Literal("t", negated: true);
            var sentence = new ComplexSentence(
                Connective.AND,
                new ComplexSentence(Connective.OR, p, q, r),
                new ComplexSentence(Connective.OR, t, s)
            );

            var testedVisitor = new ClauseMakerVisitor();
            var clauseCollection = testedVisitor.CreateClauses(sentence);

            Assert.AreEqual(2, clauseCollection.Count);

            Assert.AreEqual(1, clauseCollection[0].PositiveLiterals.Count);
            Assert.AreEqual(p, clauseCollection[0].PositiveLiterals[0]);
            Assert.AreEqual(2, clauseCollection[0].NegativeLiterals.Count);
            Assert.AreEqual(q, clauseCollection[0].NegativeLiterals[0]);
            Assert.AreEqual(r, clauseCollection[0].NegativeLiterals[1]);

            Assert.AreEqual(1, clauseCollection[1].PositiveLiterals.Count);
            Assert.AreEqual(s, clauseCollection[1].PositiveLiterals[0]);
            Assert.AreEqual(1, clauseCollection[1].NegativeLiterals.Count);
            Assert.AreEqual(t, clauseCollection[1].NegativeLiterals[0]);
        }
    }
}
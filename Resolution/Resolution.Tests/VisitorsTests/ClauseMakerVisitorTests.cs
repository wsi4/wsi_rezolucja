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
            Assert.IsTrue(clauseCollection[0].PositiveLiterals.Contains(sentence));
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
            Assert.IsTrue(clauseCollection[0].PositiveLiterals.Contains(p));
            Assert.IsTrue(clauseCollection[0].PositiveLiterals.Contains(q));

            Assert.AreEqual(1, clauseCollection[0].NegativeLiterals.Count);
            Assert.IsTrue(clauseCollection[0].NegativeLiterals.Contains(r));
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
            Assert.IsTrue(clauseCollection[0].PositiveLiterals.Contains(p));
            Assert.AreEqual(2, clauseCollection[0].NegativeLiterals.Count);
            Assert.IsTrue(clauseCollection[0].NegativeLiterals.Contains(q));
            Assert.IsTrue(clauseCollection[0].NegativeLiterals.Contains(r));

            Assert.AreEqual(1, clauseCollection[1].PositiveLiterals.Count);
            Assert.IsTrue(clauseCollection[1].PositiveLiterals.Contains(s));
            Assert.AreEqual(1, clauseCollection[1].NegativeLiterals.Count);
            Assert.IsTrue(clauseCollection[1].NegativeLiterals.Contains(t));
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Clauses;
using Resolution.Sentences;

namespace Resolution.Tests.Clauses
{
    [TestClass]
    public class ClauseCollectionBuilderTests
    {
        [TestMethod]
        public void BuildsCorrectClauseCollection()
        {
            var testedBuilder = new ClauseCollectionBuilder();

            var clause = new Clause(new[] {new Literal("p"), new Literal("q")});
            var literal1 = new Literal("r", negated: true);
            var literal2 = new Literal("s");

            testedBuilder.AddClause(clause);
            testedBuilder.AddLiteral(literal1);
            testedBuilder.AddLiteral(literal2);
            testedBuilder.EndClause();

            var clauseCollection = testedBuilder.Build();

            Assert.AreEqual(2, clauseCollection.Count);
            Assert.AreEqual(clause, clauseCollection[0]);

            Assert.AreEqual(1, clauseCollection[1].PositiveLiterals.Count);
            Assert.IsTrue(clauseCollection[1].PositiveLiterals.Contains(literal2));

            Assert.AreEqual(1, clauseCollection[1].NegativeLiterals.Count);
            Assert.IsTrue(clauseCollection[1].NegativeLiterals.Contains(literal1));
        }
    }
}

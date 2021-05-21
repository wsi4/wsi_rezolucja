using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resolution.Clauses;
using Resolution.Sentences;
using Resolution.Visitors.ClauseMaker;

namespace Resolution.Tests.VisitorsTests.ClauseMaker
{
    [TestClass]
    public class ConjunctionLevelClauseMakerStateTests
    {
        [TestMethod]
        public void ProcessLiteral_AddsLiteralAsClauseToClauseCollectionBuilder()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilderMock = new Mock<ClauseCollectionBuilder>();
            clauseCollectionBuilderMock.Setup(mock => mock.AddClause(It.IsAny<Clause>()));

            var literal = new Literal("p");
            var testedState = new ConjunctionLevelClauseMakerState(clauseMakerFsm, clauseCollectionBuilderMock.Object);

            testedState.ProcessLiteral(literal);
            clauseCollectionBuilderMock.Verify(
                mock => mock.AddClause(It.Is<Clause>(c =>
                    c.PositiveLiterals.Count == 1 &&
                    c.NegativeLiterals.Count == 0 &&
                    c.PositiveLiterals[0].Equals(literal)
                )),
                Times.Once
            );
        }

        [TestMethod]
        public void ProcessComplexSentence_ThrowsArgumentException_WhenSentenceIsImplication()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();

            var complexSentence = new ComplexSentence(Connective.IMPLICATION, new Literal("p"), new Literal("q"));
            var testedState = new ConjunctionLevelClauseMakerState(clauseMakerFsm, clauseCollectionBuilder);

            Assert.ThrowsException<ArgumentException>(() => testedState.ProcessComplexSentence(complexSentence));
        }

        [TestMethod]
        public void ProcessComplexSentence_SetsFsmStateToClauseLevel_WhenSentenceIsConjunction()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();

            var complexSentence = new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q"));
            var testedState = new ConjunctionLevelClauseMakerState(clauseMakerFsm, clauseCollectionBuilder);

            testedState.ProcessComplexSentence(complexSentence);
            Assert.IsInstanceOfType(clauseMakerFsm.State, typeof(ClauseLevelClauseMakerState));
        }

        [TestMethod]
        public void ProcessComplexSentence_SetsFsmStateToLiteralLevel_WhenSentenceIsAlternative()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();

            var complexSentence = new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q"));
            var testedState = new ConjunctionLevelClauseMakerState(clauseMakerFsm, clauseCollectionBuilder);

            testedState.ProcessComplexSentence(complexSentence);
            Assert.IsInstanceOfType(clauseMakerFsm.State, typeof(LiteralLevelClauseMakerState));
        }
    }
}
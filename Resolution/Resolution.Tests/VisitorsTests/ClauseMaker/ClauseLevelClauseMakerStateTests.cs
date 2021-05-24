using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resolution.Clauses;
using Resolution.Sentences;
using Resolution.Visitors.ClauseMaker;

namespace Resolution.Tests.VisitorsTests.ClauseMaker
{
    [TestClass]
    public class ClauseLevelClauseMakerStateTests
    {
        [TestMethod]
        public void ProcessLiteral_SetsFsmStateToParent_WhenClauseLimitReached()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;

            var literal = new Literal("p");
            var testedState = new ClauseLevelClauseMakerState(clauseMakerFsm, parentState, clauseCollectionBuilder, 0);

            testedState.ProcessLiteral(literal);
            Assert.AreEqual(parentState, clauseMakerFsm.State);
        }

        [TestMethod]
        public void ProcessLiteral_SetsFsmStateToItself_WhenClauseLimitNotReached()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;

            var literal = new Literal("p");
            var testedState = new ClauseLevelClauseMakerState(clauseMakerFsm, parentState, clauseCollectionBuilder, 1);

            testedState.ProcessLiteral(literal);
            Assert.AreEqual(testedState, clauseMakerFsm.State);
        }

        [TestMethod]
        public void ProcessLiteral_AddsLiteralToClauseCollectionBuilder_WhenClauseLimitNotReached()
        {
            var clauseCollectionBuilderMock = new Mock<ClauseCollectionBuilder>();
            clauseCollectionBuilderMock.Setup(mock => mock.AddClause(It.IsAny<Clause>()));

            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilderMock.Object).Object;

            var literal = new Literal("test");
            var testedState = new ClauseLevelClauseMakerState(
                clauseMakerFsm, parentState, clauseCollectionBuilderMock.Object, 1
            );

            testedState.ProcessLiteral(literal);
            clauseCollectionBuilderMock.Verify(
                mock => mock.AddClause(It.Is<Clause>(c =>
                    c.PositiveLiterals.Count == 1 &&
                    c.NegativeLiterals.Count == 0 &&
                    c.PositiveLiterals.Contains(literal)
                )),
                Times.Once
            );
        }

        [TestMethod]
        public void ProcessComplexSentence_SetsFsmStateToParent_WhenClauseLimitReached()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;

            var complexSentence = new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q"));
            var testedState = new ClauseLevelClauseMakerState(clauseMakerFsm, parentState, clauseCollectionBuilder, 0);

            testedState.ProcessComplexSentence(complexSentence);
            Assert.AreEqual(parentState, clauseMakerFsm.State);
        }

        [TestMethod]
        public void ProcessComplexSentence_SetsFsmStateToLiteralLevel_WhenClauseLimitNotReached()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;

            var complexSentence = new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q"));
            var testedState = new ClauseLevelClauseMakerState(clauseMakerFsm, parentState, clauseCollectionBuilder, 1);

            testedState.ProcessComplexSentence(complexSentence);
            Assert.IsInstanceOfType(clauseMakerFsm.State, typeof(LiteralLevelClauseMakerState));
        }

        [TestMethod]
        public void ProcessComplexSentence_ThrowsArgumentException_WhenSentenceIsConjunction()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;

            var complexSentence = new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q"));
            var testedState = new ClauseLevelClauseMakerState(clauseMakerFsm, parentState, clauseCollectionBuilder, 1);

            Assert.ThrowsException<ArgumentException>(() => testedState.ProcessComplexSentence(complexSentence));
        }
    }
}
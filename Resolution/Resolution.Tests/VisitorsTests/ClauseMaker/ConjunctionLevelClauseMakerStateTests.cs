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
        public void ProcessLiteral_AddsLiteralToClauseCollectionBuilder()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilderMock = new Mock<ClauseCollectionBuilder>();
            clauseCollectionBuilderMock.Setup(mock => mock.AddLiteral(It.IsAny<Literal>()));

            var literal = new Literal("p");
            var testedState = new ConjunctionLevelClauseMakerState(clauseMakerFsm, clauseCollectionBuilderMock.Object);

            testedState.ProcessLiteral(literal);
            clauseCollectionBuilderMock.Verify(
                mock => mock.AddLiteral(It.Is<Literal>(l => l.Equals(literal))),
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
        public void ProcessLiteral_UsesClauseLevelState_WhenSentenceIsAlternative()
        {
            // var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            // var clauseCollectionBuilder = new ClauseCollectionBuilder();

            // var literal = new Literal("test");
            // var testedState = new ConjunctionLevelClauseMakerState(clauseMakerFsm, clauseCollectionBuilder);

            // Assert.ThrowsException<ArgumentException>(() => testedState.ProcessLiteral(literal));
        }
    }
}
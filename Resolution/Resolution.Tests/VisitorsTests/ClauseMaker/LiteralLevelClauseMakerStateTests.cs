using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resolution.Clauses;
using Resolution.Sentences;
using Resolution.Visitors.ClauseMaker;

namespace Resolution.Tests.VisitorsTests.ClauseMaker
{
    [TestClass]
    public class LiteralLevelClauseMakerStateTests
    {
        [TestMethod]
        public void ProcessComplexSentence_ThrowsArgumentException()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;

            var complexSentence = new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q"));
            var testedState = new LiteralLevelClauseMakerState(clauseMakerFsm, parentState, clauseCollectionBuilder, 1);

            Assert.ThrowsException<ArgumentException>(() => testedState.ProcessComplexSentence(complexSentence));
        }

        [TestMethod]
        public void ProcessLiteral_AddsLiteralToClauseCollectionBuilder()
        {
            // var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            // var clauseCollectionBuilder = new Mock<ClauseCollectionBuilder>();
            // var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;
            //
            // var literal = new Literal("test");
            // var testedState = new LiteralLevelClauseMakerState(
            //     clauseMakerFsm, parentState, clauseCollectionBuilder.Object, 1
            // );
            //
            // clauseCollectionBuilder.Verify(builder => builder.AddLiteral(Moq.Match<Literal>(literal)), Times.Once);
        }

        [TestMethod]
        public void ProcessLiteral_SetsFsmStateToParentState_WhenLiteralLimitReached()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;

            var literal = new Literal("test");
            var testedState = new LiteralLevelClauseMakerState(clauseMakerFsm, parentState, clauseCollectionBuilder, 1);

            testedState.ProcessLiteral(literal);
            Assert.AreEqual(parentState, clauseMakerFsm.State);
        }

        [TestMethod]
        public void ProcessLiteral_SetsFsmStateToItself_WhenLiteralLimitNotReached()
        {
            var clauseMakerFsm = Mock.Of<IClauseMakerFSM>();
            var clauseCollectionBuilder = new ClauseCollectionBuilder();
            var parentState = new Mock<ClauseMakerState>(clauseMakerFsm, clauseCollectionBuilder).Object;

            var literal = new Literal("test");
            var testedState = new LiteralLevelClauseMakerState(clauseMakerFsm, parentState, clauseCollectionBuilder, 2);

            testedState.ProcessLiteral(literal);
            Assert.AreEqual(testedState, clauseMakerFsm.State);
        }
    }
}
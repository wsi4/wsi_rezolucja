using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resolution.Sentences;
using Resolution.Visitors.ConjunctionExclusion;

namespace Resolution.Tests.VisitorsTests.ConjunctionExclusion
{
    [TestClass]
    public class ChildConjunctionExclusionStateTests
    {
        [TestMethod]
        public void ParseLiteral_ShouldSetFsmStateToItself()
        {
            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new ChildConjunctionExclusionState(fsm);
            var literal = new Literal("p");
            testedState.ParseLiteral(literal);
            Assert.AreEqual(testedState, fsm.State);
        }

        [TestMethod]
        public void ParseComplexSentence_ShouldSetFsmStateToItself_WhenSentenceIsAlternative()
        {
            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new ChildConjunctionExclusionState(fsm);
            var complexSentence = new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q"));
            testedState.ParseComplexSentence(complexSentence);
            Assert.AreEqual(testedState, fsm.State);
        }

        [TestMethod]
        public void ParseComplexSentence_ShouldSetFsmStateToRoot_WhenSentenceIsConjunction()
        {
            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new ChildConjunctionExclusionState(fsm);
            var complexSentence = new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q"));
            testedState.ParseComplexSentence(complexSentence);
            Assert.IsInstanceOfType(fsm.State, typeof(RootConjunctionExclusionState));
        }

        [TestMethod]
        public void ParseComplexSentence_ShouldThrowArgumentException_WhenSentenceIsImplication()
        {
            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new ChildConjunctionExclusionState(fsm);
            var complexSentence = new ComplexSentence(Connective.IMPLICATION, new Literal("p"), new Literal("q"));
            Assert.ThrowsException<ArgumentException>(() => testedState.ParseComplexSentence(complexSentence));
        }
    }
}
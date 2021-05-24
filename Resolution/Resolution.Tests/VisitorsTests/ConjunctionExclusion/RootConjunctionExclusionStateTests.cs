using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Resolution.Sentences;
using Resolution.Visitors.ConjunctionExclusion;

namespace Resolution.Tests.VisitorsTests.ConjunctionExclusion
{
    [TestClass]
    public class RootConjunctionExclusionStateTests
    {
        [TestMethod]
        public void ParseLiteral_ShouldSetFsmStateToItself()
        {
            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new RootConjunctionExclusionState(
                fsm, new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q"))
            );
            var literal = new Literal("p");
            testedState.ParseLiteral(literal);
            Assert.AreEqual(testedState, fsm.State);
        }

        [TestMethod]
        public void ParseComplexSentence_ShouldSetFsmStateToItself_WhenSentenceIsConjunction()
        {
            var subConjunction = new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q"));
            var complexSentence = new ComplexSentence(Connective.AND, new Literal("s"), new Literal("r"));

            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new RootConjunctionExclusionState(fsm, subConjunction);

            testedState.ParseComplexSentence(complexSentence);
            Assert.AreEqual(testedState, fsm.State);
        }

        [TestMethod]
        public void ParseComplexSentence_ShouldSetFsmStateToChild_WhenSentenceIsAlternative()
        {
            var subConjunction = new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q"));
            var complexSentence = new ComplexSentence(Connective.OR, subConjunction, new Literal("r"));

            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new RootConjunctionExclusionState(fsm, subConjunction);

            testedState.ParseComplexSentence(complexSentence);
            Assert.IsInstanceOfType(fsm.State, typeof(ChildConjunctionExclusionState));
        }

        [TestMethod]
        public void ParseComplexSentence_ShouldRemoveSubConjunctionFromSentence_WhenSentenceIsAlternative()
        {
            var subConjunction = new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q"));
            var complexSentence = new ComplexSentence(Connective.OR, subConjunction, new Literal("r"));
            Assert.IsTrue(complexSentence.Sentences.Contains(subConjunction));

            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new RootConjunctionExclusionState(fsm, subConjunction);

            testedState.ParseComplexSentence(complexSentence);
            Assert.IsFalse(complexSentence.Sentences.Contains(subConjunction));
        }

        [TestMethod]
        public void ParseComplexSentence_ShouldThrowArgumentException_WhenSentenceIsImplication()
        {
            var fsm = Mock.Of<IConjunctionExclusionFSM>();
            var testedState = new RootConjunctionExclusionState(
                fsm, new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q"))
            );
            var complexSentence = new ComplexSentence(Connective.IMPLICATION, new Literal("p"), new Literal("q"));
            Assert.ThrowsException<ArgumentException>(() => testedState.ParseComplexSentence(complexSentence));
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using Resolution.Visitors;

namespace Resolution.Tests.VisitorsTests
{
    [TestClass]
    public class ConjunctionExclusionVisitorTests
    {
        [TestMethod]
        public void Visit_RemovesConjunction_WhenBasicCaseSentence()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q")),
                new Literal("r")
            );

            var expected = new ComplexSentence(
                Connective.AND,
                new ComplexSentence(Connective.OR, new Literal("p"), new Literal("r")),
                new ComplexSentence(Connective.OR, new Literal("q"), new Literal("r"))
            );

            var visitor = new ConjunctionExclusionVisitor();
            visitor.Visit(sentence);

            Assert.AreEqual(expected, sentence);
        }

        [TestMethod]
        public void Visit_RemovesConjunction_WhenBasicCaseLongSentence()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND, new Literal("p"), new Literal("q"), new Literal("r")
                ),
                new Literal("s"),
                new Literal("t")
            );

            var expected = new ComplexSentence(
                Connective.AND,
                new ComplexSentence(Connective.OR, new Literal("p"), new Literal("s"), new Literal("t")),
                new ComplexSentence(Connective.OR, new Literal("q"), new Literal("s"), new Literal("t")),
                new ComplexSentence(Connective.OR, new Literal("r"), new Literal("s"), new Literal("t"))
            );

            var visitor = new ConjunctionExclusionVisitor();
            visitor.Visit(sentence);

            Assert.AreEqual(expected, sentence);
        }

        [TestMethod]
        public void Visit_RemovesConjunction_WhenBasicCaseSentenceDifferentOrder()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new Literal("p"),
                new ComplexSentence(
                    Connective.AND, new Literal("q"), new Literal("r")
                ),
                new Literal("s")
            );

            var expected = new ComplexSentence(
                Connective.AND,
                new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q"), new Literal("s")),
                new ComplexSentence(Connective.OR, new Literal("p"), new Literal("r"), new Literal("s"))
            );

            var visitor = new ConjunctionExclusionVisitor();
            visitor.Visit(sentence);
            Assert.AreEqual(expected, sentence);
        }

        [TestMethod]
        public void Visit_RemovesConjunction_WhenAlternativeOfConjunctions()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND, new Literal("p"), new Literal("q"), new Literal("r")
                ),
                new ComplexSentence(
                    Connective.AND, new Literal("s"), new Literal("t")
                )
            );

            var expected = new ComplexSentence(
                Connective.AND,
                new ComplexSentence(Connective.OR, new Literal("p"), new Literal("s")),
                new ComplexSentence(Connective.OR, new Literal("p"), new Literal("t")),
                new ComplexSentence(Connective.OR, new Literal("q"), new Literal("s")),
                new ComplexSentence(Connective.OR, new Literal("q"), new Literal("t")),
                new ComplexSentence(Connective.OR, new Literal("r"), new Literal("s")),
                new ComplexSentence(Connective.OR, new Literal("r"), new Literal("t"))
            );

            var visitor = new ConjunctionExclusionVisitor();
            visitor.Visit(sentence);
            Assert.AreEqual(expected, sentence);
        }

        [TestMethod]
        public void Visit_RemovesConjunction_WhenNestedComplexSentences()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND,
                    new ComplexSentence(
                        Connective.OR, new Literal("p"), new Literal("q")
                    ),
                    new Literal("r")
                ),
                new Literal("s")
            );

            var expected = new ComplexSentence(
                Connective.AND,
                new ComplexSentence(Connective.OR, new Literal("p"), new Literal("q"), new Literal("s")),
                new ComplexSentence(Connective.OR, new Literal("r"), new Literal("s"))
            );

            var visitor = new ConjunctionExclusionVisitor();
            visitor.Visit(sentence);
            Assert.AreEqual(expected, sentence);
        }
    }
}
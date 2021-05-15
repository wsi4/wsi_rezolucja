using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using Resolution.Visitors;
using Resolution.Visitors.ConjunctionExclusion;

namespace Resolution.Tests.VisitorsTests
{
    [TestClass]
    public class ConjunctionExclusionVisitorTests
    {
        [TestMethod]
        public void BasicCaseTest()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND, new Literal("p"), new Literal("q"), new Literal("r")
                ),
                new Literal("s"),
                new Literal("t")
            );

            Console.WriteLine(sentence);

            var visitor = new ConjunctionExclusionVisitor();
            var conjunctionDetector = new ConjunctionDetectionVisitor();

            // while (conjunctionDetector.DetectConjunction(sentence))
            // {
            visitor.Visit(sentence);
            Console.WriteLine(sentence);
            // }
        }

        [TestMethod]
        public void ReverseCaseTest()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new Literal("p"),
                new ComplexSentence(
                    Connective.AND, new Literal("q"), new Literal("r"), new Literal("s")
                ),
                new Literal("t")
            );

            Console.WriteLine(sentence);

            var visitor = new ConjunctionExclusionVisitor();
            var conjunctionDetector = new ConjunctionDetectionVisitor();

            // while (conjunctionDetector.DetectConjunction( sentence ))
            // {
            visitor.Visit(sentence);
            Console.WriteLine(sentence);
            // }
        }

        [TestMethod]
        public void ComplexCaseTest()
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

            Console.WriteLine(sentence);

            var visitor = new ConjunctionExclusionVisitor();
            var conjunctionDetector = new ConjunctionDetectionVisitor();

            // while (conjunctionDetector.DetectConjunction( sentence ))
            // {
            visitor.Visit(sentence);
            Console.WriteLine(sentence);
            // }
        }

        [TestMethod]
        public void RecursiveBasicCaseTest()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND,
                    new ComplexSentence(
                        Connective.OR, new Literal("p"), new Literal("q")
                    ),
                    new Literal("r"),
                    new Literal("s")
                ),
                new Literal("t")
            );

            Console.WriteLine(sentence);

            var visitor = new ConjunctionExclusionVisitor();
            var conjunctionDetector = new ConjunctionDetectionVisitor();

            // while (conjunctionDetector.DetectConjunction( sentence ))
            // {
            visitor.Visit(sentence);
            Console.WriteLine(sentence);
            // }
        }
    }
}
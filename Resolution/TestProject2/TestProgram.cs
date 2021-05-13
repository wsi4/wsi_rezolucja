using System;
using Resolution.Sentences;
using Resolution.Visitors;
using Resolution.Visitors.ConjunctionExclusion;

namespace TestProject2
{
    public class TestProgram
    {
        public static void Main()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND, new Literal("p"), new Literal("q"), new Literal("r")
                ),
                new Literal("s"),
                new Literal("t")
            );

            var visitor = new ConjunctionExclusionVisitor();
            var conjunctionDetector = new ConjunctionDetecionVisitor();

            while (conjunctionDetector.DetectConjunction(sentence))
            {
                Console.WriteLine(sentence);
                visitor.Visit(sentence);
            }

            Console.WriteLine(sentence);

            sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND, new Literal("p"), new Literal("q"), new Literal("r")
                ),
                new ComplexSentence(
                    Connective.AND, new Literal("s"), new Literal("t")
                )
            );

            Console.WriteLine(sentence);

            while (conjunctionDetector.DetectConjunction(sentence))
            {
                visitor.Visit(sentence);
                Console.WriteLine(sentence);
            }
        }
    }
}
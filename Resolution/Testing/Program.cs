using System;
using Resolution.Sentences;
using Resolution.Visitors;
using Resolution.Visitors.ConjunctionExclusion;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            // var sentence = new ComplexSentence(
            // Connective.AND,
            // new ComplexSentence(Connective.AND, new Literal("p"), new Literal("q")),
            // new ComplexSentence(Connective.OR, new Literal("r"), new Literal("s"))
            // );

            // Console.WriteLine(sentence);

            // var visitor = new UnnestingVisitor();
            // visitor.Visit(sentence);

            // Console.WriteLine(sentence);

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

            var conjunctionExclusionVisitor = new ConjunctionExclusionVisitor();
            var conjunctionDetector = new ConjunctionDetectionVisitor();
            var unnsestingVisitor = new UnnestingVisitor();

            while (conjunctionDetector.DetectConjunction(sentence))
            {
                conjunctionExclusionVisitor.Visit(sentence);
                Console.WriteLine(sentence);
                unnsestingVisitor.Visit(sentence);
                Console.WriteLine(sentence);
            }
        }
    }
}
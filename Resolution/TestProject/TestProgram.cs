using System;
using Resolution.Sentences;
using Resolution.Visitors;
using Resolution.Visitors.ConjunctionExclusion;

namespace Resolution
{
    public class TestProgram
    {
        public static void Main()
        {
            Sentence sentence = new ComplexSentence(
                Connective.OR,
                new ComplexSentence(
                    Connective.AND, new Literal( "p" ), new Literal( "q" ), new Literal( "r" )
                ),
                new Literal( "s" ),
                new Literal( "t" )
            );

            Console.WriteLine( sentence );

            var visitor = new ConjunctionExclusionVisitor();
            var conjunctionDetector = new ConjunctionDetecionVisitor();
            Console.WriteLine( "dupa" );
            while (conjunctionDetector.DetectConjunction( sentence ))
            {
                Console.WriteLine( sentence );
                visitor.Visit( sentence );
            }

            Console.WriteLine( sentence );
        }
    }
}
using System;
using Resolution;
using Resolution.Sentences;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var sentence = new ComplexSentence(
                Connective.IMPLICATION,
                new ComplexSentence(
                    Connective.AND,
                    new Literal("ból gardła"),
                    new Literal("gorączka"),
                    new Literal("biegunka", negated: true)
                ),
                new Literal("angina")
            );

            Console.WriteLine(sentence);

            var converter = new CNFConverter();
            var clauses = converter.ConvertToCNF(sentence);

            clauses.ForEach(Console.WriteLine);
        }
    }
}
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
                Connective.AND,
                new ComplexSentence(
                    Connective.IMPLICATION,
                    new ComplexSentence(
                        Connective.AND, new Literal("ból gardła"), new Literal("gorączka"),
                        new Literal("biegunka", negated: true)
                    ),
                    new Literal("angina")
                ),
                new ComplexSentence(
                    Connective.IMPLICATION,
                    new ComplexSentence(
                        Connective.AND,
                        new Literal("gorączka"), new Literal("ból gardła", negated: true),
                        new ComplexSentence(Connective.OR, new Literal("biegunka"), new Literal("ból głowy"))
                    ),
                    new Literal("przemęczenie")
                ),
                new ComplexSentence(Connective.AND, new Literal("ból gardła"), new Literal("gorączka")),
                new Literal("angina", negated: true)
            );

            Console.WriteLine(sentence);

            var converter = new CNFConverter();
            var clauses = converter.ConvertToCNF(sentence);

            Console.WriteLine("Clauses:");
            clauses.ForEach(Console.WriteLine);
        }
    }
}
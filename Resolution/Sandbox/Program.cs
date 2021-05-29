using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Resolution;
using Resolution.Parser;
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
            string file1 = "./../../../diseasesSetFile.dis";
            Sentence usedSentence = sentence;
            var sentences = FileReader.ReadFileX(file1);
            foreach (Sentence sentence1 in sentences)
            {
                Console.WriteLine(sentence1.ToString());
                Console.WriteLine("Equals: " + sentence.Equals(sentence1).ToString());
                usedSentence = sentence1;
            }


            var converter = new CNFConverter();
            var clauses = converter.ConvertToCNF(usedSentence);

            Console.WriteLine("Clauses:");
            clauses.ForEach(Console.WriteLine);

            Example("./../../../exampleSet.dis");
        }

        private static void Example(string exampleFileSet)
        {
            Console.WriteLine("------------------------EXAMPLE---------------------------------------");
            // symptoms
            Sentence headache = new Literal("headache");
            Sentence soreThroat = new Literal("sore_throat");
            Sentence musclePain = new Literal("muscle_pain");

            IEnumerable<Sentence> sentences = FileReader.ReadFileX(exampleFileSet);
            List<Sentence> kb = sentences.ToList();
            kb.Add(headache);
            kb.Add(soreThroat);
            kb.Add(musclePain);

            var disease = new Literal("flu");
            if (AutomatedReasoning.Resolution(kb, disease))
            {
                Console.WriteLine("Diagnosis: " + disease);
            }

            Console.WriteLine("------------------------------------------------------------------");
            

        }
    }
}
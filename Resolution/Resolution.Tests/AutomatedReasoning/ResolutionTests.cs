using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resolution.Sentences;
using System.Collections.Generic;

namespace Resolution.Tests.AutomatedReasoningTests
{
    [TestClass]
    public class ResolutionTests
    {
        [TestMethod]
        public void TestGoodExample()
        {
            Sentence headacheDiagnosis = new ComplexSentence(Connective.OR, new Literal("flu"), new Literal("chronic_fatigue"));
            Sentence soreThroatDiagnosis = new ComplexSentence(Connective.OR, new Literal("flu"), new Literal("chronic_fatigue"));

            Sentence headacheImplication = new ComplexSentence(Connective.IMPLICATION, new Literal("headache"), headacheDiagnosis);
            Sentence soreThroatImplication = new ComplexSentence(Connective.IMPLICATION, new Literal("sore_throat"), soreThroatDiagnosis);
            Sentence musclePainImplication = new ComplexSentence(Connective.IMPLICATION, new Literal("muscle_pain"), new Literal("flu"));
            Sentence musclePainNotImplication = new ComplexSentence(Connective.IMPLICATION, new Literal("sore_throat"), new Literal("chronic_fatigue", true));

            Sentence headache = new Literal("headache");
            Sentence soreThroat = new Literal("sore_throat");
            Sentence musclePain = new Literal("muscle_pain");
            var kb = new List<Sentence>
            {
                headacheDiagnosis,
                soreThroatDiagnosis,
                headacheImplication,
                soreThroatImplication,
                musclePainImplication,
                musclePainNotImplication,
                headache,
                soreThroat,
                musclePain
            };


            var result = AutomatedReasoning.Resolution(kb, new Literal("flu"));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestBadExample()
        {
            Sentence headacheDiagnosis = new ComplexSentence(Connective.OR, new Literal("flu"), new Literal("chronic_fatigue"));
            Sentence soreThroatDiagnosis = new ComplexSentence(Connective.OR, new Literal("flu"), new Literal("chronic_fatigue"));

            Sentence headacheImplication = new ComplexSentence(Connective.IMPLICATION, new Literal("headache"), headacheDiagnosis);
            Sentence soreThroatImplication = new ComplexSentence(Connective.IMPLICATION, new Literal("sore_throat"), soreThroatDiagnosis);
            Sentence musclePainImplication = new ComplexSentence(Connective.IMPLICATION, new Literal("muscle_pain"), new Literal("flu"));
            Sentence musclePainNotImplication = new ComplexSentence(Connective.IMPLICATION, new Literal("sore_throat"), new Literal("chronic_fatigue", true));

            Sentence headache = new Literal("headache");
            Sentence soreThroat = new Literal("sore_throat");
            Sentence musclePain = new Literal("muscle_pain");
            var kb = new List<Sentence>
            {
                headacheDiagnosis,
                soreThroatDiagnosis,
                headacheImplication,
                soreThroatImplication,
                musclePainImplication,
                musclePainNotImplication,
                headache,
                soreThroat,
                musclePain
            };


            var result = AutomatedReasoning.Resolution(kb, new Literal("chronic_fatigue"));

            Assert.IsFalse(result);
        }
    }
}
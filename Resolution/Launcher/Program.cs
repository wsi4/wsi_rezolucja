using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Resolution;
using Resolution.Parser;
using Resolution.Parser.Patient;
using Resolution.Sentences;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string axiomsFile;
            string patientsFile;

            if (args.Length == 2)
            {
                axiomsFile = args[0];
                patientsFile = args[1];
            }
            else
            {
                Console.WriteLine("Please provide two launch parameters:");
                Console.WriteLine("Usage: ResolutionLauncher kb.dis patients.pat");
                return;
            }
            
            var diseaseAxioms = FileReader.ReadFileX(axiomsFile);
            var patients = PatientParser.ReadFile(patientsFile);

            MakeDiagnosis(patients, diseaseAxioms);
        }

        private static void MakeDiagnosis(IEnumerable<Patient> patients, IEnumerable<Sentence> diseaseAxioms)
        {
            foreach (var patient in patients)
            {
                var kb = diseaseAxioms.ToList();

                Console.Write($"{patient.Name}, {patient.Diagnosis}: ");
                if (AutomatedReasoning.Resolution(kb, patient.Symptoms, patient.NotSymptoms, patient.Diagnosis))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Yes");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No");
                    Console.ResetColor();
                }
            }
        }
    }
}
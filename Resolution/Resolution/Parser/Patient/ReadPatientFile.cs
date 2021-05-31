using Resolution.Sentences;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Resolution.Parser.Patient
{
    public record Patient(string Name, IEnumerable<Sentence> Symptoms, IEnumerable<Sentence> NotSymptoms, string Diagnosis);
    
    public class PatientParser
    {
        public static IEnumerable<Patient> ReadFile(string path)
        {
            var patients = new List<Patient>();
            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split('|');
                var name = parts[0].Trim();
                var symptoms = parts[1].Split(',').ToList();
                var notSymptoms = parts[2].Split(',').ToList();
                var diagnosis = parts[3].Trim();

                patients.Add(new Patient(name, symptoms.Select(s => new Literal(s.Trim())),
                    notSymptoms.Select(s => new Literal(s.Trim(), true)), diagnosis));
            }

            return patients;
        }
    }
}

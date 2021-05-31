using Resolution.Clauses;
using Resolution.Sentences;
using Resolution.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resolution
{
    public static class AutomatedReasoning
    {
        public static bool Resolution(IEnumerable<Sentence> kb, IEnumerable<Sentence> symptoms, IEnumerable<Sentence> notSymptoms, string disease)
        {
            var cNFConverter = new CNFConverter();

            kb = kb.Where(a => a.Contains(disease));
            kb = kb.Concat(symptoms);
            kb = kb.Concat(notSymptoms);

            Sentence alpha = new Literal(disease);
            alpha.Negate();

            var clausesSet = kb.Append(alpha)
                .Aggregate(Enumerable.Empty<Clause>(), 
                    (acc, current) => acc.Concat(cNFConverter.ConvertToCNF(current))).ToHashSet();

            var newClauses = new HashSet<Clause>();

            while (true)
            {
                var clausesList = clausesSet.ToList();

                for (int i = 0; i < clausesList.Count; i++)
                {
                    Clause ci = clausesList[i];

                    for (int j = i + 1; j < clausesList.Count; j++)
                    {
                        Clause cj = clausesList[j];

                        var resolvents = Resolve(ci, cj);
                        if (resolvents.Any(c => c.Empty))
                        {
                            return true;
                        }

                        newClauses.UnionWith(resolvents);
                    }
                }

                if (clausesSet.ContainsAll(newClauses))
                {
                    return false;
                }

                clausesSet.UnionWith(newClauses);

            }
        }

        private static ISet<Clause> Resolve(Clause ci, Clause cj)
        {
            var resolvents = ResolvePositiveWithNegative(ci, cj);
            resolvents.UnionWith(ResolvePositiveWithNegative(cj, ci));

            return resolvents;
        }

        private static ISet<Clause> ResolvePositiveWithNegative(Clause c1, Clause c2)
        {
            var complementary = c1.PositiveLiterals.Select(p => p.Symbol)
                .Intersect(c2.NegativeLiterals.Select(p => p.Symbol)).ToList();
            var resolvents = new HashSet<Clause>();

            foreach (var complement in complementary)
            {
                var resolventLiterals = new List<Literal>();

                foreach (var c1l in c1.Literals)
                {
                    if (c1l.Negated || !(c1l.Symbol == complement))
                    {
                        resolventLiterals.Add(c1l);
                    }
                }

                foreach (var c2l in c2.Literals)
                {
                    if (!c2l.Negated || !(c2l.Symbol == complement))
                    {
                        resolventLiterals.Add(c2l);
                    }
                }

                resolvents.Add(new Clause(resolventLiterals));
            }

            return resolvents;
        }
    }
}

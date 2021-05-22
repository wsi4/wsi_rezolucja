using Resolution.Clauses;
using Resolution.Sentences;
using System.Collections.Generic;
using System.Linq;

namespace Resolution
{
    public static class SetExtensions
    {
        public static bool ContainsAll<T>(this ISet<T> set, IEnumerable<T> enumerable)
        {
            return enumerable.All(e => set.Contains(e));
        }
    }

    public static class AutomatedReasoning
    {
        public static bool Resolution(IEnumerable<Sentence> kb, Sentence alpha)
        {
            var cNFConverter = new CNFConverter();

            var alphaClone = (Sentence)alpha.Clone();
            alphaClone.Negate();

            var clausesSet = kb.Append(alphaClone)
                .Aggregate(Enumerable.Empty<Clause>(), (acc, current) => acc.Concat(cNFConverter.ConvertToCNF(current))).ToHashSet();
            var newClauses = new List<Clause>();

            do
            {
                var clausesList = clausesSet.ToList();

                for (int i = 0; i < clausesList.Count; i++)
                {
                    var ci = clausesList[i];
                    for (int j = i + 1; j < clausesList.Count; j++)
                    {
                        Clause cj = clausesList[j];

                        var resolvents = Resolve(ci, cj);
                        if (resolvents.Any(c => c.Empty))
                        {
                            return true;
                        }

                        newClauses.AddRange(resolvents);
                    }
                }

                if (clausesSet.ContainsAll(newClauses))
                {
                    return false;
                }

                clausesSet.UnionWith(newClauses);

            } while (true);
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
                .Intersect(c2.NegativeLiterals.Select(p => p.Symbol));
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution.Utilities
{
    public static class SetExtensions
    {
        public static bool ContainsAll<T>(this ISet<T> set, IEnumerable<T> enumerable)
        {
            return enumerable.All(e => set.Contains(e));
        }
    }
}

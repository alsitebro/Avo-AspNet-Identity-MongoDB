using System.Collections.Generic;
using System.Linq;

namespace Avo.AspNet.Identity.MongoDB
{
    public static class Extensions
    {
        internal static IList<T> ToIList<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }
    }
}

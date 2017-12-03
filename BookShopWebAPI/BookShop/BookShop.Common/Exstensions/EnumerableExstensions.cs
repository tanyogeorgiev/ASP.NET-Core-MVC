
namespace BookShop.Common.Exstensions
{
    using System.Collections.Generic;

    public static class EnumerableExstensions
    {
        public static ISet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }
    }
}

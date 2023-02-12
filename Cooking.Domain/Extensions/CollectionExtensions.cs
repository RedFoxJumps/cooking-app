using Cooking.Domain.Models;

namespace Cooking.Domain.Extensions;

public static class CollectionExtensions
{
    private static Lazy<Random> r = new (() => new Random(123));

    public static T Random<T>(this IEnumerable<T> list, params string[] tags)
        where T : Dish
    {
        var filtered = list.Where(x => x.Tags.Any(tag => tags.Contains(tag)));
        return filtered.ElementAt(r.Value.Next(filtered.Count()));
    }

    public static IEnumerable<T> Random<T>(this IEnumerable<T> list, int cound)
    {
        var maxTake = Math.Min(list.Count(), cound);

        return list.Shuffle().Take(maxTake);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.OrderBy(x => r.Value.Next()).ToList();
    }

    public static void AddUnique<T>(this List<T> collection, IEnumerable<T> values)
        where T : Dish
    {
        var initialCollection = collection.Select(x => x.Name);
        var unique = values.Where(x => !initialCollection.Contains(x.Name));
        collection.AddRange(unique);
    }
}

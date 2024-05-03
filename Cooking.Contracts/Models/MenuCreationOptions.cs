namespace Cooking.Contracts.Models;

public class MenuCreationOptions
{
    public DishSelector TagsIntersectionPredicate { get; init; } = DishSelectors.AnyPredicate;

    public DishTag[] ContainsTags { get; init; } = Array.Empty<DishTag>();

    public DishTag[] DoesNotContainAllOf { get; init; } = Array.Empty<DishTag>();
}

public static class DishSelectors
{
    public static DishSelector AllPredicate { get; } = Enumerable.All;

    public static DishSelector AnyPredicate { get; } = Enumerable.Any;
}

public delegate bool DishSelector(IEnumerable<DishTag> tags, Func<DishTag, bool> predicate);
using Cooking.Domain.Models;

namespace Cooking.Domain.Processors.Extensions.Filters;

public abstract class FilterType
{
    public static FilterType OfType => OfTypeFilterType.Instance;

    public static FilterType Not => NotFilterType.Instance;

    public static FilterType Only => OnlyFilterType.Instance;

    public abstract IEnumerable<Dish> Filter(IEnumerable<Dish> dishes, IEnumerable<Tag> tags);
}

internal class OfTypeFilterType : FilterType
{
    private static Lazy<FilterType> FilterTypeLazy
        => new Lazy<FilterType>(() => new OfTypeFilterType());

    internal static FilterType Instance => FilterTypeLazy.Value;

    public override IEnumerable<Dish> Filter(IEnumerable<Dish> dishes, IEnumerable<Tag> tags)
    {
        return dishes.Where(x => x.Tags.Intersect(tags).Any());
    }
}

internal class NotFilterType : FilterType
{
    private static Lazy<FilterType> FilterTypeLazy
        => new Lazy<FilterType>(() => new NotFilterType());

    internal static FilterType Instance => FilterTypeLazy.Value;

    public override IEnumerable<Dish> Filter(IEnumerable<Dish> dishes, IEnumerable<Tag> tags)
    {
        var v = dishes.Where(x => !x.Tags.Intersect(tags).Any());
        return v;
    }
}

[Obsolete(message: "seems unreal and hardly usable")]
internal class OnlyFilterType : FilterType
{
    private static Lazy<FilterType> FilterTypeLazy
        => new Lazy<FilterType>(() => new OnlyFilterType());

    internal static FilterType Instance => FilterTypeLazy.Value;

    public override IEnumerable<Dish> Filter(IEnumerable<Dish> dishes, IEnumerable<Tag> tags)
    {
        return dishes
            .Where(x => tags.OrderBy(x => x)
                .SequenceEqual(x.Tags.OrderBy(x => x)));
    }
}

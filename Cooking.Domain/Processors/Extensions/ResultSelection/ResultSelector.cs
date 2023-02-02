using Cooking.Domain.Extensions;
using Cooking.Domain.Models;

namespace Cooking.Domain.Processors.Extensions.ResultSelection;

public abstract class ResultSelector
{
    public static ResultSelector Single => SingleResultSelector.Instance;

    public static ResultSelector All => AllResultSelector.Instance;

    public static ResultSelector Of(int count) => MultipleResultSelector.For(count);

    public abstract IEnumerable<Dish> Select(IEnumerable<Dish> dishes);
}

internal class SingleResultSelector : ResultSelector
{
    private static Lazy<ResultSelector> SelectorLazy
        => new Lazy<ResultSelector>(() => new SingleResultSelector());

    internal static ResultSelector Instance => SelectorLazy.Value;

    public override IEnumerable<Dish> Select(IEnumerable<Dish> dishes)
    {
        return dishes.Random(1);
    }
}

internal class MultipleResultSelector : ResultSelector
{
    private int _count;

    public MultipleResultSelector(int count)
    {
        _count = count;
    }

    public override IEnumerable<Dish> Select(IEnumerable<Dish> dishes)
    {
        return dishes.Random(_count);
    }

    internal static ResultSelector For(int count)
    {
        return new MultipleResultSelector(count);
    }
}

internal class AllResultSelector : ResultSelector
{
    private static Lazy<ResultSelector> SelectorLazy
        => new Lazy<ResultSelector>(() => new AllResultSelector());

    internal static ResultSelector Instance => SelectorLazy.Value;

    public override IEnumerable<Dish> Select(IEnumerable<Dish> dishes) => dishes.ToArray();
}
using Cooking.Domain.Extensions;
using Cooking.Domain.Models;

namespace Cooking.Domain.Processors.Extensions.Exclusion;

internal class ExcludeRecentProcessor : DishProcessorBase, IDishProcessor
{
    private readonly IEnumerable<Dish> _exclusion;

    public ExcludeRecentProcessor(IEnumerable<Dish> exclusion)
    {
        _exclusion = exclusion;
    }

    protected override void ProcessInternal(MenuContext menuContext)
    {
        menuContext.Excluded.AddUnique(_exclusion);
    }
}

public static class ExcludeRecentExtension
{
    public static IDishProcessor ExcludeRecent(this IDishProcessor processor, IEnumerable<Dish> exclusion)
    {
        var proc = new ExcludeRecentProcessor(exclusion);
        processor.Next = proc;
        return proc;
    }
}

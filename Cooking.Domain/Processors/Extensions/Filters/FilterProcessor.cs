using Cooking.Domain.Extensions;
using Cooking.Domain.Models;

namespace Cooking.Domain.Processors.Extensions.Filters;

internal class FilterProcessor : DishProcessorBase, IDishProcessor
{
    private readonly FilterType _filterType;
    private readonly IEnumerable<Tag> _tags;

    public FilterProcessor(FilterType filterType, IEnumerable<Tag> tags)
    {
        _filterType = filterType;
        _tags = tags;
    }

    protected override void ProcessInternal(MenuContext menuContext)
    {
        var contextExclude = menuContext.Excluded
            .Select(x => x)
            .Concat(menuContext.CurrentMenu)
            .Select(x => x.Name);

        var catalogueWithoutExclusions = menuContext.DishesCatalogue.ExceptBy(contextExclude, x => x.Name);
        var filtered = _filterType.Filter(catalogueWithoutExclusions, _tags);

        menuContext.FilterMenu.AddUnique(filtered);
    }
}

public static class FilterExtension
{
    public static IDishProcessor AddToMenu(this IDishProcessor processor, FilterType filterType, params Tag[] tags)
    {
        var proc = new FilterProcessor(filterType, tags);
        processor.Next = proc;
        return proc;
    }
}

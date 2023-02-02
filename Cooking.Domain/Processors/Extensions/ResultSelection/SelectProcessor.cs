using Cooking.Domain.Extensions;

namespace Cooking.Domain.Processors.Extensions.ResultSelection;

internal class SelectProcessor : DishProcessorBase, IDishProcessor
{
    private readonly ResultSelector _selector;

    public SelectProcessor(ResultSelector selector)
    {
        _selector = selector;
    }

    protected override void ProcessInternal(MenuContext menuContext)
    {
        var dishes = _selector.Select(menuContext.FilterMenu);
        menuContext.FilterMenu.Clear();
        menuContext.CurrentMenu.AddUnique(dishes);

        menuContext.Excluded.AddUnique(dishes);
    }
}

public static class SelectExtension
{
    public static IDishProcessor Select(this IDishProcessor processor, ResultSelector selector)
    {
        var proc = new SelectProcessor(selector);
        processor.Next = proc;
        return proc;
    }
}

using Cooking.Domain.Models;

namespace Cooking.Domain.Processors.Extensions.Filters;

internal class ButOfProcessor : DishProcessorBase, IDishProcessor
{
    private Tag[] _tags;

    public ButOfProcessor(Tag[] tags)
    {
        _tags = tags;
    }

    protected override void ProcessInternal(MenuContext menuContext)
    {
        var ofTag = menuContext.FilterMenu.Where(x => x.Tags.Intersect(_tags).Any()).ToList();
        ofTag.ForEach(x => menuContext.FilterMenu.Remove(x));
    }
}

public static class ButOfExtension
{
    public static IDishProcessor ButOf(this IDishProcessor processor, params Tag[] tags)
    {
        var proc = new ButOfProcessor(tags);
        processor.Next = proc;
        return proc;
    }
}

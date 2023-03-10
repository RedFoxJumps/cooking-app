namespace Cooking.Domain.Processors.Extensions.Filters;

internal class ButOfProcessor : DishProcessorBase, IDishProcessor
{
    private string[] _tags;

    public ButOfProcessor(string[] tags)
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
    public static IDishProcessor ButOf(this IDishProcessor processor, params string[] tags)
    {
        var proc = new ButOfProcessor(tags);
        processor.Next = proc;
        return proc;
    }
}

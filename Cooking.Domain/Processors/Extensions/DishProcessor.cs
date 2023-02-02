namespace Cooking.Domain.Processors.Extensions;

public interface IDishProcessor
{
    IDishProcessor Next { get; set; }

    void Process(MenuContext menuContext);
}

public abstract class DishProcessorBase : IDishProcessor
{
    public IDishProcessor Next { get; set; }

    public void Process(MenuContext menuContext)
    {
        ProcessInternal(menuContext);

        if (Next is not null)
        {
            Next.Process(menuContext);
        }
    }

    protected abstract void ProcessInternal(MenuContext menuContext);
}

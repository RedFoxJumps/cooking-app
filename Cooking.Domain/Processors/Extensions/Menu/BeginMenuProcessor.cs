using Cooking.Domain.Models;

namespace Cooking.Domain.Processors.Extensions.Menu;

public class BeginMenuProcessor : IDishProcessor
{
    private readonly IEnumerable<Dish> _initialDishes;

    public BeginMenuProcessor(IEnumerable<Dish> initialDishes)
    {
        _initialDishes = initialDishes;
    }

    public IDishProcessor Next { get; set; }

    public List<Dish> MakeMenu()
    {
        var context = new MenuContext();
        context.DishesCatalogue.AddRange(_initialDishes);
        Next.Process(context);

        var resultMenu = context.CurrentMenu.ToList();
        context.CurrentMenu.Clear();
        return resultMenu;
    }

    public void Process(MenuContext menuContext)
    {
        MakeMenu();
    }
}

public static class BeginMenuExtension
{
    public static BeginMenuProcessor InitializeMenu(this IEnumerable<Dish> initialDishes)
    {
        return new BeginMenuProcessor(initialDishes);
    }
}

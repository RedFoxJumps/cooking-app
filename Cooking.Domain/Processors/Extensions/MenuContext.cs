using Cooking.Domain.Models;

namespace Cooking.Domain.Processors.Extensions;

public class MenuContext
{
    public List<Dish> CurrentMenu { get; } = new();

    public List<Dish> Excluded { get; } = new();

    public List<Dish> FilterMenu { get; } = new();

    public List<Dish> DishesCatalogue { get; } = new();
}

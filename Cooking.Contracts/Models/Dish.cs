namespace Cooking.Contracts.Models;

public class Dish
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public string[] Tags { get; set; } = Array.Empty<string>();
}

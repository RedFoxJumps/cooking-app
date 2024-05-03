namespace Cooking.Contracts.Models;

public class Menu
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Dish[] Dishes { get; set; } = Array.Empty<Dish>();

    public string Note { get; set; } = "";
}

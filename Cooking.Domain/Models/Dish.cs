namespace Cooking.Domain.Models;

public class Dish
{
    public string Name { get; set; }

    public List<string> Tags { get; set; } = new ();

    public override string ToString()
    {
        return $"{ Name } [ { string.Join(", ", Tags) } ]";
    }

    public static Dish New(string name, params string[] tags) => new()
    {
        Name = name,
        Tags = tags.Distinct().ToList(),
    };
}

public class Tags
{
    public const string Sniadanak = "Сняданак";
    public const string Abied = "Абед";
    public const string Viacera = "Вячэра";
    public const string Salodkae = "Салодкае";
    public const string Vadkae = "Вадкае";
}
namespace Cooking.Domain.Models;

public class Dish
{
    protected Dish()
    { }

    public string Name { get; set; }

    public List<Tag> Tags { get; set; } = new ();

    public override string ToString()
    {
        return $"{ Name } [ { string.Join(", ", Tags) } ]";
    }

    public static Dish New(string name, params Tag[] tags) => new()
    {
        Name = name,
        Tags = tags.Distinct().ToList(),
    };
}

public enum Tag
{
    Vadkae,
    Salodkae,

    Sniadanak,
    Abied,
    Viacera,
}

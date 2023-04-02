namespace Cooking.Domain.Models;

public class DishTag
{
    public int? Id { get; set; }

    /// <summary>
    /// Its 'name'.
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// purpose of the tag.
    /// </summary>
    public string Description { get; set; }
}

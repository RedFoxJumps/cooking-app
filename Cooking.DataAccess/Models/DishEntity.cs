using LinqToDB.Mapping;

namespace Cooking.DataAccess.Models;

[Table("Dish")]
public class DishEntity
{
    [PrimaryKey, Identity, Column(Name = "Id")]
    public int Id { get; set; }

    [NotNull, Column("Name")]
    public string Name { get; set; }
}

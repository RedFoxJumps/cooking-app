using LinqToDB.Mapping;

namespace Cooking.DataAccess.Models;

[Table("MenuDishLink")]
public class MenuDishLinkEntity
{
    [PrimaryKey, Identity, Column(Name = "Id")]
    public int Id { get; set; }

    [NotNull, Column("MenuId")]
    public int MenuId { get; set; }

    [NotNull, Column("DishId")]
    public int DishId { get; set; }
}

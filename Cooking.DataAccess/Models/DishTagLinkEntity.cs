using LinqToDB.Mapping;

namespace Cooking.DataAccess.Models;

[Table("DishTagLink")]
public class DishTagLinkEntity
{
    [PrimaryKey, Identity, Column(Name = "Id")]
    public int Id { get; set; }

    [NotNull, Column("DishId")]
    public int DishId { get; set; }

    [NotNull, Column("TagId")]
    public int TagId { get; set; }
}

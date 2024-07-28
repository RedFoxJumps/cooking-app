using LinqToDB.Mapping;

namespace Cooking.DataAccess.Models;

[Table("DishTag")]
public class DishTagEntity : IIdentity<int>
{
    [PrimaryKey, Identity, Column(Name = nameof(Id))]
    public int Id { get; set; }

    [NotNull, Column("Tag")]
    public string Tag { get; set; }

    [Nullable, Column("Description")]
    public string Description { get; set; }
}

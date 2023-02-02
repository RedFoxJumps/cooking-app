using LinqToDB.Mapping;

namespace Cooking.DataAccess.Models;

[Table("Menu")]
public class MenuEntity
{
    [PrimaryKey, Identity, Column(Name = "Id")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("CreationDate")]
    public DateTime CreationDate { get; set; }
}

using Cooking.DataAccess.Database;
using Cooking.DataAccess.Models;
using LinqToDB;

namespace Cooking.DataAccess.Repository;

public interface IRepository
{
    ITable<DishEntity> Dishes { get; }
    ITable<DishTagEntity> DishTags { get; }
    ITable<DishTagLinkEntity> DishTagLinks { get; }
    ITable<MenuDishLinkEntity> MenuDishLinks { get; }
    ITable<MenuEntity> Menues { get; }
}

public class RepositoryBase : IRepository
{
    private CookingDatabase _cookingDatabase = new CookingDatabase();

    public ITable<DishEntity> Dishes => _cookingDatabase.GetTable<DishEntity>();
    public ITable<DishTagEntity> DishTags => _cookingDatabase.GetTable<DishTagEntity>();
    public ITable<DishTagLinkEntity> DishTagLinks => _cookingDatabase.GetTable<DishTagLinkEntity>();
    public ITable<MenuDishLinkEntity> MenuDishLinks => _cookingDatabase.GetTable<MenuDishLinkEntity>();
    public ITable<MenuEntity> Menues => _cookingDatabase.GetTable<MenuEntity>();
}

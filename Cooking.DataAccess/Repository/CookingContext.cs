using Cooking.DataAccess.Database;
using Cooking.DataAccess.Models;
using LinqToDB;

namespace Cooking.DataAccess.Repository;

public interface ICookingContext
{
    ITable<DishEntity> Dishes { get; }
    ITable<DishTagEntity> DishTags { get; }
    ITable<DishTagLinkEntity> DishTagLinks { get; }
    ITable<MenuDishLinkEntity> MenuDishLinks { get; }
    ITable<MenuEntity> Menues { get; }
}

public class CookingContext : ICookingContext, IDisposable
{
    private CookingDatabase _cookingDatabase;
    private bool disposedValue;

    public CookingContext(CookingDatabase cookingDatabase)
    {
        _cookingDatabase = cookingDatabase;
    }

    public ITable<DishEntity> Dishes => Table<DishEntity>();
    public ITable<DishTagEntity> DishTags => Table<DishTagEntity>();
    public ITable<DishTagLinkEntity> DishTagLinks => Table<DishTagLinkEntity>();
    public ITable<MenuDishLinkEntity> MenuDishLinks => Table<MenuDishLinkEntity>();
    public ITable<MenuEntity> Menues => Table<MenuEntity>();

    private ITable<TEntity> Table<TEntity>()
        where TEntity : class
    {
        return _cookingDatabase.GetTable<TEntity>();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _cookingDatabase.Close();
                _cookingDatabase.Dispose();
            }

            _cookingDatabase = null;
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

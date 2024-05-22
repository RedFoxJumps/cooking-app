using Cooking.DataAccess.Database;
using Cooking.DataAccess.Models;
using LinqToDB;

namespace Cooking.DataAccess.Repository;

public interface IRepository<TEntity>
    where TEntity : class, IIdentity<int>
{
    Task<TEntity> GetById(int id);

    IQueryable<TEntity> GetAll();

    Task<int> Insert(TEntity entity);

    Task Insert(ICollection<TEntity> entities);
}

public class Repository<TEntity> : IRepository<TEntity>
	where TEntity : class, IIdentity<int>
{
    private readonly CookingDatabase _cookingDatabase;
	private readonly ITable<TEntity> _table;

	public Repository(CookingDatabase cookingDatabase)
	{
		_cookingDatabase = cookingDatabase;
		_table = cookingDatabase.GetTable<TEntity>();
    }

    public IQueryable<TEntity> GetAll() => _table;

    public Task<TEntity> GetById(int id) => _table.FirstOrDefaultAsync(x => x.Id == id);

    // TODO: insertOrUpdate ?
    public async Task<int> Insert(TEntity entity)
	{
        var id = await _cookingDatabase.InsertWithInt32IdentityAsync(entity);
        return entity.Id = id;
    }

    /// <summary>
    /// Inserts a collection of <typeparamref name="TEntity"/> into database with object id update.
    /// </summary>
    public async Task Insert(ICollection<TEntity> entities)
    {
        var insert = (TEntity e) => Insert(e)
            .ContinueWith(t => e.Id == t.Result);

        var insertions = entities.Select(x => Insert(x).ContinueWith(t => x.Id = t.Result));
        await Task.WhenAll(insertions);
    }
}

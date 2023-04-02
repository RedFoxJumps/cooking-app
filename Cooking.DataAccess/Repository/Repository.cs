using Cooking.DataAccess.Database;
using LinqToDB;

namespace Cooking.DataAccess.Repository;

public interface IRepository<TEntity>
    where TEntity : class, IIdentity<int>
{
    Task<TEntity> GetById(int id);

	IQueryable<TEntity> GetAll();

    Task<int> Insert(TEntity entity);
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

	public IQueryable<TEntity> GetAll()
	{
		return _table;
    }
}

using Cooking.DataAccess.Database;
using LinqToDB;

namespace Cooking.DataAccess.Repository;

public interface IRepository<TEntity>
{
	Task<int> Insert(TEntity entity);

	IQueryable<TEntity> GetAll();
}

public class Repository<TEntity> : IRepository<TEntity>
	where TEntity : class
{
    private readonly CookingDatabase _cookingDatabase;
	private readonly ITable<TEntity> _table;

	public Repository(CookingDatabase cookingDatabase)
	{
		_cookingDatabase = cookingDatabase;
		_table = cookingDatabase.GetTable<TEntity>();
    }

	public async Task<int> Insert(TEntity entity)
	{
		return await _cookingDatabase.InsertWithInt32IdentityAsync(entity);
    }

	public IQueryable<TEntity> GetAll()
	{
		return _table;
    }
}

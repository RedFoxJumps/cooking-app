using LinqToDB;
using LinqToDB.Data;

namespace Cooking.DataAccess.Database;

public class CookingDatabase : DataConnection
{
    public CookingDatabase()
        : base(ProviderName.SQLiteMS, Constants.ConnectionString)
    { }
}

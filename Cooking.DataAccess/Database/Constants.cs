namespace Cooking.DataAccess.Database;

internal class Constants
{
    internal const string DatabaseName = "PrefilledDB";
    internal static readonly string DatabaseFile = $@"{AppContext.BaseDirectory}\Data\{DatabaseName}.sqlite";
    internal static readonly string ConnectionString = $@"Data Source={DatabaseFile}";
}

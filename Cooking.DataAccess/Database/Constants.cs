namespace Cooking.DataAccess.Database;

internal class Constants
{
    internal const string DatabaseName = "PrefilledDB";
    internal static readonly string Directory = $@"{AppContext.BaseDirectory}\Data";
    internal static readonly string DatabaseFile = $@"{Directory}\{DatabaseName}.sqlite";
    internal static readonly string ConnectionString = $@"Data Source={DatabaseFile}";
}

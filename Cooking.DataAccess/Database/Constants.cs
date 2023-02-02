namespace Cooking.DataAccess.Database;

internal class Constants
{
    internal const string DatabaseName = "PrefilledDB";
    internal const string DatabaseFile = $@".\Data\{DatabaseName}.sqlite";
    internal const string ConnectionString = $@"Data Source={DatabaseFile}";
}

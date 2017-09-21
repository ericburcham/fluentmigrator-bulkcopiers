namespace FluentMigrator.BulkCopiers.IntegrationTests
{
    internal static class IntegrationTestOptions
    {
        public static readonly DatabaseServerOptions SqlServer = new DatabaseServerOptions
        {
            ConnectionString = Configuration.SqlServer.ConnectionString
        };

        public class DatabaseServerOptions
        {
            public string ConnectionString;
            public bool IsEnabled = true;
        }
    }
}
namespace FluentMigrator.BulkCopiers.IntegrationTests
{
    internal static class Constants
    {
        public static class SqlServer
        {
            public const string CREATE_DATABASE_QUERY = "CREATE DATABASE " + DATABASE_NAME + ";";

            public const string DATABASE_NAME = "FluentMigrator_BulkCopiers";
            
            public static string DROP_DATABASE_QUERY = "DROP DATABASE IF EXISTS " + DATABASE_NAME + ";";
        }
    }
}
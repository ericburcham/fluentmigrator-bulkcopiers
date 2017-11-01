namespace FluentMigrator.BulkCopiers.IntegrationTests
{
    internal static class Constants
    {
        public static class SqlServer
        {
            public const string CREATE_DATABASE_COMMAND = "CREATE DATABASE " + DATABASE_NAME + ";";

            public const string DATABASE_NAME = "FluentMigrator_BulkCopiers";
            
            public const string DROP_DATABASE_COMMAND = "DROP DATABASE IF EXISTS " + DATABASE_NAME + ";";
        }
    }
}
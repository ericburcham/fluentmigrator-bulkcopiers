using System.Configuration;
using System.Data.SqlClient;

namespace FluentMigrator.BulkCopiers.IntegrationTests
{
    internal static class Configuration
    {
        public static class SqlServer
        {
            private static string _connectionString;
            private static string _masterConnectionString;

            private static string BaseConnectionString { get; }

            static SqlServer()
            {
                BaseConnectionString = ConfigurationManager.ConnectionStrings["sqlServerConnectionString"].ConnectionString;
            }

            public static string ConnectionString
            {
                get
                {
                    if (_connectionString == null)
                    {
                        var connectionStringBuilder = GetSqlServerConnectionStringBuilder();
                        connectionStringBuilder.InitialCatalog = Constants.SqlServer.DATABASE_NAME;
                        _connectionString = connectionStringBuilder.ConnectionString;
                    }

                    return _connectionString;
                }
            }

            public static string MasterConnectionString
            {
                get
                {
                    if (_masterConnectionString == null)
                    {
                        var connectionStringBuilder = GetSqlServerConnectionStringBuilder();
                        connectionStringBuilder.InitialCatalog = "master";
                        _masterConnectionString = connectionStringBuilder.ConnectionString;
                    }

                    return _masterConnectionString;
                }
            }

            private static SqlConnectionStringBuilder GetSqlServerConnectionStringBuilder()
            {
                return new SqlConnectionStringBuilder(BaseConnectionString);
            }
        }
    }
}
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace FluentMigrator.BulkCopiers.IntegrationTests.Processors.SqlServer
{
    [SetUpFixture]
    public class SqlServerSetupFixture
    {
        private readonly string _connectionString;

        public SqlServerSetupFixture()
        {
            _connectionString = Configuration.SqlServer.MasterConnectionString;
        }
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(Constants.SqlServer.CREATE_DATABASE_QUERY, connection)
                {
                    CommandType = CommandType.Text
                };
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(Constants.SqlServer.DROP_DATABASE_QUERY, connection)
                {
                    CommandType = CommandType.Text
                };
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
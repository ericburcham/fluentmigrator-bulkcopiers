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
                connection.Open();
                var dropCommand = new SqlCommand(Constants.SqlServer.DROP_DATABASE_QUERY, connection)
                {
                    CommandType = CommandType.Text
                };
                dropCommand.ExecuteNonQuery();
                dropCommand.Dispose();
                
                var createCommand = new SqlCommand(Constants.SqlServer.CREATE_DATABASE_QUERY, connection)
                {
                    CommandType = CommandType.Text
                };
                createCommand.ExecuteNonQuery();
                createCommand.Dispose();
                connection.Close();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            SqlConnection.ClearAllPools();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = Constants.SqlServer.DROP_DATABASE_QUERY;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
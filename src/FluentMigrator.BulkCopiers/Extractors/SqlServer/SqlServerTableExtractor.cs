using System;
using System.Data.SqlClient;

namespace FluentMigrator.BulkCopiers
{
    public class SqlServerTableExtractor : IExtractTabularData
    {
        public SqlServerTableExtractor(SqlConnection connection, IPersistTabularData persister, string sales, string customers)
        {
            throw new NotImplementedException();
        }

        public void Extract(string path)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.SqlServer;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;
using NUnit.Framework;

namespace FluentMigrator.BulkCopiers.IntegrationTests.Processors.SqlServer.SqlServerTableExtractorTests
{
    [TestFixture]
    public class When_Extract_Is_Invoked
    {
        private Type _type;
        private MigrationRunner _runner;

        private Assembly Assembly => Type.Assembly;
        private string Namespace => Type.Namespace;
        private Type Type => _type ?? (_type = GetType());
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Red
            // Green
            // Refactor
            
            // Configure runner
            SqlConnection connection;
            try
            {
                connection = new SqlConnection(IntegrationTestOptions.SqlServer.ConnectionString);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            var generator = new SqlServer2014Generator();
            var announcer = new TextWriterAnnouncer(new DebugTextWriter());
            var options = new ProcessorOptions();
            var factory = new SqlServerDbFactory();
            var processor = new SqlServerProcessor(connection, generator, announcer, options, factory);
            var runnerContext = new RunnerContext(announcer)
            {
                Namespace = Namespace
            };
            _runner = new MigrationRunner(Assembly, runnerContext, processor);
            
            // Create supporting schema and data.
            _runner.Up(new SupportingMigration());
            
            // Execute
            var path = Directory.GetCurrentDirectory();
            IPersistTabularData persister = new BinaryPersister();
            IExtractTabularData extractor = new SqlServerTableExtractor(connection, persister, "Sales", "Customers");
            extractor.Extract(path);
        }

        [Test]
        public void The_Persisted_DataTable_Should_Be_Valid()
        {
            // TODO Implement test.
            Assert.Pass();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Destroy supporting schema and data.
            try
            {
                _runner.Down(new SupportingMigration());
            }
            finally
            {
                _runner.VersionLoader.RemoveVersionTable();
            }
        }
    }

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

    public interface IExtractTabularData
    {
        void Extract(string path);
    }

    public class BinaryPersister : IPersistTabularData
    {
    }

    public interface IPersistTabularData
    {
    }

    public class SupportingMigration : Migration
    {
        public override void Up()
        {
            throw new NotImplementedException();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }

    public sealed class DebugTextWriter : StreamWriter
    {
        public DebugTextWriter() : base(new DebugOutStream(), Encoding.Unicode, 1024)
        {
            AutoFlush = true;
        }

        class DebugOutStream : Stream
        {
            public override void Write(byte[] buffer, int offset, int count)
            {
                Debug.Write(Encoding.Unicode.GetString(buffer, offset, count));
            }

            public override bool CanRead => false;
            public override bool CanSeek => false;
            public override bool CanWrite => true;
            public override void Flush() { Debug.Flush(); }
            public override long Length => throw new InvalidOperationException();
            public override int Read(byte[] buffer, int offset, int count) { throw new InvalidOperationException(); }
            public override long Seek(long offset, SeekOrigin origin) { throw new InvalidOperationException(); }
            public override void SetLength(long value) { throw new InvalidOperationException(); }
            public override long Position
            {
                get => throw new InvalidOperationException();
                set => throw new InvalidOperationException();
            }
        };
    }}
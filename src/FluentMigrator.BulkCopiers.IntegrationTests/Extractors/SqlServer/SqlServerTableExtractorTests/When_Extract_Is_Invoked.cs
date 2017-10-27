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

namespace FluentMigrator.BulkCopiers.IntegrationTests.Extractors.SqlServer.SqlServerTableExtractorTests
{
    [TestFixture]
    public class When_Extract_Is_Invoked
    {
        private Type _type;
        private MigrationRunner _runner;

        private Assembly Assembly => Type.Assembly;

        private string Namespace => Type.Namespace;

        private Type Type => _type ?? (_type = GetType());

        private SqlConnection _connection;
        private SqlServerProcessor _processor;

        ~When_Extract_Is_Invoked()
        {
            _processor.Dispose();
            _connection.Dispose();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Red
            // Green
            // Refactor

            // Configure runner
            _connection = new SqlConnection(IntegrationTestOptions.SqlServer.ConnectionString);
            _connection.Open();

            var generator = new SqlServer2014Generator();
            var announcer = new TextWriterAnnouncer(new DebugTextWriter());
            var options = new ProcessorOptions();
            var factory = new SqlServerDbFactory();
            _processor = new SqlServerProcessor(_connection, generator, announcer, options, factory);
            var runnerContext = new RunnerContext(announcer)
            {
                Namespace = Namespace
            };
            _runner = new MigrationRunner(Assembly, runnerContext, _processor);

            // Create supporting schema and data.
            _runner.Up(new SupportingMigration());

            // Execute
            var path = Directory.GetCurrentDirectory();
            IPersistTabularData persister = new BinaryPersister();
            IExtractTabularData extractor = new SqlServerTableExtractor(_connection, persister, "Sales", "Customers");
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
                _runner.VersionLoader.RemoveVersionTable();
            }
            finally
            {
                _connection.Close();
            }
        }
    }

    public class SupportingMigration : Migration
    {
        public override void Up()
        {
            Create.Schema("Sales");
            Create.Table("Customers").InSchema("Sales")
                .WithColumn("CustomerId").AsInt64().Identity().PrimaryKey()
                .WithColumn("Name").AsString();
            Insert.IntoTable("Customer").InSchema("Sales")
                .Row(new {Name = "Customer Name"});
        }

        public override void Down()
        {
            Delete.Table("Customers").InSchema("Sales");
            Delete.Schema("Sales");
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

            public override void Flush()
            {
                Debug.Flush();
            }

            public override long Length => throw new InvalidOperationException();

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new InvalidOperationException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new InvalidOperationException();
            }

            public override void SetLength(long value)
            {
                throw new InvalidOperationException();
            }

            public override long Position
            {
                get => throw new InvalidOperationException();
                set => throw new InvalidOperationException();
            }
        };
    }
}
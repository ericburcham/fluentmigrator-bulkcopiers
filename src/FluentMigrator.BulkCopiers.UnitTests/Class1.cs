using System;
using NUnit.Framework;

namespace FluentMigrator.BulkCopiers.UnitTests
{
    [TestFixture]
    public class DoNothingTests
    {
        [Test]
        public void PassingTest()
        {
            Assert.Pass();
        }
    }
}
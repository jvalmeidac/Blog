using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Tests.Project.Infrastructure.UnitOfWork
{
    [TestFixture]
    public class UnitOfWorkInsertTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestTest()
        {
            var test = 1;

            test.Should().Be(1);
        }
    }
}

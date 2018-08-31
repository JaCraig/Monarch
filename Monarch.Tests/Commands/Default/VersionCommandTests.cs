using FluentAssertions;
using Monarch.Commands.Default;
using Monarch.Defaults;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using System.Threading.Tasks;
using Xunit;

namespace Monarch.Tests.Commands.Default
{
    public class VersionCommandTests : TestBaseClass
    {
        [Fact]
        public void Creation()
        {
            var TestObject = new VersionCommand(new IConsoleWriter[] { }, new IOptions[] { });
            TestObject.Console.Should().BeOfType<DefaultConsoleWriter>();
        }

        [Fact]
        public async Task Run()
        {
            var TestObject = new VersionCommand(new IConsoleWriter[] { }, new IOptions[] { });
            var Result = await TestObject.Run(new EmptyInput()).ConfigureAwait(false);
            Result.Should().Be(0);
        }
    }
}
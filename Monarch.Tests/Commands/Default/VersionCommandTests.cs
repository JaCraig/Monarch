using FluentAssertions;
using Monarch.Commands.Default;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using Monarch.Tests.Utils;
using System.Threading.Tasks;
using Xunit;

namespace Monarch.Tests.Commands.Default
{
    public class VersionCommandTests : TestBaseClass<VersionCommand>
    {
        [Fact]
        public void Creation()
        {
            var TestObject = new VersionCommand(new IConsoleWriter[] { new EmptyConsoleWriter() }, System.Array.Empty<IOptions>());
            TestObject.Console.Should().BeOfType<EmptyConsoleWriter>();
        }

        [Fact]
        public async Task Run()
        {
            var TestObject = new VersionCommand(new IConsoleWriter[] { new EmptyConsoleWriter() }, System.Array.Empty<IOptions>());
            var Result = await TestObject.Run(new EmptyInput()).ConfigureAwait(false);
            Result.Should().Be(0);
        }
    }
}
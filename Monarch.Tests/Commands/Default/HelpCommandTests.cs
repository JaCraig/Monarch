using FluentAssertions;
using Monarch.Commands.Default;
using Monarch.Defaults;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using System.Threading.Tasks;
using Xunit;

namespace Monarch.Tests.Commands.Default
{
    public class HelpCommandTests : TestBaseClass
    {
        public static readonly TheoryData<HelpInput> CommandsData = new TheoryData<HelpInput>
        {
            { new HelpInput{Command="?" } },
            { new HelpInput{Command="v" } },
            { new HelpInput{Command="asdf" } },
            { new HelpInput{Command="usercommand" } }
        };

        [Fact]
        public void Creation()
        {
            var TestObject = new HelpCommand(new IConsoleWriter[] { }, new IOptions[] { });
            TestObject.Console.Should().BeOfType<DefaultConsoleWriter>();
            TestObject.Options.Should().BeOfType<DefaultOptions>();
        }

        [Theory]
        [MemberData(nameof(CommandsData))]
        public async Task Run(HelpInput data)
        {
            var TestObject = new HelpCommand(new IConsoleWriter[] { }, new IOptions[] { });
            var Result = await TestObject.Run(data).ConfigureAwait(false);
            Result.Should().Be(0);
        }
    }
}
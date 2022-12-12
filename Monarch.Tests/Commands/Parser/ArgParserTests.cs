using FluentAssertions;
using Mecha.xUnit;
using Microsoft.Extensions.DependencyInjection;
using Mirage.Generators.Default;
using Monarch.Commands.BaseClasses;
using Monarch.Commands.Default;
using Monarch.Commands.Interfaces;
using Monarch.Commands.Parser;
using Monarch.Defaults;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using Monarch.Tests.Utils;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace Monarch.Tests.Commands.Parser
{
    public class ArgParserTests : TestBaseClass<ArgParser>
    {
        public static readonly TheoryData<TestData, string> CommandsData = new TheoryData<TestData, string>
        {
            { new TestData{Data=new string[]{"?","?" } },"?" },
            { new TestData{Data=new string[]{"V","?" } },"v" },
            { new TestData{Data=new string[]{"VErsION","ASDF","?" } },"v" },
            { new TestData{Data=new string[]{"ASDF" } },"UserCommand" },
            { new TestData{Data=new string[]{"UserCommand" } },"UserCommand" },
        };

        [Theory]
        [MemberData(nameof(CommandsData))]
        public void CommandsParsed(TestData data, string expectedCommand)
        {
            var Item = new ServiceCollection().AddCanisterModules().BuildServiceProvider().GetService<IArgParser>();
            var Tokens = Item.GetTokens(data?.Data);
            Tokens.Should().NotBeNullOrEmpty();
            Tokens[0].Should().BeOfType<CommandToken>();
            Assert.StartsWith(expectedCommand, Tokens[0].Value, System.StringComparison.OrdinalIgnoreCase);
        }

        [Property]
        public void RandomArgs([Required] TestData data)
        {
            var Item = new ServiceCollection().AddCanisterModules().BuildServiceProvider().GetService<IArgParser>();
            var Tokens = Item.GetTokens(data?.Data);
            Tokens.Should().NotBeNullOrEmpty();
            Tokens[0].Should().BeOfType<CommandToken>();
            Tokens[0].Value.Should().BeEquivalentTo("UserCommand");
            Tokens.Should().HaveCountGreaterOrEqualTo(1).And.HaveCountLessOrEqualTo(11);
        }

        [Fact]
        public void SingleUserCommand()
        {
            var Item = new ArgParser(new DefaultOptions(),
                new ICommand[] {
                    new HelpCommand(new IConsoleWriter[]{new EmptyConsoleWriter() },System.Array.Empty<IOptions>()),
                    new UserCommand()});
            var Tokens = Item.GetTokens(new string[] { "UserCommand", "Test", "Data" });
            Tokens.Should().NotBeNullOrEmpty();
            Tokens[0].Should().BeOfType<CommandToken>();
            Tokens[0].Value.Should().BeEquivalentTo("UserCommand");
            Tokens[1].Should().BeOfType<OptionValueToken>();
            Tokens[1].Value.Should().BeEquivalentTo("Test");
            Tokens[2].Should().BeOfType<OptionValueToken>();
            Tokens[2].Value.Should().BeEquivalentTo("Data");
        }

        public class TestData
        {
            [ArrayGenerator(typeof(string), 1, 10)]
            public string[] Data { get; set; }
        }

        public class UserCommand : CommandBaseClass<EmptyInput>
        {
            public override string[] Aliases => new string[] { "UserCommand" };

            public override string Description => "Test User Command";

            public override string Name => "Name";

            protected override async Task<int> Run(EmptyInput input)
            {
                await Task.CompletedTask.ConfigureAwait(false);
                return 1;
            }
        }
    }
}
using FluentAssertions;
using Mirage.Generators.Default;
using Monarch.Commands.BaseClasses;
using Monarch.Commands.Default;
using Monarch.Commands.Interfaces;
using Monarch.Commands.Parser;
using Monarch.Defaults;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using Monarch.Tests.Utils;
using System.Threading.Tasks;
using TestFountain;
using Xunit;

namespace Monarch.Tests.Commands.Parser
{
    public class ArgParserTests : TestBaseClass
    {
        public static readonly TheoryData<TestData, string> CommandsData = new TheoryData<TestData, string>
        {
            { new TestData{Data=new string[]{"?","?" } },"?" },
            { new TestData{Data=new string[]{"V","?" } },"v" },
            { new TestData{Data=new string[]{"VErsION","ASDF","?" } },"v" },
            { new TestData{Data=new string[]{"ASDF" } },"?" },
            { new TestData{Data=new string[]{"UserCommand" } },"UserCommand" },
        };

        [Theory]
        [MemberData(nameof(CommandsData))]
        public void CommandsParsed(TestData data, string expectedCommand)
        {
            var Item = Canister.Builder.Bootstrapper.Resolve<ArgParser>();
            var Tokens = Item.GetTokens(data?.Data);
            Tokens.Should().NotBeNullOrEmpty();
            Tokens[0].Should().BeOfType<CommandToken>();
            Tokens[0].Value.Should().Equals(expectedCommand);
        }

        [Theory]
        [FountainData(1000, 500)]
        public void RandomArgs(TestData data)
        {
            var Item = Canister.Builder.Bootstrapper.Resolve<ArgParser>();
            var Tokens = Item.GetTokens(data?.Data);
            if (data?.Data == null)
            {
                Tokens.Should().BeEmpty();
            }
            else
            {
                Tokens.Should().NotBeNullOrEmpty();
                Tokens[0].Should().BeOfType<CommandToken>();
                Tokens[0].Value.Should().Equals("?");
                Tokens.Should().HaveCountGreaterOrEqualTo(1).And.HaveCountLessOrEqualTo(11);
            }
        }

        [Fact]
        public void SingleUserCommand()
        {
            var Item = new ArgParser(new DefaultOptions(),
                new ICommand[] {
                    new HelpCommand(new IConsoleWriter[]{new EmptyConsoleWriter() },new IOptions[]{ }),
                    new UserCommand()});
            var Tokens = Item.GetTokens(new string[] { "UserCommand", "Test", "Data" });
            Tokens.Should().NotBeNullOrEmpty();
            Tokens[0].Should().BeOfType<CommandToken>();
            Tokens[0].Value.Should().Equals("UserCommand");
            Tokens[1].Should().BeOfType<OptionValueToken>();
            Tokens[1].Value.Should().Equals("Test");
            Tokens[2].Should().BeOfType<OptionValueToken>();
            Tokens[2].Value.Should().Equals("Data");
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
                await Task.CompletedTask;
                return 1;
            }
        }
    }
}
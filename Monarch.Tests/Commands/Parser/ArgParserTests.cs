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
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace Monarch.Tests.Commands.Parser
{
    public class ArgParserTests : TestBaseClass<ArgParser>
    {
        public ArgParserTests()
        {
            TestObject = new ArgParser(new DefaultOptions(), new ICommand[] { new HelpCommand(new IConsoleWriter[] { new EmptyConsoleWriter() }, System.Array.Empty<IOptions>()), new UserCommand() });
        }

        public static readonly TheoryData<TestData, string> CommandsData = new()
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
            IArgParser? Item = new ServiceCollection().AddCanisterModules()?.BuildServiceProvider().GetService<IArgParser>();
            TokenBaseClass[] Tokens = Item?.GetTokens(data?.Data ?? Array.Empty<string>()) ?? Array.Empty<TokenBaseClass>();
            Assert.NotNull(Tokens);
            Assert.NotEmpty(Tokens);
            _ = Assert.IsType<CommandToken>(Tokens[0]);
            Assert.StartsWith(expectedCommand, Tokens[0].Value, System.StringComparison.OrdinalIgnoreCase);
        }

        [Property]
        public void RandomArgs([Required] TestData data)
        {
            IArgParser? Item = new ServiceCollection().AddCanisterModules()?.BuildServiceProvider().GetService<IArgParser>();
            TokenBaseClass[] Tokens = Item?.GetTokens(data?.Data ?? Array.Empty<string>()) ?? Array.Empty<TokenBaseClass>();
            Assert.NotNull(Tokens);
            Assert.NotEmpty(Tokens);
            _ = Assert.IsType<CommandToken>(Tokens[0]);
            Assert.Equal("UserCommand", Tokens[0].Value);
            Assert.True(Tokens.Length is >= 1 and <= 11);
        }

        [Fact]
        public void SingleUserCommand()
        {
            var Item = new ArgParser(new DefaultOptions(),
                new ICommand[] {
                    new HelpCommand(new IConsoleWriter[]{new EmptyConsoleWriter() },System.Array.Empty<IOptions>()),
                    new UserCommand()});
            TokenBaseClass[] Tokens = Item.GetTokens(new string[] { "UserCommand", "Test", "Data" });
            Assert.NotNull(Tokens);
            Assert.NotEmpty(Tokens);
            _ = Assert.IsType<CommandToken>(Tokens[0]);
            Assert.Equal("UserCommand", Tokens[0].Value);
            _ = Assert.IsType<OptionValueToken>(Tokens[1]);
            Assert.Equal("Test", Tokens[1].Value);
            _ = Assert.IsType<OptionValueToken>(Tokens[2]);
            Assert.Equal("Data", Tokens[2].Value);
        }

        public class TestData
        {
            [ArrayGenerator(typeof(string), 1, 10)]
            public string[]? Data { get; set; }
        }

        public class UserCommand : CommandBaseClass<EmptyInput>
        {
            public override string[] Aliases => new string[] { "UserCommand" };

            public override string Description => "Test User Command";

            public override string Name => "Name";

            protected override async Task<int> Run(EmptyInput? input)
            {
                await Task.CompletedTask.ConfigureAwait(false);
                return 1;
            }
        }
    }
}
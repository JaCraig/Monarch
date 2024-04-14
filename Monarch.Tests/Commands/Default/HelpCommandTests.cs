using FluentAssertions;
using Monarch.Commands.Default;
using Monarch.Defaults;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using Monarch.Tests.Utils;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Monarch.Tests.Commands.Default
{
    public class HelpCommandTests : TestBaseClass<HelpCommand>
    {
        public HelpCommandTests()
        {
            _TestClass = new HelpCommand(_Console, _Options);
            TestObject = new HelpCommand(_Console, _Options);
        }

        public static readonly TheoryData<HelpInput> CommandsData = new()
        {
            { new HelpInput{Command="?" } },
            { new HelpInput{Command="v" } },
            { new HelpInput{Command="asdf" } },
            { new HelpInput{Command="usercommand" } }
        };

        private readonly IConsoleWriter[] _Console = new[] { new EmptyConsoleWriter() };
        private readonly IOptions[] _Options = Array.Empty<IOptions>();
        private readonly HelpCommand _TestClass;

        [Fact]
        public async Task CanCallRun()
        {
            // Arrange
            var Input = new HelpInput { Command = "TestValue1353782729" };

            // Act
            var Result = await _TestClass.Run(Input);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public async Task CanCallRunWithNullInput() => await _TestClass.Run(default(HelpInput));

        [Fact]
        public void CanConstruct()
        {
            // Arrange
            var Console = new IConsoleWriter[] { new EmptyConsoleWriter() };
            IOptions[] Options = Array.Empty<IOptions>();

            // Act
            var Instance = new HelpCommand(Console, Options);

            // Assert
            _ = Instance.Should().NotBeNull();
        }

        [Fact]
        public void CanConstructWithNullConsole() => _ = new HelpCommand(default, _Options);

        [Fact]
        public void CanConstructWithNullOptions() => _ = new HelpCommand(_Console, default);

        [Fact]
        public void CanGetAliases()
        {
            // Assert
            _ = _TestClass.Aliases.Should().BeAssignableTo<string[]>();

            _ = _TestClass.Aliases.Should().BeEquivalentTo(new[] { "?", "help", "h" });
        }

        [Fact]
        public void CanGetDescription()
        {
            // Assert
            _ = _TestClass.Description.Should().BeAssignableTo<string>();

            _ = _TestClass.Description.Should().BeEquivalentTo("Show command line help");
        }

        [Fact]
        public void CanGetName()
        {
            // Assert
            _ = _TestClass.Name.Should().BeAssignableTo<string>();

            _ = _TestClass.Name.Should().BeEquivalentTo("Help");
        }

        [Fact]
        public void ConsoleIsInitializedCorrectly() => _TestClass.Console.Should().BeSameAs(_Console[0]);

        [Fact]
        public void Creation()
        {
            var TestObject = new HelpCommand(new IConsoleWriter[] { new EmptyConsoleWriter() }, Array.Empty<IOptions>());
            _ = TestObject.Console.Should().BeOfType<EmptyConsoleWriter>();
            _ = TestObject.Options.Should().BeOfType<DefaultOptions>();
        }

        [Fact]
        public void OptionsIsInitializedCorrectly() => _TestClass.Options.Should().BeOfType(typeof(DefaultOptions));

        [Theory]
        [MemberData(nameof(CommandsData))]
        public async Task Run(HelpInput data)
        {
            var TestObject = new HelpCommand(new IConsoleWriter[] { new EmptyConsoleWriter() }, Array.Empty<IOptions>());
            var Result = await TestObject.Run(data);
            _ = Result.Should().Be(0);
        }
    }
}
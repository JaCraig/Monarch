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
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullConsole() => _ = new HelpCommand(default, _Options);

        [Fact]
        public void CanConstructWithNullOptions() => _ = new HelpCommand(_Console, default);

        [Fact]
        public void CanGetAliases()
        {
            // Assert
            _ = Assert.IsType<string[]>(_TestClass.Aliases);
            Assert.Equal(new[] { "?", "help", "h" }, _TestClass.Aliases);
        }

        [Fact]
        public void CanGetDescription()
        {
            // Assert
            _ = Assert.IsType<string>(_TestClass.Description);
            Assert.Equal("Show command line help", _TestClass.Description);
        }

        [Fact]
        public void CanGetName()
        {
            // Assert
            _ = Assert.IsType<string>(_TestClass.Name);
            Assert.Equal("Help", _TestClass.Name);
        }

        [Fact]
        public void ConsoleIsInitializedCorrectly() => Assert.Same(_Console[0], _TestClass.Console);

        [Fact]
        public void Creation()
        {
            // Arrange
            var TestObject = new HelpCommand(new IConsoleWriter[] { new EmptyConsoleWriter() }, Array.Empty<IOptions>());

            // Assert
            _ = Assert.IsType<EmptyConsoleWriter>(TestObject.Console);
            _ = Assert.IsType<DefaultOptions>(TestObject.Options);
        }

        [Fact]
        public void OptionsIsInitializedCorrectly() => Assert.IsType<DefaultOptions>(_TestClass.Options);

        [Theory]
        [MemberData(nameof(CommandsData))]
        public async Task Run(HelpInput data)
        {
            var TestObject = new HelpCommand(new IConsoleWriter[] { new EmptyConsoleWriter() }, Array.Empty<IOptions>());
            var Result = await TestObject.Run(data);
            Assert.Equal(0, Result);
        }
    }
}
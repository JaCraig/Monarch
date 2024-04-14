using FluentAssertions;
using Monarch.Commands;
using Monarch.Commands.Interfaces;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Monarch.Tests
{
    public class CommandRunnerTests : TestBaseClass<CommandRunner>
    {
        public CommandRunnerTests()
        {
            _Manager = new CommandManager(new[] { Substitute.For<ICommand>(), Substitute.For<ICommand>(), Substitute.For<ICommand>() }, new[] { Substitute.For<IOptions>(), Substitute.For<IOptions>(), Substitute.For<IOptions>() }, new[] { Substitute.For<IArgLexer>(), Substitute.For<IArgLexer>(), Substitute.For<IArgLexer>() }, new[] { Substitute.For<IArgParser>(), Substitute.For<IArgParser>(), Substitute.For<IArgParser>() });
            _TestClass = new CommandRunner(_Manager);
            TestObject = new CommandRunner(_Manager);
        }

        private readonly CommandManager _Manager;
        private readonly CommandRunner _TestClass;

        [Fact]
        public async Task CanCallRun()
        {
            // Arrange
            var Args = new[] { "TestValue1660602230", "TestValue1655192855", "TestValue322170422" };

            // Act
            var Result = await _TestClass.Run(Args);

            // Assert
            _ = Result.Should().Be(0);
        }

        [Fact]
        public async Task CanCallRunWithNullArgs() => await _TestClass.Run(default);

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new CommandRunner();

            // Assert
            _ = Instance.Should().NotBeNull();

            // Act
            Instance = new CommandRunner(_Manager);

            // Assert
            _ = Instance.Should().NotBeNull();
        }

        [Fact]
        public void ManagerIsInitializedCorrectly() => _TestClass.Manager.Should().BeSameAs(_Manager);
    }
}
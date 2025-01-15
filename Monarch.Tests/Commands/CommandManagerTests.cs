using Monarch.Commands;
using Monarch.Commands.Interfaces;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Monarch.Tests.Commands
{
    public class CommandManagerTests : TestBaseClass<CommandManager>
    {
        public CommandManagerTests()
        {
            _Commands = new[] { Substitute.For<ICommand>(), Substitute.For<ICommand>(), Substitute.For<ICommand>() };
            _Options = new[] { Substitute.For<IOptions>(), Substitute.For<IOptions>(), Substitute.For<IOptions>() };
            _Lexer = new[] { Substitute.For<IArgLexer>(), Substitute.For<IArgLexer>(), Substitute.For<IArgLexer>() };
            _Parser = new[] { Substitute.For<IArgParser>(), Substitute.For<IArgParser>(), Substitute.For<IArgParser>() };
            _TestClass = new CommandManager(_Commands, _Options, _Lexer, _Parser);
            TestObject = new CommandManager(_Commands, _Options, _Lexer, _Parser);
        }

        private readonly IEnumerable<ICommand> _Commands;
        private readonly IEnumerable<IArgLexer> _Lexer;
        private readonly IEnumerable<IOptions> _Options;
        private readonly IEnumerable<IArgParser> _Parser;
        private readonly CommandManager _TestClass;

        [Fact]
        public void CanCallGetCommand()
        {
            // Arrange
            var Args = new[] { "?" };

            // Act
            Assert.NotNull(_TestClass.GetCommand(Array.Empty<string>()));
            Assert.NotNull(_TestClass.GetCommand(Args));
        }

        [Fact]
        public void CanCallGetInput()
        {
            // Arrange
            ICommand Command = Substitute.For<ICommand>();
            var Args = new[] { "TestValue350805657", "TestValue191198581", "TestValue156854383" };

            // Act
            var Result = _TestClass.GetInput(Command, Args);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void CanCallGetInputWithNullCommand() => _TestClass.GetInput(default, new[] { "TestValue1316758140", "TestValue1839205710", "TestValue879839909" });

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new CommandManager(_Commands, _Options, _Lexer, _Parser);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullCommands() => _ = new CommandManager(default, _Options, _Lexer, _Parser);

        [Fact]
        public void CanConstructWithNullLexer() => _ = new CommandManager(_Commands, _Options, default, _Parser);

        [Fact]
        public void CanConstructWithNullOptions() => _ = new CommandManager(_Commands, default, _Lexer, _Parser);

        [Fact]
        public void CanConstructWithNullParser() => _ = new CommandManager(_Commands, _Options, _Lexer, default);

        [Fact]
        public void CommandsIsInitializedCorrectly() => Assert.Same(_Commands, _TestClass.Commands);

        [Fact]
        public void LexerIsInitializedCorrectly() => Assert.Same(_Lexer.First(), _TestClass.Lexer);

        [Fact]
        public void OptionsIsInitializedCorrectly() => Assert.Same(_Options.First(), _TestClass.Options);

        [Fact]
        public void ParserIsInitializedCorrectly() => Assert.Same(_Parser.First(), _TestClass.Parser);
    }
}
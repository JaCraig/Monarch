using Monarch.Commands.Lexer;
using Monarch.Commands.Parser;
using Monarch.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Monarch.Tests.Commands.Lexer
{
    public class ArgLexerTests : TestBaseClass<ArgLexer>
    {
        public ArgLexerTests()
        {
            TestObject = new ArgLexer();
        }

        private readonly ArgLexer _TestClass = new();

        [Fact]
        public void CanCallLex()
        {
            // Arrange
            var Tokens = new List<TokenBaseClass>();
            PropertyInfo[] Properties = GetTestType().GetProperties();

            // Act
            Command Result = _TestClass.Lex(Tokens, Properties);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallLexWithNullProperties() => _TestClass.Lex([], default);

        [Fact]
        public void CanCallLexWithNullTokens() => _TestClass.Lex(default, GetTestType().GetProperties());

        [Fact]
        public void LexPerformsMapping()
        {
            // Arrange
            var Tokens = new List<TokenBaseClass>
            {
                new CommandToken("TestClass"),
                new OptionNameToken("TestValue1"),
                new OptionValueToken("TestValue1"),
                new OptionNameToken("TestValue2"),
                new OptionValueToken("TestValue2")
            };
            PropertyInfo[] Properties = GetTestType().GetProperties();

            // Act
            Command Result = _TestClass.Lex(Tokens, Properties);

            // Assert
            Assert.Equal("TestClass", Result.Name?.Value);
            Assert.Equal("TestValue1", Result.Properties[0].PropertyInfo?.Name);
            Assert.Equal("TestValue2", Result.Properties[1].PropertyInfo?.Name);
            Assert.Equal("TestValue1", Result.Properties[0].FlagValue[0].Value);
            Assert.Equal("TestValue2", Result.Properties[1].FlagValue[0].Value);
        }

        private static Type GetTestType() => typeof(TestClass);

        private class TestClass
        {
            public string? TestValue1 { get; set; }
            public string? TestValue2 { get; set; }
        }
    }
}
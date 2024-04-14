using FluentAssertions;
using Monarch.Commands.Lexer;
using Monarch.Commands.Parser;
using Monarch.Tests.BaseClasses;
using System.Collections.Generic;
using Xunit;

namespace Monarch.Tests.Commands.Lexer
{
    public class CommandTests : TestBaseClass<Command>
    {
        public CommandTests()
        {
            _TestClass = new Command();
            TestObject = new Command();
        }

        private readonly Command _TestClass;

        [Fact]
        public void CanCallGetValue()
        {
            // Arrange
            var InputObject = new object();

            // Act
            var Result = _TestClass.GetValue(InputObject);

            // Assert
            Assert.Equal(Result, InputObject);
        }

        [Fact]
        public void CanCallGetValueWithNullInputObject() => _TestClass.GetValue(default);

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            Assert.Equal(" ", Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new Command();

            // Assert
            _ = Instance.Should().NotBeNull();
        }

        [Fact]
        public void CanSetAndGetName()
        {
            // Arrange
            var TestValue = new CommandToken("TestValue940248421");

            // Act
            _TestClass.Name = TestValue;

            // Assert
            _ = _TestClass.Name.Should().BeSameAs(TestValue);
        }

        [Fact]
        public void CanSetAndGetProperties()
        {
            // Arrange
            var TestValue = new List<Property>();

            // Act
            _TestClass.Properties = TestValue;

            // Assert
            _ = _TestClass.Properties.Should().BeSameAs(TestValue);
        }
    }
}
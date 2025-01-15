using Monarch.Defaults;
using Monarch.Interfaces;
using Monarch.Tests.BaseClasses;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Monarch.Tests.Defaults
{
    public class DefaultConsoleWriterTests : TestBaseClass<DefaultConsoleWriter>
    {
        public DefaultConsoleWriterTests()
        {
            _Options = new[] { Substitute.For<IOptions>(), Substitute.For<IOptions>(), Substitute.For<IOptions>() };
            _TestClass = new DefaultConsoleWriter(_Options);
            TestObject = new DefaultConsoleWriter(_Options);
        }

        private readonly IEnumerable<IOptions> _Options;
        private readonly DefaultConsoleWriter _TestClass;

        [Fact]
        public void CanCallIndent()
        {
            // Act
            IConsoleWriter Result = _TestClass.Indent();

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallOutdent()
        {
            // Act
            IConsoleWriter Result = _TestClass.Outdent();

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallResetConsoleColor()
        {
            // Act
            IConsoleWriter Result = _TestClass.ResetConsoleColor();

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallSetConsoleColor()
        {
            // Arrange
            const ConsoleColor Color = ConsoleColor.DarkYellow;

            // Act
            IConsoleWriter Result = _TestClass.SetConsoleColor(Color);

            // Assert
            Assert.Same(Result, _TestClass);
        }

        [Fact]
        public void CanCallWriteLineWithArrayOfChar()
        {
            // Arrange
            var Value = new[] { 'h', 'Q', 'D' };

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithArrayOfCharWithNullValue() => _TestClass.WriteLine(default(char[]));

        [Fact]
        public void CanCallWriteLineWithBool()
        {
            // Arrange
            const bool Value = false;

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithChar()
        {
            // Arrange
            const char Value = 'U';

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithDecimal()
        {
            // Arrange
            const decimal Value = 1237548370.41M;

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithDouble()
        {
            // Arrange
            const double Value = 509036370.48;

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithFloat()
        {
            // Arrange
            const float Value = 23992.7637F;

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithInt()
        {
            // Arrange
            const int Value = 1951810483;

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithLong()
        {
            // Arrange
            const long Value = 217568591L;

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithNoParameters()
        {
            // Act
            IConsoleWriter Result = _TestClass.WriteLine();

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithObject()
        {
            // Arrange
            var Value = new object();

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithObjectWithNullValue() => _TestClass.WriteLine(default(object));

        [Fact]
        public void CanCallWriteLineWithString()
        {
            // Arrange
            const string Value = "TestValue468029781";

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallWriteLineWithStringWithInvalidValue(string? value) => _TestClass.WriteLine(value);

        [Fact]
        public void CanCallWriteLineWithUint()
        {
            // Arrange
            const uint Value = 1441847793;

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteLineWithUlong()
        {
            // Arrange
            const ulong Value = 1849543636;

            // Act
            IConsoleWriter Result = _TestClass.WriteLine(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteSeparator()
        {
            // Act
            IConsoleWriter Result = _TestClass.WriteSeparator();

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithArrayOfChar()
        {
            // Arrange
            var Value = new[] { 'k', 'j', 'b' };

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithArrayOfCharWithNullValue() => _TestClass.Write(default(char[]));

        [Fact]
        public void CanCallWriteWithBool()
        {
            // Arrange
            const bool Value = true;

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithChar()
        {
            // Arrange
            const char Value = 'F';

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithDecimal()
        {
            // Arrange
            const decimal Value = 63288866.52M;

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithDouble()
        {
            // Arrange
            const double Value = 1193375756.43;

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithFloat()
        {
            // Arrange
            const float Value = 6422.90332F;

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithInt()
        {
            // Arrange
            const int Value = 1863962906;

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithLong()
        {
            // Arrange
            const long Value = 1861053179L;

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithObject()
        {
            // Arrange
            var Value = new object();

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithObjectWithNullValue() => _TestClass.Write(default(object));

        [Fact]
        public void CanCallWriteWithString()
        {
            // Arrange
            const string Value = "TestValue803815430";

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanCallWriteWithStringWithInvalidValue(string? value) => _TestClass.Write(value);

        [Fact]
        public void CanCallWriteWithUint()
        {
            // Arrange
            const uint Value = 6978592;

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanCallWriteWithUlong()
        {
            // Arrange
            const ulong Value = 135743457;

            // Act
            IConsoleWriter Result = _TestClass.Write(Value);

            // Assert
            Assert.Same(_TestClass, Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new DefaultConsoleWriter(_Options);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullOptions() => new DefaultConsoleWriter(default);

        [Fact]
        public void CanGetConsoleWidth() =>
            // Assert
            Assert.IsType<int>(_TestClass.ConsoleWidth);

        [Fact]
        public void CanGetCurrentIndent() =>
            // Assert
            Assert.IsType<int>(_TestClass.CurrentIndent);

        [Fact]
        public void CanGetSeparator() =>
            // Assert
            Assert.IsType<string>(_TestClass.Separator);
    }
}
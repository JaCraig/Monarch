using FluentAssertions;
using Monarch.Exceptions;
using NSubstitute;
using System;
using System.Runtime.Serialization;
using Xunit;

namespace Monarch.Tests.Exceptions
{
    public class ParsingExceptionTests
    {
        public ParsingExceptionTests()
        {
            _message = "TestValue917027529";
            _innerException = new Exception();
            _info = new SerializationInfo(typeof(string), Substitute.For<IFormatterConverter>());
            _context = new StreamingContext();
            _testClass = new ParsingException(_message, _innerException);
        }

        private readonly StreamingContext _context;
        private readonly SerializationInfo _info;
        private readonly Exception _innerException;
        private readonly string _message;
        private readonly ParsingException _testClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ParsingException();

            // Assert
            _ = instance.Should().NotBeNull();

            // Act
            instance = new ParsingException(_message);

            // Assert
            _ = instance.Should().NotBeNull();

            // Act
            instance = new ParsingException(_message, _innerException);

            // Assert
            _ = instance.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidMessage(string? value)
        {
            _ = new ParsingException(value);
            _ = new ParsingException(value, _innerException);
        }

        [Fact]
        public void CanConstructWithNullInfo() => new ParsingException(default, _innerException);

        [Fact]
        public void CanConstructWithNullInnerException() => new ParsingException(_message, default);
    }
}
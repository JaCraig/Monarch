using FluentAssertions;
using Monarch.Commands.Lexer;
using Monarch.Commands.Parser;
using Monarch.Tests.BaseClasses;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Monarch.Tests.Commands.Lexer
{
    public enum TestEnum
    {
        Value1,
        Value2,
        Value3
    }

    public class PropertyTests : TestBaseClass<Property>
    {
        public PropertyTests()
        {
            TestObject = new Property();
        }

        private readonly Property _TestClass = new();

        [Fact]
        public void CanCallGetValue()
        {
            // Arrange
            var InputObject = new object();

            // Act
            _TestClass.GetValue(InputObject);
        }

        [Fact]
        public void CanCallGetValueWithNullInputObject() => _TestClass.GetValue(default);

        [Fact]
        public void CanCallToString()
        {
            // Act
            var Result = _TestClass.ToString();

            // Assert
            _ = Result.Should().Be(" ");
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new Property();

            // Assert
            _ = Instance.Should().NotBeNull();
        }

        [Fact]
        public void CanGetIsIEnumerable() =>
            // Assert
            _ = _TestClass.IsIEnumerable.As<object>().Should().BeAssignableTo<bool>();

        [Fact]
        public void CanGetMaxValueCount() =>
            // Assert
            _ = _TestClass.MaxValueCount.As<object>().Should().BeAssignableTo<int>();

        [Fact]
        public void CanSetAndGetFlagName()
        {
            // Arrange
            var TestValue = new CommandToken("TestValue1844508801");

            // Act
            _TestClass.FlagName = TestValue;

            // Assert
            _ = _TestClass.FlagName.Should().BeSameAs(TestValue);
        }

        [Fact]
        public void CanSetAndGetFlagValue()
        {
            // Arrange
            var TestValue = new List<TokenBaseClass>();

            // Act
            _TestClass.FlagValue = TestValue;

            // Assert
            _ = _TestClass.FlagValue.Should().BeSameAs(TestValue);
        }

        [Fact]
        public void CanSetAndGetPropertyInfo()
        {
            // Arrange
            PropertyInfo TestValue = typeof(TestClass).Properties().First();

            // Act
            _TestClass.PropertyInfo = TestValue;

            // Assert
            _ = _TestClass.PropertyInfo.Should().BeSameAs(TestValue);
        }

        [Fact]
        public void Test()
        {
            var TestObject = new Property
            {
                FlagName = new OptionNameToken("Test"),
                FlagValue = new OptionValueToken[] { new("Value2") }.ToList<TokenBaseClass>(),
                PropertyInfo = typeof(TestClass).GetProperty("EnumValue")
            };

            var TestItem = new TestClass();
            TestObject.GetValue(TestItem);

            Assert.Equal(TestEnum.Value2, TestItem.EnumValue);
        }
    }

    public class TestClass
    {
        public TestEnum EnumValue { get; set; }
    }
}
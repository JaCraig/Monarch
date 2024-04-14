using FluentAssertions;
using Monarch.Commands.Attributes;
using Monarch.Tests.BaseClasses;
using System;
using Xunit;

namespace Monarch.Tests.Commands.Attributes
{
    public class DynamicDisplayAttributeTests : TestBaseClass<DynamicDisplayAttribute>
    {
        public DynamicDisplayAttributeTests()
        {
            _DescriptionType = typeof(string);
            _TestClass = new DynamicDisplayAttribute(_DescriptionType);
            TestObject = new DynamicDisplayAttribute(_DescriptionType);
        }

        private readonly Type _DescriptionType;
        private readonly DynamicDisplayAttribute _TestClass;

        [Fact]
        public void CanCallGetDescription()
        {
            // Act
            var Result = _TestClass.GetDescription();

            // Assert
            Assert.Equal("", Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new DynamicDisplayAttribute(_DescriptionType);

            // Assert
            _ = Instance.Should().NotBeNull();
        }

        [Fact]
        public void CanConstructWithNullDescriptionType() => new DynamicDisplayAttribute(default);

        [Fact]
        public void DescriptionTypeIsInitializedCorrectly() => _TestClass.DescriptionType.Should().BeSameAs(_DescriptionType);
    }
}
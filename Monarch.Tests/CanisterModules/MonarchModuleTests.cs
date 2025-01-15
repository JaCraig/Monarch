using Microsoft.Extensions.DependencyInjection;
using Monarch.CanisterModules;
using Monarch.Tests.BaseClasses;
using NSubstitute;
using Xunit;

namespace Monarch.Tests.CanisterModules
{
    public class MonarchModuleTests : TestBaseClass<MonarchModule>
    {
        public MonarchModuleTests()
        {
            _TestClass = new MonarchModule();
            TestObject = new MonarchModule();
        }

        private readonly MonarchModule _TestClass;

        [Fact]
        public void CanCallLoad()
        {
            // Arrange
            IServiceCollection Bootstrapper = Substitute.For<IServiceCollection>();

            // Act
            _TestClass.Load(Bootstrapper);
        }

        [Fact]
        public void CanGetOrder() =>
            // Assert
            Assert.IsType<int>(_TestClass.Order);
    }
}
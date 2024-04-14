using Canister.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Monarch.Tests.BaseClasses;
using NSubstitute;
using System;
using Xunit;

namespace Monarch.Tests.ExtensionMethods
{
    public class MonarchCanisterExtensionsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(MonarchCanisterExtensions);

        [Fact]
        public void CanCallRegisterMonarch()
        {
            // Arrange
            ICanisterConfiguration Bootstrapper = Substitute.For<ICanisterConfiguration>();

            // Act
            _ = Bootstrapper.RegisterMonarch();
        }
    }
}
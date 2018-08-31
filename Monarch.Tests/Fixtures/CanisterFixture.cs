using System;
using System.Reflection;
using TestFountain.Registration;
using Xunit;

namespace Monarch.Tests.Fixtures
{
    [CollectionDefinition("Canister collection")]
    public class CanisterCollection : ICollectionFixture<CanisterFixture>
    {
    }

    public class CanisterFixture : IDisposable
    {
        public CanisterFixture()
        {
            if (Canister.Builder.Bootstrapper == null)
            {
                Canister.Builder.CreateContainer(null, typeof(CanisterFixture).GetTypeInfo().Assembly)
                   .RegisterTestFountain()
                   .RegisterMonarch()
                   .Build();
            }
        }

        public void Dispose()
        {
        }
    }
}
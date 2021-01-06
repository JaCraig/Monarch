using Microsoft.Extensions.DependencyInjection;
using System;
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
                new ServiceCollection().AddCanisterModules(x => x.AddAssembly(typeof(CanisterFixture).Assembly)
                   .RegisterTestFountain()
                   .RegisterMonarch());
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool managed)
        {
        }
    }
}
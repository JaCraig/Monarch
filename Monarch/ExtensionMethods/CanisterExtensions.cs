using Canister.Interfaces;

namespace Monarch
{
    /// <summary>
    /// Canister extensions
    /// </summary>
    public static class CanisterExtensions
    {
        /// <summary>
        /// Registers monarch with Canister.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <returns>The bootstrapper.</returns>
        public static IBootstrapper RegisterMonarch(this IBootstrapper bootstrapper)
        {
            return bootstrapper.AddAssembly(typeof(CanisterExtensions).Assembly);
        }
    }
}
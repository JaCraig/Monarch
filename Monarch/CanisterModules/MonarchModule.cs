using Canister.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Monarch.Commands;
using Monarch.Commands.Interfaces;
using Monarch.Commands.Lexer;
using Monarch.Commands.Parser;
using Monarch.Interfaces;

namespace Monarch.CanisterModules
{
    /// <summary>
    /// Monarch module
    /// </summary>
    /// <seealso cref="IModule"/>
    public class MonarchModule : IModule
    {
        /// <summary>
        /// Order to run this in
        /// </summary>
        public int Order => 1;

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public void Load(IBootstrapper bootstrapper)
        {
            if (bootstrapper == null)
                return;
            bootstrapper.RegisterAll<ICommand>(ServiceLifetime.Singleton);
            bootstrapper.RegisterAll<IOptions>(ServiceLifetime.Singleton);
            bootstrapper.RegisterAll<IConsoleWriter>(ServiceLifetime.Singleton);
            bootstrapper.Register<ArgParser>();
            bootstrapper.Register<ArgLexer>();
            bootstrapper.Register<CommandManager>(ServiceLifetime.Singleton);
            bootstrapper.Register<CommandRunner>(ServiceLifetime.Singleton);
        }
    }
}
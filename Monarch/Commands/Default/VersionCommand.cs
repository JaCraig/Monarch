using Monarch.Commands.BaseClasses;
using Monarch.Defaults;
using Monarch.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Monarch.Commands.Default
{
    /// <summary>
    /// Version command
    /// </summary>
    /// <seealso cref="CommandBaseClass{EmptyInput}"/>
    public class VersionCommand : CommandBaseClass<EmptyInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionCommand"/> class.
        /// </summary>
        /// <param name="console">The console.</param>
        /// <param name="options">The options.</param>
        public VersionCommand(IEnumerable<IConsoleWriter> console, IEnumerable<IOptions> options)
        {
            Console = console.FirstOrDefault(x => !(x is DefaultConsoleWriter)) ?? new DefaultConsoleWriter(options);
        }

        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        public override string[] Aliases => new string[] { "v", "version" };

        /// <summary>
        /// Gets the console.
        /// </summary>
        /// <value>The console.</value>
        public IConsoleWriter Console { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description => "Show version information";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Version";

        /// <summary>
        /// Runs the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        protected override async Task<int> Run(EmptyInput input)
        {
            await Task.CompletedTask;
            var AppAssembly = Assembly.GetEntryAssembly();
            var CustomAttributes = AppAssembly.GetCustomAttributes();
            var AppName = AppAssembly.GetName();

            Console.WriteLine($"{CustomAttributes.OfType<AssemblyProductAttribute>().FirstOrDefault()?.Product} ({AppName.Version})");
            return 0;
        }
    }
}
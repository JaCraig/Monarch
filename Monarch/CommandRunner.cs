using Monarch.Commands;
using System.Threading.Tasks;

namespace Monarch
{
    /// <summary>
    /// Command runner
    /// </summary>
    public class CommandRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRunner"/> class.
        /// </summary>
        public CommandRunner()
            : this(Canister.Builder.Bootstrapper.Resolve<CommandManager>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRunner"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CommandRunner(CommandManager manager)
        {
            Manager = manager;
        }

        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <value>The manager.</value>
        public CommandManager Manager { get; }

        /// <summary>
        /// Runs the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The result.</returns>
        public Task<int> Run(string[] args)
        {
            var Command = Manager.GetCommand(args);
            var Input = Manager.GetInput(Command, args);
            return Command.Run(Input);
        }
    }
}
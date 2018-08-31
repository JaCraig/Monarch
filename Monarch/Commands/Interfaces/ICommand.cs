using System.Threading.Tasks;

namespace Monarch.Commands.Interfaces
{
    /// <summary>
    /// Command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        string[] Aliases { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }

        /// <summary>
        /// Gets the name
        /// </summary>
        /// <value>The name</value>
        string Name { get; }

        /// <summary>
        /// Determines whether this instance can run the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>
        /// <c>true</c> if this instance can run the specified argument; otherwise, <c>false</c>.
        /// </returns>
        bool CanRun(string arg);

        /// <summary>
        /// Creates a command input.
        /// </summary>
        /// <returns>The command input.</returns>
        object CreateInput();

        /// <summary>
        /// Runs the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result</returns>
        Task<int> Run(object input);
    }

    /// <summary>
    /// Command interface
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    public interface ICommand<TInput> : ICommand
    {
    }
}
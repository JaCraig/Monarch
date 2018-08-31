using Monarch.Commands.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Monarch.Commands.BaseClasses
{
    /// <summary>
    /// Command base class
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    /// <seealso cref="ICommand{TInput}"/>
    public abstract class CommandBaseClass<TInput> : ICommand<TInput>
        where TInput : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBaseClass"/> class.
        /// </summary>
        protected CommandBaseClass()
        {
        }

        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        public abstract string[] Aliases { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public abstract string Description { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Determines whether this instance can run the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>
        /// <c>true</c> if this instance can run the specified argument; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRun(string arg)
        {
            arg = arg.ToUpper();
            return Aliases.Select(x => x.ToUpper()).Any(x => x == arg);
        }

        /// <summary>
        /// Creates the input.
        /// </summary>
        /// <returns></returns>
        public object CreateInput()
        {
            return new TInput();
        }

        /// <summary>
        /// Runs the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        public Task<int> Run(object input)
        {
            return Run(input as TInput);
        }

        /// <summary>
        /// Runs the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        protected abstract Task<int> Run(TInput input);
    }
}
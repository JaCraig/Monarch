using Monarch.Interfaces;

namespace Monarch.Defaults
{
    /// <summary>
    /// Default options
    /// </summary>
    /// <seealso cref="IOptions"/>
    public class DefaultOptions : IOptions
    {
        /// <summary>
        /// Gets the command prefix.
        /// </summary>
        /// <value>The command prefix.</value>
        public string CommandPrefix { get; } = "";

        /// <summary>
        /// Gets the flag prefix.
        /// </summary>
        /// <value>The flag prefix.</value>
        public string FlagPrefix { get; } = "-";

        /// <summary>
        /// Gets the indent amount.
        /// </summary>
        /// <value>The indent amount.</value>
        public int IndentAmount { get; } = 4;
    }
}
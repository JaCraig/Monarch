namespace Monarch.Interfaces
{
    /// <summary>
    /// Options interface
    /// </summary>
    public interface IOptions
    {
        /// <summary>
        /// Gets the command prefix.
        /// </summary>
        /// <value>The command prefix.</value>
        string CommandPrefix { get; }

        /// <summary>
        /// Gets the flag prefix.
        /// </summary>
        /// <value>The flag prefix.</value>
        string FlagPrefix { get; }

        /// <summary>
        /// Gets the indent amount.
        /// </summary>
        /// <value>The indent amount.</value>
        int IndentAmount { get; }
    }
}
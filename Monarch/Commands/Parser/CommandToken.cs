namespace Monarch.Commands.Parser
{
    /// <summary>
    /// Command token
    /// </summary>
    /// <seealso cref="TokenBaseClass"/>
    public class CommandToken : TokenBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandToken"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public CommandToken(string token)
            : base(token)
        {
        }
    }
}
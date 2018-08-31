namespace Monarch.Commands.Parser
{
    /// <summary>
    /// Option value token
    /// </summary>
    /// <seealso cref="TokenBaseClass"/>
    public class OptionValueToken : TokenBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionValueToken"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public OptionValueToken(string value)
            : base(value)
        {
        }
    }
}
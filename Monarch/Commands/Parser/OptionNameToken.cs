namespace Monarch.Commands.Parser
{
    /// <summary>
    /// Option name token
    /// </summary>
    /// <seealso cref="TokenBaseClass"/>
    public class OptionNameToken : TokenBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionNameToken"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public OptionNameToken(string value)
            : base(value)
        {
        }
    }
}
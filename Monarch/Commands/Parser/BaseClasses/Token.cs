namespace Monarch.Commands.Parser
{
    /// <summary>
    /// Token base class
    /// </summary>
    public abstract class TokenBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenBaseClass"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">value</exception>
        protected TokenBaseClass(string value)
        {
            Value = value ?? throw new System.ArgumentNullException(nameof(value));
            UpperValue = Value.ToUpper();
        }

        /// <summary>
        /// Gets the upper value.
        /// </summary>
        /// <value>The upper value.</value>
        public string UpperValue { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
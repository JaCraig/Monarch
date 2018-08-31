using BigBook;
using Monarch.Commands.Parser;
using System.Collections.Generic;

namespace Monarch.Commands.Lexer
{
    /// <summary>
    /// Command
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command()
        {
            Properties = new List<Property>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public TokenBaseClass Name { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public List<Property> Properties { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="inputObject">The input object.</param>
        /// <returns>The resulting value.</returns>
        public object GetValue(object inputObject)
        {
            for (int i = 0; i < Properties.Count; i++)
            {
                Properties[i].GetValue(inputObject);
            }
            return inputObject;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return Name + " " + Properties.ToString(x => x.ToString());
        }
    }
}
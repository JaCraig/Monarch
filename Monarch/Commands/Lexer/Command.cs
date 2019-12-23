/*
Copyright 2018 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

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
        public TokenBaseClass? Name { get; set; }

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
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return Name + " " + Properties.ToString(x => x.ToString());
        }
    }
}
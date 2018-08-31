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
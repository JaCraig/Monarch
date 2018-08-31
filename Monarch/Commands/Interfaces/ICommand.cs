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

using System.Threading.Tasks;

namespace Monarch.Commands.Interfaces
{
    /// <summary>
    /// Command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        string[] Aliases { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }

        /// <summary>
        /// Gets the name
        /// </summary>
        /// <value>The name</value>
        string Name { get; }

        /// <summary>
        /// Determines whether this instance can run the specified argument.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>
        /// <c>true</c> if this instance can run the specified argument; otherwise, <c>false</c>.
        /// </returns>
        bool CanRun(string arg);

        /// <summary>
        /// Creates a command input.
        /// </summary>
        /// <returns>The command input.</returns>
        object CreateInput();

        /// <summary>
        /// Runs the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result</returns>
        Task<int> Run(object input);
    }

    /// <summary>
    /// Command interface
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    public interface ICommand<TInput> : ICommand
    {
    }
}
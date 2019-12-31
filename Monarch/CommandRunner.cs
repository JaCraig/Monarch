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

using Monarch.Commands;
using System.Threading.Tasks;

namespace Monarch
{
    /// <summary>
    /// Command runner
    /// </summary>
    public class CommandRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRunner"/> class.
        /// </summary>
        public CommandRunner()
            : this(Canister.Builder.Bootstrapper?.Resolve<CommandManager>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRunner"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CommandRunner(CommandManager? manager)
        {
            Manager = manager;
        }

        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <value>The manager.</value>
        public CommandManager? Manager { get; }

        /// <summary>
        /// Runs the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The result.</returns>
        public Task<int> Run(string[] args)
        {
            if (Manager == null)
                return Task.FromResult(0);
            var Command = Manager.GetCommand(args);
            var Input = Manager.GetInput(Command, args);
            return Command.Run(Input);
        }
    }
}
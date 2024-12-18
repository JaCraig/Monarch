﻿/*
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

using BigBook.ExtensionMethods;
using Monarch.Commands.Default;
using Monarch.Commands.Interfaces;
using Monarch.Exceptions;
using Monarch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monarch.Commands.Parser
{
    /// <summary>
    /// Arg parser
    /// </summary>
    /// <seealso cref="IArgParser"/>
    public class ArgParser : IArgParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgParser"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="commands">The commands.</param>
        public ArgParser(IOptions options, IEnumerable<ICommand> commands)
        {
            Commands = commands;
            Options = options;
        }

        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <value>The commands.</value>
        public IEnumerable<ICommand> Commands { get; }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        public IOptions Options { get; }

        /// <summary>
        /// Gets the tokens.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Converts the args into tokens.</returns>
        /// <exception cref="ParsingException">Unable to find command.</exception>
        public TokenBaseClass[] GetTokens(string?[]? args)
        {
            if (args is null || args.Length == 0)
                return Array.Empty<TokenBaseClass>();
            var Results = new List<TokenBaseClass>();
            var Found = false;
            if (args[0]?.StartsWith(Options.CommandPrefix, StringComparison.Ordinal) == true)
            {
                Found |= Commands.Any(x => x.CanRun(args[0].StripLeft(Options.CommandPrefix) ?? ""));
            }
            if (Found)
            {
                Results.Add(new CommandToken(args[0].StripLeft(Options.CommandPrefix) ?? ""));
            }
            else if (Commands.Count(x => x is not HelpCommand and not VersionCommand) == 1)
            {
                Results.Add(new CommandToken(Commands.First(x => x is not HelpCommand and not VersionCommand).Aliases[0]));
            }
            else
            {
                Results.Add(new CommandToken(Commands.First(x => x is HelpCommand).Aliases[0]));
            }

            for (var X = Found ? 1 : 0; X < args.Length; ++X)
            {
                if (args[X]?.StartsWith(Options.FlagPrefix, StringComparison.Ordinal) == true)
                    Results.Add(new OptionNameToken(args[X]?.Replace(Options.FlagPrefix, "") ?? ""));
                else
                    Results.Add(new OptionValueToken(args[X] ?? ""));
            }
            return Results.ToArray();
        }
    }
}
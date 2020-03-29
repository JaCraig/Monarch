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
using Monarch.Commands.Default;
using Monarch.Commands.Interfaces;
using Monarch.Commands.Lexer;
using Monarch.Commands.Parser;
using Monarch.Defaults;
using Monarch.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Valkyrie;

namespace Monarch.Commands
{
    /// <summary>
    /// Manager class.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        /// <param name="commands">The commands.</param>
        /// <param name="options">The options.</param>
        /// <param name="lexer">The lexer.</param>
        /// <param name="parser">The parser.</param>
        public CommandManager(IEnumerable<ICommand> commands, IEnumerable<IOptions> options, IEnumerable<IArgLexer> lexer, IEnumerable<IArgParser> parser)
        {
            Commands = commands ?? new List<ICommand>();
            Options = options.FirstOrDefault(x => !(x is DefaultOptions)) ?? new DefaultOptions();
            Lexer = lexer.FirstOrDefault(x => !(x is ArgLexer)) ?? new ArgLexer();
            Parser = parser.FirstOrDefault(x => !(x is ArgParser)) ?? new ArgParser(Options, Commands);
        }

        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <value>The commands.</value>
        public IEnumerable<ICommand> Commands { get; }

        /// <summary>
        /// Gets the lexer.
        /// </summary>
        /// <value>The lexer.</value>
        public IArgLexer Lexer { get; }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        public IOptions Options { get; }

        /// <summary>
        /// Gets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public IArgParser Parser { get; }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The command specified by the arguments.</returns>
        public ICommand GetCommand(string[] args)
        {
            args ??= Array.Empty<string>();
            if (args.Length == 0)
                return GetCommand(new string[] { Options.CommandPrefix + "?" });
            for (int x = 0; x < args.Length; ++x)
            {
                var TempCommand = GetCommand(args[x]);
                if (!(TempCommand is null))
                    return TempCommand;
            }
            throw new Exception("Could not find command");
        }

        /// <summary>
        /// Gets the input based on the args passed in.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The resulting command input.</returns>
        public object GetInput(ICommand command, string[] args)
        {
            var Tokens = Parser.GetTokens(args);

            var InputObject = command.CreateInput();

            var InputProperties = InputObject.GetType()
                                                .GetProperties()
                                                .OrderBy(x => x.Attribute<DisplayAttribute>()?.GetOrder() ?? int.MaxValue)
                                                .ThenBy(x => x.Name)
                                                .ToArray();

            var Tree = Lexer.Lex(Tokens.ToList(), InputProperties);

            var ReturnValue = Tree.GetValue(InputObject);
            ReturnValue.Validate();

            return ReturnValue;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <returns>The appropriate command.</returns>
        private ICommand? GetCommand(string arg)
        {
            if (!arg.StartsWith(Options.CommandPrefix, StringComparison.OrdinalIgnoreCase))
                return null;
            arg = arg.StripLeft(Options.CommandPrefix).ToUpper();
            var PotentialCommand = Commands.FirstOrDefault(x => x.Aliases.Contains(arg)) ??
                    Commands.FirstOrDefault(x => x.Aliases.Select(y => y.ToUpper()).Contains(arg));
            if (!(PotentialCommand is null))
                return PotentialCommand;
            if (Commands.Count() == 3)
                return Commands.FirstOrDefault(x => !(x is HelpCommand) && !(x is VersionCommand));
            if (Commands.Count() == 2)
                return Commands.FirstOrDefault(x => x is HelpCommand);
            return null;
        }
    }
}
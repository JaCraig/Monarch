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

using BigBook;
using BigBook.ExtensionMethods;
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
using Valkyrie.ExtensionMethods;

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
        public CommandManager(IEnumerable<ICommand>? commands, IEnumerable<IOptions>? options, IEnumerable<IArgLexer>? lexer, IEnumerable<IArgParser>? parser)
        {
            Commands = commands ?? [];
            Options = options?.FirstOrDefault(x => x is not DefaultOptions) ?? new DefaultOptions();
            Lexer = lexer?.FirstOrDefault(x => x is not ArgLexer) ?? new ArgLexer();
            Parser = parser?.FirstOrDefault(x => x is not ArgParser) ?? new ArgParser(Options, Commands);
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
        public ICommand GetCommand(string?[]? args)
        {
            args ??= Array.Empty<string?>();
            if (args.Length == 0)
                return GetCommand(new string[] { Options.CommandPrefix + "?" });
            for (var X = 0; X < args.Length; ++X)
            {
                ICommand? TempCommand = GetCommand(args[X] ?? "");
                if (TempCommand is not null)
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
        public object? GetInput(ICommand? command, string?[]? args)
        {
            if (command is null)
                return null;

            TokenBaseClass[] Tokens = Parser.GetTokens(args);

            var InputObject = command.CreateInput();

            if (InputObject is null)
                return null;

            System.Reflection.PropertyInfo[] InputProperties = InputObject.GetType()
                                                .GetProperties()
                                                .OrderBy(x => x.Attribute<DisplayAttribute>()?.GetOrder() ?? int.MaxValue)
                                                .ThenBy(x => x.Name)
                                                .ToArray();

            Command Tree = Lexer.Lex(Tokens.ToList(), InputProperties);

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
            if (string.IsNullOrEmpty(arg) || !arg.StartsWith(Options.CommandPrefix, StringComparison.OrdinalIgnoreCase))
                return null;
            arg = arg.StripLeft(Options.CommandPrefix)?.ToUpper() ?? "";
            ICommand? PotentialCommand = Commands.FirstOrDefault(x => x.Aliases.Contains(arg)) ??
                    Commands.FirstOrDefault(x => x.Aliases.Select(y => y.ToUpper()).Contains(arg));
            return PotentialCommand ?? (Commands.Count() == 3
                ? Commands.FirstOrDefault(x => x is not HelpCommand and not VersionCommand)
                : Commands.Count() == 2 ? Commands.FirstOrDefault(x => x is HelpCommand) : null);
        }
    }
}
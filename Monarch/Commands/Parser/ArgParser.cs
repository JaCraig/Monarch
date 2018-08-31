using BigBook;
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
    public class ArgParser
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
        public TokenBaseClass[] GetTokens(string[] args)
        {
            if (args == null || args.Length == 0)
                return Array.Empty<TokenBaseClass>();
            var Results = new List<TokenBaseClass>();
            bool Found = false;
            if (args[0].StartsWith(Options.CommandPrefix, StringComparison.Ordinal))
            {
                Found |= Commands.Any(x => x.CanRun(args[0].StripLeft(Options.CommandPrefix)));
            }
            if (Found)
            {
                Results.Add(new CommandToken(args[0].StripLeft(Options.CommandPrefix)));
            }
            else if (Commands.Count(x => !(x is HelpCommand) && !(x is VersionCommand)) == 1)
            {
                Results.Add(new CommandToken(Commands.First(x => !(x is HelpCommand) && !(x is VersionCommand)).Aliases[0]));
            }
            else
            {
                Results.Add(new CommandToken(Commands.First(x => x is HelpCommand).Aliases[0]));
            }

            for (int x = Found ? 1 : 0; x < args.Length; ++x)
            {
                if (args[x].StartsWith(Options.FlagPrefix, StringComparison.Ordinal))
                    Results.Add(new OptionNameToken(args[x].Replace(Options.FlagPrefix, "")));
                else
                    Results.Add(new OptionValueToken(args[x]));
            }
            return Results.ToArray();
        }
    }
}
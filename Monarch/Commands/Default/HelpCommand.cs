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
using Monarch.Commands.BaseClasses;
using Monarch.Commands.Interfaces;
using Monarch.Defaults;
using Monarch.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.Commands.Default
{
    /// <summary>
    /// Help command
    /// </summary>
    /// <seealso cref="CommandBaseClass{HelpInput}"/>
    public class HelpCommand : CommandBaseClass<HelpInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpCommand"/> class.
        /// </summary>
        /// <param name="console">The console.</param>
        /// <param name="options">The options.</param>
        public HelpCommand(IEnumerable<IConsoleWriter> console, IEnumerable<IOptions> options)
        {
            Console = console.FirstOrDefault(x => !(x is DefaultConsoleWriter)) ?? new DefaultConsoleWriter(options);
            Options = options.FirstOrDefault(x => !(x is DefaultOptions)) ?? new DefaultOptions();
        }

        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        public override string[] Aliases => new string[] { "?" };

        /// <summary>
        /// Gets the console.
        /// </summary>
        /// <value>The console.</value>
        public IConsoleWriter Console { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public override string Description => "Show command line help";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Help";

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>The options.</value>
        public IOptions Options { get; }

        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <value>The commands.</value>
        private IEnumerable<ICommand> Commands => Canister.Builder.Bootstrapper.ResolveAll<ICommand>();

        /// <summary>
        /// Runs the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        protected override async Task<int> Run(HelpInput input)
        {
            await Task.CompletedTask.ConfigureAwait(false);
            var AppAssembly = Assembly.GetEntryAssembly();
            var CustomAttributes = AppAssembly.GetCustomAttributes();
            var AppName = AppAssembly.GetName();

            Console.Indent();
            if (string.IsNullOrEmpty(input.Command))
                PrintDefaultHelp(CustomAttributes, AppName);
            else
                PrintCommandHelp(input);
            Console.Outdent();
            return 0;
        }

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <returns></returns>
        private int GetMaxLength()
        {
            return Commands.Max(x => (x.Aliases.ToString(y => Options.CommandPrefix + y, ", ") + "    ").Length);
        }

        /// <summary>
        /// Prints the command help.
        /// </summary>
        /// <param name="input">The input.</param>
        private void PrintCommandHelp(HelpInput input)
        {
            var CommandUsing = Commands.FirstOrDefault(x => x.Aliases.Select(y => y.ToUpper()).Contains(input.Command?.ToUpper()));
            if (CommandUsing == null)
            {
                Console.WriteLine($"'{input.Command}' is not found as a command.");
                return;
            }
            Console.WriteLine($"Command: '{input.Command.ToString(StringCase.FirstCharacterUpperCase)}' ({CommandUsing.Description})")
                    .WriteLine();

            var Properties = CommandUsing.CreateInput().GetType().GetProperties().OrderBy(x => x.Name).ToArray();

            var UsageBuilder = new StringBuilder();
            UsageBuilder.Append("Usage: ").Append(input.Command).Append(" ");
            for (int x = 0; x < Properties.Length; ++x)
            {
                if ((Properties[x].PropertyType.IsGenericType
                    && Properties[x].PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>))
                    || Properties[x].Attribute<RequiredAttribute>() != null)
                {
                    UsageBuilder.Append("<").Append(Properties[x].Name).Append("> ");
                }
                else
                {
                    UsageBuilder.Append("[").Append(Properties[x].Name).Append("] ");
                }
            }

            Console.WriteLine(UsageBuilder.ToString());

            if (Properties.Length > 0)
            {
                Console.WriteSeparator()
                        .WriteLine("Options")
                        .WriteLine()
                        .WriteSeparator();

                var MaxLength = Properties.Max(x => (x.Name + "    ").Length);

                for (int x = 0; x < Properties.Length; ++x)
                {
                    var Builder = new StringBuilder();
                    Builder.Append(SetLength(Properties[x].Name, MaxLength));

                    Builder.Append(Properties[x].GetCustomAttribute<DisplayAttribute>()?.Description);

                    var ValidationAttributes = Properties[x].GetCustomAttributes<ValidationAttribute>();

                    Builder.Append(" ");

                    if (ValidationAttributes.FirstOrDefault(y => y is MaxLengthAttribute) is MaxLengthAttribute MaxLengthAttr)
                        Builder.Append("[Max Length = ").Append(MaxLengthAttr.Length).Append("]");
                    if (ValidationAttributes.FirstOrDefault(y => y is MinLengthAttribute) is MinLengthAttribute MinLengthAttr)
                        Builder.Append("[Min Length = ").Append(MinLengthAttr.Length).Append("]");

                    Builder.Append(ValidationAttributes.Any(y => !(y is MinLengthAttribute) && !(y is MaxLengthAttribute)) ? ", " : " ")
                        .Append(ValidationAttributes.Where(y => !(y is MinLengthAttribute) && !(y is MaxLengthAttribute))
                        .ToString(y => "[" + y.GetType().Name.Replace("Attribute", "") + "]", ", "));

                    Console.WriteLine(Builder.ToString());
                }
            }
        }

        /// <summary>
        /// Prints the command information.
        /// </summary>
        /// <param name="MaxLength">The maximum length.</param>
        /// <param name="Command">The command.</param>
        private void PrintCommandInfo(int MaxLength, ICommand Command)
        {
            Console.WriteLine(SetLength(Command.Aliases.ToString(x => Options.CommandPrefix + x, ", "), MaxLength) + Command.Description);
        }

        /// <summary>
        /// Prints the default help.
        /// </summary>
        /// <param name="CustomAttributes">The custom attributes.</param>
        /// <param name="AppName">Name of the application.</param>
        private void PrintDefaultHelp(IEnumerable<Attribute> CustomAttributes, AssemblyName AppName)
        {
            PrintHeader(CustomAttributes, AppName);

            int MaxLength = GetMaxLength();
            PrintUserCommands(MaxLength);
            PrintInternalCommands(MaxLength);
        }

        /// <summary>
        /// Prints the header.
        /// </summary>
        /// <param name="CustomAttributes">The custom attributes.</param>
        /// <param name="AppName">Name of the application.</param>
        private void PrintHeader(IEnumerable<Attribute> CustomAttributes, AssemblyName AppName)
        {
            Console.WriteLine($"{CustomAttributes.OfType<AssemblyProductAttribute>().FirstOrDefault()?.Product ?? ""} ({AppName.Version})")
                    .WriteLine(CustomAttributes.OfType<AssemblyCopyrightAttribute>().FirstOrDefault()?.Copyright ?? "")
                    .WriteLine()
                    .WriteSeparator()
                    .WriteLine(CustomAttributes.OfType<AssemblyDescriptionAttribute>().FirstOrDefault()?.Description ?? "")
                    .WriteLine()
                    .WriteSeparator()
                    .WriteLine($"Usage: {AppName.Name} [command] [command-options]")
                    .WriteLine()
                    .WriteLine("Commands:")
                    .WriteLine();
        }

        /// <summary>
        /// Prints the internal commands.
        /// </summary>
        /// <param name="MaxLength">The maximum length.</param>
        private void PrintInternalCommands(int MaxLength)
        {
            Console.WriteLine();
            foreach (var Command in Commands
                                .Where(x => (x is HelpCommand) || (x is VersionCommand))
                                .OrderBy(x => x.Aliases.FirstOrDefault()))
            {
                PrintCommandInfo(MaxLength, Command);
            }
        }

        /// <summary>
        /// Prints the user commands.
        /// </summary>
        /// <param name="MaxLength">The maximum length.</param>
        private void PrintUserCommands(int MaxLength)
        {
            foreach (var Command in Commands
                            .Where(x => !(x is HelpCommand) && !(x is VersionCommand))
                            .OrderBy(x => x.Aliases.FirstOrDefault()))
            {
                PrintCommandInfo(MaxLength, Command);
            }
        }

        /// <summary>
        /// Sets the length.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>The resulting string</returns>
        private string SetLength(string v, int maxLength)
        {
            var NumberSpaces = maxLength - v.Length;
            return v + new string(' ', NumberSpaces);
        }
    }
}
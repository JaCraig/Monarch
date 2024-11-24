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
using BigBook.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using Monarch.Commands.Attributes;
using Monarch.Commands.BaseClasses;
using Monarch.Commands.Interfaces;
using Monarch.Defaults;
using Monarch.ExtensionMethods;
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
        public HelpCommand(IEnumerable<IConsoleWriter>? console, IEnumerable<IOptions>? options)
        {
            Console = console?.FirstOrDefault(x => x is not DefaultConsoleWriter) ?? new DefaultConsoleWriter(options);
            Options = options?.FirstOrDefault(x => x is not DefaultOptions) ?? new DefaultOptions();
        }

        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        public override string[] Aliases => new string[] { "?", "help", "h" };

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
        private static IEnumerable<ICommand> Commands => Services.ServiceProvider?.GetServices<ICommand>() ?? Array.Empty<ICommand>();

        /// <summary>
        /// Runs the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The result.</returns>
        protected override async Task<int> Run(HelpInput? input)
        {
            input ??= new HelpInput();
            await Task.CompletedTask.ConfigureAwait(false);
            var AppAssembly = Assembly.GetEntryAssembly();
            if (AppAssembly is null)
                return 0;
            IEnumerable<Attribute> CustomAttributes = AppAssembly.GetCustomAttributes();
            AssemblyName AppName = AppAssembly.GetName();

            _ = Console.Indent();
            if (string.IsNullOrEmpty(input.Command))
                PrintDefaultHelp(CustomAttributes, AppName);
            else
                PrintCommandHelp(input);
            _ = Console.Outdent();
            return 0;
        }

        /// <summary>
        /// Sets the length.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>The resulting string</returns>
        private static string SetLength(string v, int maxLength)
        {
            var NumberSpaces = maxLength - v.Length;
            return v + new string(' ', NumberSpaces);
        }

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <returns></returns>
        private int GetMaxLength() => Commands.Max(x => (x.Aliases.ToString(y => Options.CommandPrefix + y, ", ") + "    ").Length);

        /// <summary>
        /// Prints the command help.
        /// </summary>
        /// <param name="input">The input.</param>
        private void PrintCommandHelp(HelpInput input)
        {
            ICommand? CommandUsing = Commands.FirstOrDefault(x => x.Aliases.Select(y => y.ToUpper()).Contains(input.Command?.ToUpper()));
            if (CommandUsing is null)
            {
                _ = Console.WriteLine($"'{input.Command}' is not found as a command.");
                return;
            }
            _ = Console.WriteLine($"Command: '{input.Command?.ToString(StringCase.FirstCharacterUpperCase)}' ({CommandUsing.Description})")
                    .WriteLine();

            PropertyInfo[] Properties = CommandUsing.CreateInput().GetType().GetProperties().OrderBy(x => x.Name).ToArray();

            var UsageBuilder = new StringBuilder();
            _ = UsageBuilder.Append("Usage: ").Append(input.Command).Append(' ');
            for (var X = 0; X < Properties.Length; ++X)
            {
                _ = (Properties[X].PropertyType.IsGenericType
                    && Properties[X].PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>))
                    || Properties[X].Attribute<RequiredAttribute>() != null
                    ? UsageBuilder.Append('<').Append(Properties[X].Name).Append("> ")
                    : UsageBuilder.Append('[').Append(Properties[X].Name).Append("] ");
            }

            _ = Console.WriteLine(UsageBuilder.ToString());

            if (Properties.Length > 0)
            {
                _ = Console.WriteSeparator()
                        .WriteLine("Options")
                        .WriteLine()
                        .WriteSeparator();

                var MaxLength = Properties.Max(x => (x.Name + "    ").Length);

                for (var X = 0; X < Properties.Length; ++X)
                {
                    var Builder = new StringBuilder();
                    _ = Builder.Append(SetLength(Properties[X].Name, MaxLength));

                    _ = Builder.Append(Properties[X].GetCustomAttribute<DisplayAttribute>()?.Description);

                    _ = Builder.Append(Properties[X].GetCustomAttribute<DynamicDisplayAttribute>()?.GetDescription());

                    _ = Builder.Append(' ');

                    if (Properties[X].PropertyType.IsEnum)
                    {
                        _ = Builder.Append("[Values = (").Append(Enum.GetNames(Properties[X].PropertyType).ToString(x => "'" + x + "'")).Append(")] ");
                    }

                    IEnumerable<ValidationAttribute> ValidationAttributes = Properties[X].GetCustomAttributes<ValidationAttribute>();

                    if (ValidationAttributes.FirstOrDefault(y => y is MaxLengthAttribute) is MaxLengthAttribute MaxLengthAttr)
                        _ = Builder.Append("[Max Length = ").Append(MaxLengthAttr.Length).Append(']');
                    if (ValidationAttributes.FirstOrDefault(y => y is MinLengthAttribute) is MinLengthAttribute MinLengthAttr)
                        _ = Builder.Append("[Min Length = ").Append(MinLengthAttr.Length).Append(']');

                    _ = Builder.Append(ValidationAttributes.Any(y => y is not MinLengthAttribute and not MaxLengthAttribute) ? ", " : " ")
                        .Append(ValidationAttributes.Where(y => y is not MinLengthAttribute and not MaxLengthAttribute)
                        .ToString(y => "[" + y.GetType().Name.Replace("Attribute", "") + "]", ", "));

                    _ = Console.WriteLine(Builder.ToString());
                }
            }
        }

        /// <summary>
        /// Prints the command information.
        /// </summary>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="command">The command.</param>
        private void PrintCommandInfo(int maxLength, ICommand command) => Console.WriteLine(SetLength(command.Aliases.ToString(x => Options.CommandPrefix + x, ", "), maxLength) + command.Description);

        /// <summary>
        /// Prints the default help.
        /// </summary>
        /// <param name="customAttributes">The custom attributes.</param>
        /// <param name="appName">Name of the application.</param>
        private void PrintDefaultHelp(IEnumerable<Attribute> customAttributes, AssemblyName appName)
        {
            PrintHeader(customAttributes, appName);

            var MaxLength = GetMaxLength();
            PrintUserCommands(MaxLength);
            PrintInternalCommands(MaxLength);
        }

        /// <summary>
        /// Prints the header.
        /// </summary>
        /// <param name="customAttributes">The custom attributes.</param>
        /// <param name="appName">Name of the application.</param>
        private void PrintHeader(IEnumerable<Attribute> customAttributes, AssemblyName appName)
        {
            _ = Console.WriteLine($"{customAttributes.OfType<AssemblyProductAttribute>().FirstOrDefault()?.Product ?? ""} ({appName.Version})")
                    .WriteLine(customAttributes.OfType<AssemblyCopyrightAttribute>().FirstOrDefault()?.Copyright ?? "")
                    .WriteLine()
                    .WriteSeparator()
                    .WriteLine(customAttributes.OfType<AssemblyDescriptionAttribute>().FirstOrDefault()?.Description ?? "")
                    .WriteLine()
                    .WriteSeparator()
                    .WriteLine($"Usage: {appName.Name} [command] [command-options]")
                    .WriteLine()
                    .WriteLine("Commands:")
                    .WriteLine();
        }

        /// <summary>
        /// Prints the internal commands.
        /// </summary>
        /// <param name="maxLength">The maximum length.</param>
        private void PrintInternalCommands(int maxLength)
        {
            _ = Console.WriteLine();
            foreach (ICommand? Command in Commands
                                .Where(x => x is HelpCommand or VersionCommand)
                                .OrderBy(x => x.Aliases.FirstOrDefault()))
            {
                PrintCommandInfo(maxLength, Command);
            }
        }

        /// <summary>
        /// Prints the user commands.
        /// </summary>
        /// <param name="maxLength">The maximum length.</param>
        private void PrintUserCommands(int maxLength)
        {
            foreach (ICommand? Command in Commands
                            .Where(x => x is not HelpCommand and not VersionCommand)
                            .OrderBy(x => x.Aliases.FirstOrDefault()))
            {
                PrintCommandInfo(maxLength, Command);
            }
        }
    }
}
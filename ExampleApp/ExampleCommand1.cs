using BigBook;
using Microsoft.Extensions.DependencyInjection;
using Monarch.Commands.Attributes;
using Monarch.Commands.BaseClasses;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ExampleApp
{
    /// <summary>
    /// Example command available to the user to run from the command line
    /// </summary>
    /// <seealso cref="CommandBaseClass&lt;ExampleInput&gt;" />
    public class ExampleCommand1 : CommandBaseClass<ExampleInput>
    {
        /// <summary>
        /// Gets the aliases. These are the names that can be used to call the command.
        /// </summary>
        /// <value>
        /// The aliases.
        /// </value>
        public override string[] Aliases { get; } = new[] { "Example1" };

        /// <summary>
        /// Gets the description. This is what is displayed when the user calls the help command.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public override string Description { get; } = "Example command.";

        /// <summary>
        /// Gets the name. This is the name of the command.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name { get; } = "Example Command 1";

        /// <summary>
        /// Runs the specified input. This is where the command does its work.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// The result.
        /// </returns>
        protected override Task<int> Run(ExampleInput input)
        {
            Console.WriteLine(input.ExampleEnum);
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// Example input for the command.
    /// </summary>
    public class ExampleInput
    {
        /// <summary>
        /// Gets or sets the example enum. This is an example of an enum input.
        /// </summary>
        /// <value>
        /// The example enum.
        /// </value>
        public ExampleEnum ExampleEnum { get; set; }

        /// <summary>
        /// Gets or sets the name value. This is an example of a string input. The <see cref="DynamicDisplayAttribute"/> is used to display the current assemblies loaded.
        /// </summary>
        /// <value>
        /// The name value.
        /// </value>
        [DynamicDisplay(typeof(NameValueDisplay))]
        public string NameValue { get; set; }
    }

    /// <summary>
    /// Used to display the current assemblies loaded.
    /// </summary>
    internal class NameValueDisplay
    {
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Assemblies currently loaded: " + new ServiceCollection().AddCanisterModules().BuildServiceProvider().GetServices<Assembly>().ToString(x => x.GetName().Name);
        }
    }

    /// <summary>
    /// Example enum for the possible values
    /// </summary>
    public enum ExampleEnum
    {
        /// <summary>
        /// The value1
        /// </summary>
        Value1,

        /// <summary>
        /// The value2
        /// </summary>
        Value2,

        /// <summary>
        /// The value3
        /// </summary>
        Value3
    }
}
using BigBook;
using Monarch.Commands.Attributes;
using Monarch.Commands.BaseClasses;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ExampleApp
{
    public enum ExampleEnum
    {
        Value1,
        Value2,
        Value3
    }

    public class ExampleCommand1 : CommandBaseClass<ExampleInput>
    {
        public override string[] Aliases { get; } = new[] { "Example1" };

        public override string Description { get; } = "Example command.";

        public override string Name { get; } = "Example Command 1";

        protected override Task<int> Run(ExampleInput input)
        {
            Console.WriteLine(input.ExampleEnum);
            return Task.FromResult(0);
        }
    }

    public class ExampleInput
    {
        public ExampleEnum ExampleEnum { get; set; }

        [DynamicDisplay(typeof(NameValueDisplay))]
        public string NameValue { get; set; }
    }

    internal class NameValueDisplay
    {
        public override string ToString()
        {
            return "Assemblies currently loaded: " + Canister.Builder.Bootstrapper.ResolveAll<Assembly>().ToString(x => x.GetName().Name);
        }
    }
}
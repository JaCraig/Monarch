using BigBook;
using Monarch.Commands.BaseClasses;
using System;
using System.Threading.Tasks;

namespace TestApp
{
    public class TestCommand : CommandBaseClass<TestInput>
    {
        public override string[] Aliases => new string[] { "Test" };

        public override string Description => "Test command";

        public override string Name => "Test Command";

        protected override async Task<int> Run(TestInput input)
        {
            await Task.CompletedTask;
            Console.WriteLine(input.Value1);
            Console.WriteLine(input.Value2);
            Console.WriteLine(input.Value3.ToString(x => x));
            return 0;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Monarch;

namespace ExampleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new ServiceCollection().AddCanisterModules();

            new ServiceCollection().AddCanisterModules().BuildServiceProvider().GetService<CommandRunner>().Run(args);
        }
    }
}
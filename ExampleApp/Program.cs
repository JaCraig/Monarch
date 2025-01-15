using Microsoft.Extensions.DependencyInjection;
using Monarch;

namespace ExampleApp
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            // Run the command runner with the provided arguments
            new ServiceCollection().AddCanisterModules().BuildServiceProvider().GetService<CommandRunner>().Run(args);
    }
}
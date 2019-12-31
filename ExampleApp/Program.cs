using Monarch;
using System.Reflection;

namespace ExampleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Canister.Builder.CreateContainer(null, typeof(Program).GetTypeInfo().Assembly)
                   .RegisterMonarch()
                   .Build();

            Canister.Builder.Bootstrapper.Resolve<CommandRunner>().Run(args);
        }
    }
}
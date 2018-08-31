using Monarch;
using System;
using System.Threading.Tasks;

namespace TestApp
{
    /// <summary>
    /// Program
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private static Task<int> Main(string[] args)
        {
            //args = new string[] { "version" };
            args = new string[] { "1", "2", "3", "4", "5", "6" };

            //args = new string[] { "test", "1", "2", "3", "4", "5", "6" };
            //args = new string[] { "?", "test" };
            Canister.Builder.CreateContainer(null, typeof(Program).Assembly)
                .RegisterMonarch()
                .Build();

            var Result = new CommandRunner().Run(args);
            Console.ReadLine();
            return Result;
        }
    }
}
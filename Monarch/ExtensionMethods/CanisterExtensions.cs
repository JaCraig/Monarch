﻿/*
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

using Canister.Interfaces;
using Monarch;
using Monarch.Commands;
using Monarch.Commands.Interfaces;
using Monarch.Commands.Lexer;
using Monarch.Commands.Parser;
using Monarch.Interfaces;
using Valkyrie.Registration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Canister extensions
    /// </summary>
    public static class MonarchCanisterExtensions
    {
        /// <summary>
        /// Registers monarch with Canister.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <returns>The bootstrapper.</returns>
        public static ICanisterConfiguration? RegisterMonarch(this ICanisterConfiguration? bootstrapper) => bootstrapper?.AddAssembly(typeof(MonarchCanisterExtensions).Assembly);

        /// <summary>
        /// Registers Monarch services with the provided IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection? RegisterMonarch(this IServiceCollection? services)
        {
            if (services.Exists<CommandManager>())
                return services;
            return services?.AddAllSingleton<ICommand>()
                    ?.AddAllSingleton<IOptions>()
                    ?.AddAllSingleton<IConsoleWriter>()
                    ?.AddTransient<IArgParser, ArgParser>()
                    ?.AddTransient<IArgLexer, ArgLexer>()
                    ?.AddSingleton<CommandManager>()
                    ?.AddSingleton<CommandRunner>()
                    ?.RegisterValkyrie();
        }
    }
}
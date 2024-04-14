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
using Monarch.Commands.Interfaces;
using Monarch.Commands.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Monarch.Commands.Lexer
{
    /// <summary>
    /// Arg Lexer
    /// </summary>
    /// <seealso cref="IArgLexer"/>
    public class ArgLexer : IArgLexer
    {
        /// <summary>
        /// Lexes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>The command</returns>
        public Command Lex(List<TokenBaseClass>? tokens, PropertyInfo[]? properties)
        {
            properties ??= Array.Empty<PropertyInfo>();
            var Results = new Command();
            if (tokens is null || tokens.Count == 0)
                return Results;

            Results.Name = tokens.Find(x => x is CommandToken);
            tokens = tokens.Where(x => x is not CommandToken).ToList();

            if (properties.Length == 0)
                return Results;

            Property? CurrentProperty = null;

            while (tokens.Count > 0)
            {
                if (tokens[0] is OptionNameToken)
                {
                    CurrentProperty = Results.Properties.Find(x => x.FlagName?.UpperValue == tokens[0].UpperValue);
                    if (CurrentProperty is null)
                    {
                        CurrentProperty = new Property
                        {
                            FlagName = tokens[0],
                            PropertyInfo = System.Array.Find(properties, x => string.Equals(x.Name.ToUpper(), tokens[0].UpperValue, System.StringComparison.CurrentCulture))
                        };
                        properties = properties.Remove(new PropertyInfo[] { CurrentProperty.PropertyInfo! }).ToArray();
                        Results.Properties.Add(CurrentProperty);
                    }
                }
                else if (CurrentProperty is null && properties.Length > 0)
                {
                    CurrentProperty = new Property
                    {
                        FlagName = new OptionNameToken(properties[0].Name),
                        PropertyInfo = properties[0]
                    };
                    CurrentProperty.FlagValue.Add(tokens[0]);
                    properties = properties.Remove(new PropertyInfo[] { properties[0] }).ToArray();
                    Results.Properties.Add(CurrentProperty);
                }
                else if (CurrentProperty != null)
                {
                    if (CurrentProperty.MaxValueCount <= CurrentProperty.FlagValue.Count && properties.Length > 0)
                    {
                        CurrentProperty = new Property
                        {
                            FlagName = new OptionNameToken(properties[0].Name),
                            PropertyInfo = properties[0]
                        };
                        CurrentProperty.FlagValue.Add(tokens[0]);
                        properties = properties.Remove(new PropertyInfo[] { properties[0] }).ToArray();
                        Results.Properties.Add(CurrentProperty);
                    }
                    else if (CurrentProperty.MaxValueCount > CurrentProperty.FlagValue.Count)
                    {
                        CurrentProperty.FlagValue.Add(tokens[0]);
                    }
                }
                _ = tokens.Remove(tokens[0]);
            }
            return Results;
        }
    }
}
using BigBook;
using Monarch.Commands.Parser;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Monarch.Commands.Lexer
{
    public class ArgLexer
    {
        public Command Lex(List<TokenBaseClass> tokens, PropertyInfo[] properties)
        {
            var Results = new Command();
            if (tokens.Count == 0)
                return Results;

            Results.Name = tokens.FirstOrDefault(x => x is CommandToken);
            tokens = tokens.Where(x => !(x is CommandToken)).ToList();

            if (properties.Length == 0)
                return Results;

            Property CurrentProperty = null;

            while (tokens.Count > 0)
            {
                if (tokens[0] is OptionNameToken)
                {
                    CurrentProperty = Results.Properties.FirstOrDefault(x => x.FlagName.UpperValue == tokens[0].UpperValue);
                    if (CurrentProperty == null)
                    {
                        CurrentProperty = new Property
                        {
                            FlagName = tokens[0],
                            PropertyInfo = System.Array.Find(properties, x => string.Equals(x.Name.ToUpper(), tokens[0].UpperValue, System.StringComparison.CurrentCulture))
                        };
                        properties = properties.Remove(new PropertyInfo[] { CurrentProperty.PropertyInfo }).ToArray();
                        Results.Properties.Add(CurrentProperty);
                    }
                }
                else if (CurrentProperty == null && properties.Length > 0)
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
                else
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
                tokens.Remove(tokens[0]);
            }
            return Results;
        }
    }
}
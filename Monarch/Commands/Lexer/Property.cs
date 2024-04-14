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
using Monarch.Commands.Parser;
using ObjectCartographer;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Monarch.Commands.Lexer
{
    /// <summary>
    /// Property Info
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        public Property()
        {
            FlagValue = new List<TokenBaseClass>();
        }

        /// <summary>
        /// Gets or sets the name of the flag.
        /// </summary>
        /// <value>The name of the flag.</value>
        public TokenBaseClass? FlagName { get; set; }

        /// <summary>
        /// Gets or sets the flag value.
        /// </summary>
        /// <value>The flag value.</value>
        public List<TokenBaseClass> FlagValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is ienumerable.
        /// </summary>
        /// <value><c>true</c> if this instance is ienumerable; otherwise, <c>false</c>.</value>
        public bool IsIEnumerable => PropertyInfo?.PropertyType != PropertyInfo?.PropertyType.GetIEnumerableElementType();

        /// <summary>
        /// Gets the maximum value count.
        /// </summary>
        /// <value>The maximum value count.</value>
        public int MaxValueCount => IsIEnumerable ? (PropertyInfo?.Attribute<MaxLengthAttribute>()?.Length ?? int.MaxValue) : 1;

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>The property.</value>
        public PropertyInfo? PropertyInfo { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="inputObject">The input object.</param>
        public void GetValue(object? inputObject)
        {
            if (PropertyInfo is null || inputObject is null)
                return;
            if (IsIEnumerable)
            {
                System.Type CurrentPropertyType = PropertyInfo.PropertyType;
                System.Type ConvertToType = CurrentPropertyType.GetIEnumerableElementType();

                var CurrentList = (IList?)typeof(List<>).MakeGenericType(ConvertToType).Create();
                for (int I = 0, FlagValueCount = FlagValue.Count; I < FlagValueCount; I++)
                {
                    TokenBaseClass Item = FlagValue[I];
                    _ = (CurrentList?.Add(Item.Value.To(ConvertToType, null)));
                }
                PropertyInfo.SetValue(inputObject, CurrentList);
            }
            else
            {
                var CurrentValue = FlagValue[0].Value.To(PropertyInfo.PropertyType, null);
                PropertyInfo.SetValue(inputObject, CurrentValue);
            }
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString() => $"{FlagName} {FlagValue.ToString(x => x.ToString())}";
    }
}
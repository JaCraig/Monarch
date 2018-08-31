using BigBook;
using Monarch.Commands.Parser;
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
        public TokenBaseClass FlagName { get; set; }

        /// <summary>
        /// Gets or sets the flag value.
        /// </summary>
        /// <value>The flag value.</value>
        public List<TokenBaseClass> FlagValue { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is ienumerable.
        /// </summary>
        /// <value><c>true</c> if this instance is ienumerable; otherwise, <c>false</c>.</value>
        public bool IsIEnumerable => PropertyInfo.PropertyType != PropertyInfo.PropertyType.GetIEnumerableElementType();

        /// <summary>
        /// Gets the maximum value count.
        /// </summary>
        /// <value>The maximum value count.</value>
        public int MaxValueCount
        {
            get
            {
                if (IsIEnumerable)
                    return PropertyInfo.Attribute<MaxLengthAttribute>()?.Length ?? int.MaxValue;
                return 1;
            }
        }

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>The property.</value>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="inputObject">The input object.</param>
        public void GetValue(object inputObject)
        {
            if (IsIEnumerable)
            {
                var CurrentPropertyType = PropertyInfo.PropertyType;
                var ConvertToType = CurrentPropertyType.GetIEnumerableElementType();

                var CurrentList = (IList)typeof(List<>).MakeGenericType(ConvertToType).Create();
                for (int i = 0, FlagValueCount = FlagValue.Count; i < FlagValueCount; i++)
                {
                    var Item = FlagValue[i];
                    CurrentList.Add(Item.Value.To(ConvertToType, null));
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
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return FlagName + " " + FlagValue.ToString(x => x.ToString());
        }
    }
}
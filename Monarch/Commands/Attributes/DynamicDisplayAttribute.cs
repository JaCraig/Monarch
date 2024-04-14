using BigBook;
using System;

namespace Monarch.Commands.Attributes
{
    /// <summary>
    /// Dynamic display attribute
    /// </summary>
    /// <seealso cref="Attribute"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DynamicDisplayAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicDisplayAttribute"/> class.
        /// </summary>
        /// <param name="descriptionType">Type of the description.</param>
        public DynamicDisplayAttribute(Type? descriptionType)
        {
            DescriptionType = descriptionType;
        }

        /// <summary>
        /// Gets the type of the description.
        /// </summary>
        /// <value>The type of the description.</value>
        public Type? DescriptionType { get; }

        /// <summary>
        /// Lock object
        /// </summary>
        private readonly object _LockObj = new();

        /// <summary>
        /// The description
        /// </summary>
        private string _Description = "";

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <returns>The description.</returns>
        public string GetDescription()
        {
            if (!string.IsNullOrEmpty(_Description))
                return _Description;
            lock (_LockObj)
            {
                if (!string.IsNullOrEmpty(_Description))
                    return _Description;
                _Description = DescriptionType?.Create()?.ToString() ?? "";
                return _Description;
            }
        }
    }
}
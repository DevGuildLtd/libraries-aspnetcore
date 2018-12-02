using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.ModelMapping.Annotations
{
    /// <summary>
    /// Specifies name of data entity key property in the view model class.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class KeyMappingAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyMappingAttribute"/> class.
        /// </summary>
        /// <param name="entityProperty">The entity property.</param>
        public KeyMappingAttribute(String entityProperty)
        {
            this.EntityProperty = entityProperty;
        }

        /// <summary>
        /// Gets the name of the data entity property.
        /// </summary>
        /// <value>
        /// The name of the data entity property.
        /// </value>
        public String EntityProperty { get; }
    }
}

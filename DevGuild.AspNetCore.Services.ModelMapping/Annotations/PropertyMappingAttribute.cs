using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.ModelMapping.Annotations
{
    /// <summary>
    /// Specifies property mapping configuration.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyMappingAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMappingAttribute"/> class.
        /// </summary>
        /// <param name="mode">The mapping mode.</param>
        public PropertyMappingAttribute(PropertyMappingMode mode)
        {
            this.Mode = mode;
        }

        /// <summary>
        /// Gets or sets the name of the target property.
        /// </summary>
        /// <value>
        /// The name of the target property.
        /// </value>
        public String PropertyName { get; set; } = null;

        /// <summary>
        /// Gets the mapping mode.
        /// </summary>
        /// <value>
        /// The mapping mode.
        /// </value>
        public PropertyMappingMode Mode { get; }
    }
}

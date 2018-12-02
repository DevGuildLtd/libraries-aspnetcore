using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.ModelMapping.Annotations
{
    /// <summary>
    /// Sets default property mapping model.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultPropertyMappingModeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPropertyMappingModeAttribute"/> class.
        /// </summary>
        /// <param name="mode">The mapping mode.</param>
        public DefaultPropertyMappingModeAttribute(PropertyMappingMode mode)
        {
            this.Mode = mode;
        }

        /// <summary>
        /// Gets the mapping mode.
        /// </summary>
        /// <value>
        /// The mapping mode.
        /// </value>
        public PropertyMappingMode Mode { get; }
    }
}

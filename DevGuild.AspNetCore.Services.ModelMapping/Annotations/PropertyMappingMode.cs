using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.ModelMapping.Annotations
{
    /// <summary>
    /// Represents mode of property mapping.
    /// </summary>
    [Flags]
    public enum PropertyMappingMode
    {
        /// <summary>
        /// Property is not mapped.
        /// </summary>
        None = 0,

        /// <summary>
        /// Property is mapped from data model to details model.
        /// </summary>
        ToDetails = 1,

        /// <summary>
        /// Property is mapped from create model to data model.
        /// </summary>
        FromCreate = 2,

        /// <summary>
        /// Property is mapped from update model to data model.
        /// </summary>
        FromUpdate = 4,

        /// <summary>
        /// Property is mapped in all directions.
        /// </summary>
        All = PropertyMappingMode.ToDetails | PropertyMappingMode.FromCreate | PropertyMappingMode.FromUpdate
    }
}

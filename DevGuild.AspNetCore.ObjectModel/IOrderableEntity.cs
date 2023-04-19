using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Represents entity that can be manually ordered.
    /// </summary>
    public interface IOrderableEntity
    {
        /// <summary>
        /// Gets or sets order no of the entity.
        /// </summary>
        Int32 OrderNo { get; set; }
    }
}

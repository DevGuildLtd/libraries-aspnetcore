using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Represents a view model used for hierarchical entities creation.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IHierarchicalCreateModel<TEntity>
    {
        /// <summary>
        /// Gets or sets the parent entity.
        /// </summary>
        /// <value>
        /// The parent entity.
        /// </value>
        TEntity Parent { get; set; }
    }
}

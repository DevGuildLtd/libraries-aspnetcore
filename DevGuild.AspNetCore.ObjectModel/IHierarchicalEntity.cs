using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Defines properties of a hierarchical entity.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IHierarchicalEntity<TIdentifier, TEntity>
        where TIdentifier : struct
    {
        /// <summary>
        /// Gets or sets the parent entity identifier.
        /// </summary>
        /// <value>
        /// The parent entity identifier.
        /// </value>
        TIdentifier? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the parent entity.
        /// </summary>
        /// <value>
        /// The parent entity.
        /// </value>
        TEntity Parent { get; set; }
    }
}

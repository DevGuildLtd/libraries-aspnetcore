using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Represents view model used in the Index views.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public interface IEntityIndexModel<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        ICollection<T> Items { get; set; }
    }
}

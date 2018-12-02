using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Default implementation of the <see cref="IEntityIndexModel{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <seealso cref="IEntityIndexModel{T}" />
    public class DefaultEntityIndexModel<T> : IEntityIndexModel<T>
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ICollection<T> Items { get; set; }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

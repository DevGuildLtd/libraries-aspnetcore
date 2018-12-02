using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Represents a view model that could be use to enable entity selection.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class SelectableEntity<T>
    {
        /// <summary>
        /// Gets or sets the selectable entity.
        /// </summary>
        /// <value>
        /// The selectable entity.
        /// </value>
        public T Item { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the entity is selected; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsSelected { get; set; }
    }
}

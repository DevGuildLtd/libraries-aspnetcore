using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Provides a mechanism to specify original entity in edit view model.
    /// </summary>
    /// <typeparam name="TDetailsModel">The type of the model.</typeparam>
    public interface IEditModelOriginalEntity<TDetailsModel>
    {
        /// <summary>
        /// Gets or sets the original entity.
        /// </summary>
        /// <value>
        /// The original entity.
        /// </value>
        TDetailsModel Original { get; set; }
    }
}

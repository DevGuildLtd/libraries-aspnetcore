using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Represents concurrency-checked view model.
    /// </summary>
    public interface IEditModelConcurrencyCheck
    {
        /// <summary>
        /// Gets or sets the concurrency token.
        /// </summary>
        /// <value>
        /// The concurrency token.
        /// </value>
        Guid ConcurrencyToken { get; set; }
    }
}

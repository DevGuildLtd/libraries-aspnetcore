using System;

namespace DevGuild.AspNetCore.ObjectModel
{
    /// <summary>
    /// Provides an ability to specify concurrency token in the domain model object.
    /// </summary>
    public interface IConcurrencyCheck
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

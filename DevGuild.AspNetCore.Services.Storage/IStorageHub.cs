using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Storage
{
    /// <summary>
    /// Provides access to storage containers.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IStorageHub : IDisposable
    {
        /// <summary>
        /// Gets the named storage container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>A storage container.</returns>
        IStorageContainer GetContainer(String containerName);
    }
}

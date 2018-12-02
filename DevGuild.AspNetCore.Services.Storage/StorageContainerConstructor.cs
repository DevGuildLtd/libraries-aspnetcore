using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Storage
{
    /// <summary>
    /// Represents storage container constructor.
    /// </summary>
    public abstract class StorageContainerConstructor
    {
        /// <summary>
        /// Creates an instance of a storage container.
        /// </summary>
        /// <returns>A storage container.</returns>
        public abstract IStorageContainer Create();
    }
}

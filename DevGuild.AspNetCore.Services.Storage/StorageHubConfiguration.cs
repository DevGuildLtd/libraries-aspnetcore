using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Storage
{
    /// <summary>
    /// Represents storage hub configuration.
    /// </summary>
    public class StorageHubConfiguration
    {
        private readonly Dictionary<String, StorageContainerConstructor> constructors = new Dictionary<String, StorageContainerConstructor>();

        /// <summary>
        /// Gets the container constructor by its name.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>A container constructor</returns>
        public StorageContainerConstructor GetConstructor(String containerName)
        {
            return this.constructors[containerName];
        }

        /// <summary>
        /// Registers the container constructor.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="constructor">The container constructor.</param>
        public void RegisterConstructor(String containerName, StorageContainerConstructor constructor)
        {
            if (this.constructors.ContainsKey(containerName))
            {
                this.constructors[containerName] = constructor;
            }
            else
            {
                this.constructors.Add(containerName, constructor);
            }
        }
    }
}

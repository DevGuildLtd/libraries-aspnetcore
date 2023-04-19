using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Storage
{
    /// <summary>
    /// Represents implementation of the storage hub.
    /// </summary>
    /// <seealso cref="IStorageHub" />
    public sealed class StorageHub : IStorageHub
    {
        private readonly StorageHubConfiguration configuration;
        private readonly Dictionary<String, IStorageContainer> containers = new Dictionary<String, IStorageContainer>();
        private Boolean isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageHub"/> class.
        /// </summary>
        /// <param name="configuration">The storage hub configuration.</param>
        public StorageHub(StorageHubConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public IStorageContainer GetContainer(String containerName)
        {
            if (!this.containers.TryGetValue(containerName, out var container))
            {
                container = this.configuration.GetConstructor(containerName).Create();
                this.containers.Add(containerName, container);
            }

            return container;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }

            foreach (var container in this.containers.Values)
            {
                container.Dispose();
            }

            this.isDisposed = true;
        }
    }
}

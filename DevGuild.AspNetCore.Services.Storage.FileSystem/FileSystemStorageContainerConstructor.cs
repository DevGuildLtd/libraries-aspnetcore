using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Storage.FileSystem
{
    /// <summary>
    /// Represents file system storage container constructor.
    /// </summary>
    /// <seealso cref="StorageContainerConstructor" />
    public class FileSystemStorageContainerConstructor : StorageContainerConstructor
    {
        /// <summary>
        /// Gets or sets the base directory.
        /// </summary>
        /// <value>
        /// The base directory.
        /// </value>
        public String BaseDirectory { get; set; }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        public String BaseUrl { get; set; }

        /// <inheritdoc />
        public override IStorageContainer Create()
        {
            return new FileSystemStorageContainer(this.BaseDirectory, this.BaseUrl);
        }
    }
}

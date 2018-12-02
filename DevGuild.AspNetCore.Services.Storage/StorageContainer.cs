using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Storage
{
    /// <summary>
    /// Represents base implementation of the storage container.
    /// </summary>
    /// <seealso cref="IStorageContainer" />
    public abstract class StorageContainer : IStorageContainer
    {
        /// <summary>
        /// Finalizes an instance of the <see cref="StorageContainer"/> class.
        /// </summary>
        ~StorageContainer()
        {
            this.Dispose(false);
        }

        /// <inheritdoc />
        public abstract Task StoreFileAsync(String fileName, Stream fileStream);

        /// <inheritdoc />
        public abstract String GetFileUrl(String fileName);

        /// <inheritdoc />
        public abstract Task<String> GetFileUrlAsync(String fileName);

        /// <inheritdoc />
        public abstract Task<Stream> GetFileContentAsync(String fileName);

        /// <inheritdoc />
        public abstract Task DeleteFileAsync(String fileName);

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Validates and normalize the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="useBackSlash">if set to <c>true</c> back slashes will be used as path separator.</param>
        /// <returns>Normalized file name.</returns>
        /// <exception cref="System.ArgumentException">
        /// File name must not start or end with a '/' or '\\'
        /// or
        /// No part of file name could be '.' or '..'
        /// or
        /// No part of file name could be empty
        /// </exception>
        protected String ValidateAndNormalizeFileName(String fileName, Boolean useBackSlash)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"{nameof(fileName)} is null or empty", nameof(fileName));
            }

            if (fileName.StartsWith("/") || fileName.EndsWith("/") || fileName.StartsWith("\\") || fileName.EndsWith("\\"))
            {
                throw new ArgumentException("File name must not start or end with a '/' or '\\'");
            }

            var parts = fileName.Split(new[] { '/', '\\' }, StringSplitOptions.None);
            if (parts.Any(x => x == "." || x == ".."))
            {
                throw new ArgumentException("No part of file name could be '.' or '..'");
            }

            if (parts.Any(String.IsNullOrEmpty))
            {
                throw new ArgumentException("No part of file name could be empty");
            }

            return useBackSlash ? fileName.Replace("/", "\\") : fileName.Replace("\\", "/");
        }

        /// <summary>
        /// When overriden in a derived class, performs freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> managed resources should be disposed.</param>
        protected virtual void Dispose(Boolean disposing)
        {
        }
    }
}

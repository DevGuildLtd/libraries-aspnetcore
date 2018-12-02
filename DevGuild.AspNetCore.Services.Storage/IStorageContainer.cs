using System;
using System.IO;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Storage
{
    /// <summary>
    /// Provides ability to store, retrieve and delete files and to generate public urls to access theses files.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IStorageContainer : IDisposable
    {
        /// <summary>
        /// Asynchronously stores the file in the container.
        /// </summary>
        /// <param name="fileName">Name of the file relative to the container.</param>
        /// <param name="fileStream">The file stream containing file content.</param>
        /// <returns>A task that represents this operation.</returns>
        /// <exception cref="FileAlreadyExistsException">File already exists.</exception>
        Task StoreFileAsync(String fileName, Stream fileStream);

        /// <summary>
        /// Gets the public url of the file.
        /// </summary>
        /// <param name="fileName">Name of the file relative to the container.</param>
        /// <returns>File's public url.</returns>
        /// <remarks>This method does not check whether the file exists or not.</remarks>
        String GetFileUrl(String fileName);

        /// <summary>
        /// Asynchronously gets the public url of the file.
        /// </summary>
        /// <param name="fileName">Name of the file relative to the container.</param>
        /// <returns>A task that represents this operation and contains public url as a result.</returns>
        /// <remarks>This method does not check whether the file exists or not.</remarks>
        Task<String> GetFileUrlAsync(String fileName);

        /// <summary>
        /// Asynchronously gets the file content as a stream.
        /// </summary>
        /// <param name="fileName">Name of the file relative to the container.</param>
        /// <returns>A task that represents this operation and contains content stream as a result.</returns>
        /// <exception cref="FileNotFoundException">File not found.</exception>
        Task<Stream> GetFileContentAsync(String fileName);

        /// <summary>
        /// Asynchronously deletes the file.
        /// </summary>
        /// <param name="fileName">Name of the file relative to the container.</param>
        /// <returns>A task that represents this operation.</returns>
        /// <remarks>This method will not throw exception file does not exists.</remarks>
        Task DeleteFileAsync(String fileName);
    }
}

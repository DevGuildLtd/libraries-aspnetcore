using System;
using System.IO;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Storage.FileSystem
{
    /// <summary>
    /// Represents file system storage container.
    /// </summary>
    /// <seealso cref="StorageContainer" />
    public sealed class FileSystemStorageContainer : StorageContainer
    {
        private readonly String baseDirectory;
        private readonly String baseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemStorageContainer"/> class.
        /// </summary>
        /// <param name="baseDirectory">The base directory.</param>
        /// <param name="baseUrl">The base URL.</param>
        public FileSystemStorageContainer(String baseDirectory, String baseUrl)
        {
            if (baseDirectory == null)
            {
                throw new ArgumentNullException($"{nameof(baseDirectory)} is null", nameof(baseDirectory));
            }

            if (baseUrl == null)
            {
                throw new ArgumentNullException($"{nameof(baseUrl)} is null", nameof(baseUrl));
            }

            this.baseDirectory = baseDirectory;
            this.baseUrl = baseUrl.EndsWith("/") ? baseUrl : $"{baseUrl}/";
        }

        /// <inheritdoc />
        public override async Task StoreFileAsync(String fileName, Stream stream)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            if (stream == null)
            {
                throw new ArgumentNullException($"{nameof(stream)} is null", nameof(stream));
            }

            if (stream.Position > 0)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            fileName = this.ValidateAndNormalizeFileName(fileName, this.IsBackSlashUsedAsDirectorySeparator());
            var fullPath = Path.Combine(this.baseDirectory, fileName);
            var containingDir = Path.GetDirectoryName(fullPath);
            if (containingDir != null && !Directory.Exists(containingDir))
            {
                Directory.CreateDirectory(containingDir);
            }

            if (File.Exists(fullPath))
            {
                throw new FileAlreadyExistsException();
            }

            using (var fileStream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                await stream.CopyToAsync(fileStream);
            }
        }

        /// <inheritdoc />
        public override String GetFileUrl(String fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            var localUrl = this.ValidateAndNormalizeFileName(fileName, false);
            return String.Concat(this.baseUrl, localUrl);
        }

        /// <inheritdoc />
        public override Task<String> GetFileUrlAsync(String fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            var localUrl = this.ValidateAndNormalizeFileName(fileName, false);
            return Task.FromResult(String.Concat(this.baseUrl, localUrl));
        }

        /// <inheritdoc />
        public override async Task<Stream> GetFileContentAsync(String fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException($"{nameof(fileName)} is null", nameof(fileName));
            }

            fileName = this.ValidateAndNormalizeFileName(fileName, this.IsBackSlashUsedAsDirectorySeparator());
            var fullPath = Path.Combine(this.baseDirectory, fileName);

            try
            {
                using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var memoryStream = new MemoryStream(new Byte[fileStream.Length]);
                    await fileStream.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return memoryStream;
                }
            }
            catch (DirectoryNotFoundException)
            {
                // FileNotFoundException should be thrown if containing directory was not found.
                throw new FileNotFoundException();
            }
        }

        /// <inheritdoc />
        public override Task DeleteFileAsync(String fileName)
        {
            var fullPath = Path.Combine(this.baseDirectory, fileName);
            try
            {
                File.Delete(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                // DeleteFile should not fail if containing directory does not exists.
            }

            return Task.FromResult(0);
        }

        private Boolean IsBackSlashUsedAsDirectorySeparator()
        {
            switch (Path.DirectorySeparatorChar)
            {
                case '\\': return true;
                case '/': return false;
                default: throw new InvalidOperationException("Unexpected directory separator configured for this system");
            }
        }
    }
}

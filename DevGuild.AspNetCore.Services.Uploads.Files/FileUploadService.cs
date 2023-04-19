using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Contracts;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.Storage;
using DevGuild.AspNetCore.Services.Uploads.Files.Configuration;
using DevGuild.AspNetCore.Services.Uploads.Files.Models;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Services.Uploads.Files
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IStorageHub storageHub;
        private readonly FileUploadConfigurationsManager configurationsManager;
        private readonly IEntityStore<UploadedFile> filesStore;

        public FileUploadService(IStorageHub storageHub, IRepository repository, FileUploadConfigurationsManager configurationsManager)
        {
            this.storageHub = storageHub;
            this.configurationsManager = configurationsManager;
            this.filesStore = repository.GetEntityStore<UploadedFile>();
        }

        public async Task<UploadedFile> GetUploadedFileAsync(Guid id)
        {
            var file = await this.filesStore.Query().SingleOrDefaultAsync(x => x.Id == id);
            return file;
        }

        public async Task<FileUploadResult> CreateUploadedFileAsync(String configuration, String fileName, Stream fileStream)
        {
            Ensure.Argument.NotNullOrEmpty(configuration, nameof(configuration));
            Ensure.Argument.NotNullOrEmpty(fileName, nameof(fileName));
            Ensure.Argument.NotNull(fileStream, nameof(fileStream));

            var configurationEntry = this.configurationsManager.GetConfiguration(configuration);
            if (configurationEntry == null)
            {
                return FileUploadResult.Fail("ConfigurationNotFound");
            }

            var container = this.storageHub.GetContainer(configurationEntry.Container);
            if (container == null)
            {
                return FileUploadResult.Fail("ConfigurationInvalid");
            }

            var fileExtension = Path.GetExtension(fileName).Substring(1).ToLowerInvariant();
            if (!configurationEntry.AllowedFormats.Contains(fileExtension, StringComparer.InvariantCultureIgnoreCase))
            {
                return FileUploadResult.Fail("ForbiddenFileFormat");
            }

            using var hasher = SHA512.Create();
            using var memoryStream = new MemoryStream();

            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var hashBytes = await hasher.ComputeHashAsync(memoryStream);
            memoryStream.Position = 0;

            var uploadedFile = new UploadedFile(
                originalName: Path.GetFileNameWithoutExtension(fileName),
                extension: fileExtension,
                size: memoryStream.Length,
                hash: Convert.ToBase64String(hashBytes),
                configuration: configurationEntry,
                customData: Array.Empty<UploadedFileCustomData>());

            var path = uploadedFile.GetContainerPath();
            await container.StoreFileAsync(path, memoryStream);
            await this.filesStore.InsertAsync(uploadedFile);
            return FileUploadResult.Succeed(uploadedFile);
        }
    }
}

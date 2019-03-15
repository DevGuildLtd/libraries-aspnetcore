using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Storage;

namespace DevGuild.AspNetCore.Testing.Storage
{
    public class InMemoryStorageContainer : StorageContainer
    {
        private readonly String baseUrl;
        private readonly Dictionary<String, Byte[]> inMemoryFiles;

        public InMemoryStorageContainer(String baseUrl, Dictionary<String, Byte[]> inMemoryFiles)
        {
            this.baseUrl = baseUrl.EndsWith("/") ? baseUrl : $"{baseUrl}/";
            this.inMemoryFiles = inMemoryFiles;
        }

        public override async Task StoreFileAsync(String fileName, Stream fileStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                if (fileStream.CanSeek && fileStream.Position > 0)
                {
                    fileStream.Position = 0;
                }

                await fileStream.CopyToAsync(memoryStream);

                var localUrl = this.ValidateAndNormalizeFileName(fileName, false);
                this.inMemoryFiles[localUrl] = memoryStream.ToArray();
            }
        }

        public override String GetFileUrl(String fileName)
        {
            var localUrl = this.ValidateAndNormalizeFileName(fileName, false);
            return String.Concat(this.baseUrl, localUrl);
        }

        public override Task<String> GetFileUrlAsync(String fileName)
        {
            var localUrl = this.ValidateAndNormalizeFileName(fileName, false);
            return Task.FromResult(String.Concat(this.baseUrl, localUrl));
        }

        public override async Task<Stream> GetFileContentAsync(String fileName)
        {
            var localUrl = this.ValidateAndNormalizeFileName(fileName, false);
            if (this.inMemoryFiles.TryGetValue(localUrl, out var bytes))
            {
                var stream = new MemoryStream();
                await stream.WriteAsync(bytes);
                stream.Position = 0;
                return stream;
            }

            throw new FileNotFoundException();
        }

        public override Task DeleteFileAsync(String fileName)
        {
            var localUrl = this.ValidateAndNormalizeFileName(fileName, false);
            this.inMemoryFiles.Remove(localUrl);
            return Task.CompletedTask;
        }
    }
}

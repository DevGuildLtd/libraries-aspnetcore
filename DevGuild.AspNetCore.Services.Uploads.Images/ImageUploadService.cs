using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.Permissions;
using DevGuild.AspNetCore.Services.Storage;
using DevGuild.AspNetCore.Services.Uploads.Images.Configuration;
using DevGuild.AspNetCore.Services.Uploads.Images.Models;
using DevGuild.AspNetCore.Services.Uploads.Images.Processing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace DevGuild.AspNetCore.Services.Uploads.Images
{
    /// <summary>
    /// Represents implementation of the image upload services.
    /// </summary>
    /// <seealso cref="IImageUploadService" />
    public class ImageUploadService : IImageUploadService
    {
        private static readonly Dictionary<String, SKEncodedImageFormat> ImageFormats = new Dictionary<String, SKEncodedImageFormat>
        {
            ["jpg"] = SKEncodedImageFormat.Jpeg,
            ["jpeg"] = SKEncodedImageFormat.Jpeg,
            ["png"] = SKEncodedImageFormat.Png,
            ["gif"] = SKEncodedImageFormat.Gif,
        };

        private readonly IStorageHub storageHub;
        private readonly IPermissionsHub permissionsHub;
        private readonly ImageUploadConfigurationsManager configurationsManager;
        private readonly IEntityStore<UploadedImage> imagesStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadService"/> class.
        /// </summary>
        /// <param name="storageHub">The storage hub.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="permissionsHub">The permissions hub.</param>
        /// <param name="configurationsManager">The configurations manager.</param>
        public ImageUploadService(IStorageHub storageHub, IRepository repository, IPermissionsHub permissionsHub, ImageUploadConfigurationsManager configurationsManager)
        {
            this.storageHub = storageHub;
            this.permissionsHub = permissionsHub;
            this.configurationsManager = configurationsManager;

            this.imagesStore = repository.GetEntityStore<UploadedImage>();
        }

        /// <inheritdoc />
        public async Task<UploadedImage> GetUploadedImageAsync(Guid id)
        {
            var image = await this.imagesStore.Query().SingleOrDefaultAsync(x => x.Id == id);
            return image ?? null;
        }

        /// <inheritdoc />
        public Task<String> GetImageUrlAsync(Guid? imageId, String variation)
        {
            async Task<String> AsyncImplementation()
            {
                var image = await this.imagesStore.Query().SingleOrDefaultAsync(x => x.Id == imageId);
                var containerPath = image.GetContainerPath(variation);
                var container = this.storageHub.GetContainer(image.Container);
                var fileUrl = await container.GetFileUrlAsync(containerPath);

                return fileUrl;
            }

            return imageId == null ? Task.FromResult(this.configurationsManager.NoImageUrl) : AsyncImplementation();
        }

        /// <inheritdoc />
        public Task<String> GetImageUrlAsync(UploadedImage image, String variation)
        {
            async Task<String> AsyncImplementation()
            {
                var containerPath = image.GetContainerPath(variation);
                var container = this.storageHub.GetContainer(image.Container);
                var fileUrl = await container.GetFileUrlAsync(containerPath);

                return fileUrl;
            }

            return image == null ? Task.FromResult(this.configurationsManager.NoImageUrl) : AsyncImplementation();
        }

        /// <inheritdoc />
        public String GetImageUrl(UploadedImage image, String variation)
        {
            if (image == null)
            {
                return this.configurationsManager.NoImageUrl;
            }

            var containerPath = image.GetContainerPath(variation);
            var container = this.storageHub.GetContainer(image.Container);
            var fileUrl = container.GetFileUrl(containerPath);

            return fileUrl;
        }

        /// <inheritdoc />
        public Task<ImageUploadResult> ProcessImageUploadAsync(String configuration, IFormFile file)
        {
            if (String.IsNullOrEmpty(configuration))
            {
                throw new ArgumentException($"{nameof(configuration)} is null or empty", nameof(configuration));
            }

            if (file == null)
            {
                throw new ArgumentNullException($"{nameof(file)} is null", nameof(file));
            }

            return this.CreateUploadedImageAsync(configuration, file.FileName, file.OpenReadStream());
        }

        /// <inheritdoc />
        public async Task<ImageUploadResult> CreateUploadedImageAsync(String configuration, String imageName, Stream imageStream)
        {
            if (String.IsNullOrEmpty(configuration))
            {
                throw new ArgumentException($"{nameof(configuration)} is null or empty", nameof(configuration));
            }

            if (String.IsNullOrEmpty(imageName))
            {
                throw new ArgumentException($"{nameof(imageName)} is null or empty", nameof(imageName));
            }

            if (imageStream == null)
            {
                throw new ArgumentNullException($"{nameof(imageStream)} is null", nameof(imageStream));
            }

            var configurationEntry = this.configurationsManager.GetConfiguration(configuration);
            if (configurationEntry == null)
            {
                return ImageUploadResult.Fail("ConfigurationNotFound");
            }

            var container = this.storageHub.GetContainer(configurationEntry.Container);
            if (container == null)
            {
                return ImageUploadResult.Fail("ConfigurationInvalid");
            }

            var imageFormat = Path.GetExtension(imageName).Substring(1).ToLowerInvariant();
            if (!configurationEntry.AllowedFormats.Contains(imageFormat))
            {
                return ImageUploadResult.Fail("ForbiddenFileFormat");
            }

            using (var originalBitmap = SKBitmap.Decode(imageStream))
            {
                var variationsStreams = new Dictionary<String, Tuple<String, Stream>>();
                foreach (var variation in configurationEntry.Variations)
                {
                    var processors = ImageProcessor.CreateProcessors(variation.Processors);
                    var metadata = new Dictionary<String, Object>
                    {
                        ["TargetFormat"] = imageFormat
                    };

                    var image = SKImage.FromBitmap(originalBitmap);
                    foreach (var processor in processors)
                    {
                        image = await processor.ProcessImageAsync(image, metadata);
                    }

                    var targetFormat = ((String)metadata["TargetFormat"]).ToLowerInvariant();
                    var stream = ImageUploadService.SaveImage(image, metadata);
                    variationsStreams.Add(variation.Id, Tuple.Create(targetFormat, stream));
                }

                var originalName = Path.GetFileNameWithoutExtension(imageName);
                var uploadedImage = new UploadedImage(
                    configuration: configurationEntry,
                    originalName: originalName,
                    variations: variationsStreams.Select(x => new UploadedImageVariation(x.Key, x.Value.Item1)),
                    customData: new UploadedImageCustomData[0]);

                foreach (var variation in variationsStreams)
                {
                    var path = uploadedImage.GetContainerPath(variation.Key);
                    await container.StoreFileAsync(path, variation.Value.Item2);
                }

                await this.imagesStore.InsertAsync(uploadedImage);
                return ImageUploadResult.Succeed(uploadedImage);
            }
        }

        private static Stream SaveImage(SKImage image, Dictionary<String, Object> metadata)
        {
            var targetFormat = ((String)metadata["TargetFormat"]).ToLowerInvariant();
            var targetQuality = 100;

            if (metadata.TryGetValue($"TargetQuality_{targetFormat}", out var qualityEntry) && qualityEntry is Int32 quality)
            {
                targetQuality = quality;
            }

            var format = ImageUploadService.ImageFormats[targetFormat];
            var stream = new MemoryStream();

            var data = image.Encode(format, targetQuality);
            return data.AsStream(streamDisposesData: true);
        }
    }
}

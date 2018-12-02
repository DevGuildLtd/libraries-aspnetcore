using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Services.Uploads.Images.Configuration;
using Newtonsoft.Json;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Models
{
    /// <summary>
    /// Represents uploaded image.
    /// </summary>
    public class UploadedImage
    {
        private List<UploadedImageVariation> variations;
        private List<UploadedImageCustomData> custmData;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadedImage"/> class.
        /// </summary>
        public UploadedImage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadedImage"/> class.
        /// </summary>
        /// <param name="originalName">Name of the original.</param>
        /// <param name="configuration">The image configuration.</param>
        /// <param name="variations">The image variations.</param>
        /// <param name="customData">The image custom data.</param>
        public UploadedImage(
            String originalName,
            ImageUploadConfiguration configuration,
            IEnumerable<UploadedImageVariation> variations,
            IEnumerable<UploadedImageCustomData> customData)
        {
            this.Id = Guid.NewGuid();
            this.OriginalName = originalName;
            this.Configuration = configuration.Id;
            this.ConfigurationVersion = configuration.Version;
            this.Container = configuration.Container;
            this.ContainerPrefix = configuration.ContainerPrefix;
            this.ReferenceCount = 0;
            this.ReferenceCountLastUpdated = DateTime.UtcNow;
            this.SetVariations(variations);
            this.SetCustomData(customData);
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the original.
        /// </summary>
        /// <value>
        /// The name of the original.
        /// </value>
        [Required]
        [MaxLength(256)]
        public String OriginalName { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        [Required]
        [MaxLength(100)]
        public String Configuration { get; set; }

        /// <summary>
        /// Gets or sets the configuration version.
        /// </summary>
        /// <value>
        /// The configuration version.
        /// </value>
        public Int32 ConfigurationVersion { get; set; }

        /// <summary>
        /// Gets or sets the container name.
        /// </summary>
        /// <value>
        /// The container name.
        /// </value>
        [Required]
        [MaxLength(100)]
        public String Container { get; set; }

        /// <summary>
        /// Gets or sets the container prefix.
        /// </summary>
        /// <value>
        /// The container prefix.
        /// </value>
        [Required]
        [MaxLength(100)]
        public String ContainerPrefix { get; set; }

        /// <summary>
        /// Gets or sets the reference count.
        /// </summary>
        /// <value>
        /// The reference count.
        /// </value>
        public Int32 ReferenceCount { get; set; }

        /// <summary>
        /// Gets or sets the date the reference count was last updated.
        /// </summary>
        /// <value>
        /// The date the reference count was last updated.
        /// </value>
        public DateTime ReferenceCountLastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the serialized variations collection.
        /// </summary>
        /// <value>
        /// The serialized variations collection.
        /// </value>
        [Column("Variations")]
        public String VariationsContent { get; set; }

        /// <summary>
        /// Gets the image variations.
        /// </summary>
        /// <value>
        /// The image variations.
        /// </value>
        [NotMapped]
        public IReadOnlyCollection<UploadedImageVariation> Variations
        {
            get
            {
                if (this.variations == null)
                {
                    this.variations = this.VariationsContent != null
                        ? JsonConvert.DeserializeObject<List<UploadedImageVariation>>(this.VariationsContent)
                        : new List<UploadedImageVariation>();
                }

                return this.variations.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets or sets the serialized custom data.
        /// </summary>
        /// <value>
        /// The serialized custom data.
        /// </value>
        [Column("CustomData")]
        public String CustomDataContent { get; set; }

        /// <summary>
        /// Gets the image custom data.
        /// </summary>
        /// <value>
        /// The image custom data.
        /// </value>
        [NotMapped]
        public IReadOnlyCollection<UploadedImageCustomData> CustomData
        {
            get
            {
                if (this.custmData == null)
                {
                    this.custmData = this.CustomDataContent != null
                        ? JsonConvert.DeserializeObject<List<UploadedImageCustomData>>(this.CustomDataContent)
                        : new List<UploadedImageCustomData>();
                }

                return this.custmData.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the container path.
        /// </summary>
        /// <param name="variationId">The variation identifier.</param>
        /// <returns>Path to the image relative to the container root.</returns>
        public String GetContainerPath(String variationId)
        {
            if (String.IsNullOrEmpty(variationId))
            {
                throw new ArgumentException($"{nameof(variationId)} is null or empty", nameof(variationId));
            }

            if (this.Variations.All(x => x.Id != variationId))
            {
                throw new ArgumentException($"Variation not found");
            }

            var variation = this.Variations.Single(x => x.Id == variationId);

            var pathBuilder = new StringBuilder();
            if (!String.IsNullOrEmpty(this.ContainerPrefix))
            {
                pathBuilder.Append($"{this.ContainerPrefix}/");
            }

            pathBuilder.Append($"{variation.Id}/{this.Id:D}.{variation.Extension}");
            return pathBuilder.ToString();
        }

        public String GetCustomDataValue(String key)
        {
            var entry = this.CustomData.FirstOrDefault(x => x.Key == key);
            return entry?.Value;
        }

        public void SetCustomDataValue(String key, String value)
        {
            var customDataList = this.CustomData.ToList();
            for (var i = 0; i < customDataList.Count; i++)
            {
                if (customDataList[i].Key == key)
                {
                    customDataList[i] = new UploadedImageCustomData(key, value);
                    this.SetCustomData(customDataList);
                    return;
                }
            }

            customDataList.Add(new UploadedImageCustomData(key, value));
            this.SetCustomData(customDataList);
        }

        private void SetVariations(IEnumerable<UploadedImageVariation> variations)
        {
            this.variations = variations.ToList();
            this.VariationsContent = JsonConvert.SerializeObject(this.variations);
        }

        private void SetCustomData(IEnumerable<UploadedImageCustomData> customData)
        {
            this.custmData = customData.ToList();
            this.CustomDataContent = JsonConvert.SerializeObject(this.custmData);
        }
    }
}

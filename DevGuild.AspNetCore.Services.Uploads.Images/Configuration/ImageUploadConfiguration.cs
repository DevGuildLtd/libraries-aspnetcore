using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents image upload configuration.
    /// </summary>
    public sealed class ImageUploadConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadConfiguration"/> class.
        /// </summary>
        /// <param name="id">The configuration identifier.</param>
        /// <param name="version">The configuration version.</param>
        /// <param name="container">The container name.</param>
        /// <param name="containerPrefix">The container prefix.</param>
        /// <param name="allowedFormats">The allowed formats.</param>
        /// <param name="variations">The image variations.</param>
        public ImageUploadConfiguration(String id, Int32 version, String container, String containerPrefix, IEnumerable<String> allowedFormats, IEnumerable<ImageUploadVariationConfiguration> variations)
        {
            this.Id = id;
            this.Version = version;
            this.Container = container;
            this.ContainerPrefix = containerPrefix;
            this.AllowedFormats = allowedFormats.ToList().AsReadOnly();
            this.Variations = variations.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the configuration identifier.
        /// </summary>
        /// <value>
        /// The configuration identifier.
        /// </value>
        public String Id { get; }

        /// <summary>
        /// Gets the configuration version.
        /// </summary>
        /// <value>
        /// The configuration version.
        /// </value>
        public Int32 Version { get; }

        /// <summary>
        /// Gets the container name.
        /// </summary>
        /// <value>
        /// The container name.
        /// </value>
        public String Container { get; }

        /// <summary>
        /// Gets the container prefix.
        /// </summary>
        /// <value>
        /// The container prefix.
        /// </value>
        public String ContainerPrefix { get; }

        /// <summary>
        /// Gets the allowed formats.
        /// </summary>
        /// <value>
        /// The allowed formats.
        /// </value>
        public IReadOnlyCollection<String> AllowedFormats { get; }

        /// <summary>
        /// Gets the image variations.
        /// </summary>
        /// <value>
        /// The iamge variations.
        /// </value>
        public IReadOnlyCollection<ImageUploadVariationConfiguration> Variations { get; }
    }
}

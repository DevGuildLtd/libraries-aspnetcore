using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents image upload configuration builder.
    /// </summary>
    public class ImageUploadConfigurationBuilder
    {
        private readonly String identifier;
        private readonly Int32 version;
        private readonly List<String> allowedFormats = new List<String>();
        private readonly List<ImageUploadVariationConfiguration> variations = new List<ImageUploadVariationConfiguration>();
        private String containerName;
        private String containerPrefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadConfigurationBuilder"/> class.
        /// </summary>
        /// <param name="identifier">The configuration identifier.</param>
        /// <param name="version">The configuration version.</param>
        public ImageUploadConfigurationBuilder(String identifier, Int32 version)
        {
            if (String.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException($"{nameof(identifier)} is null or empty", nameof(identifier));
            }

            this.identifier = identifier;
            this.version = version;
        }

        /// <summary>
        /// Configures storage container for the configuration.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="containerPrefix">The container prefix.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageUploadConfigurationBuilder Container(String containerName, String containerPrefix)
        {
            if (String.IsNullOrEmpty(containerName))
            {
                throw new ArgumentException($"{nameof(containerName)} is null or empty", nameof(containerName));
            }

            if (containerPrefix == null)
            {
                throw new ArgumentNullException($"{nameof(containerPrefix)} is null", nameof(containerPrefix));
            }

            this.containerName = containerName;
            this.containerPrefix = containerPrefix;
            return this;
        }

        /// <summary>
        /// Configures allowed formats for the configuration.
        /// </summary>
        /// <param name="formats">The allowed formats.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageUploadConfigurationBuilder AllowedFormats(params String[] formats)
        {
            if (formats == null)
            {
                throw new ArgumentNullException($"{nameof(formats)} is null", nameof(formats));
            }

            if (formats.Any(x => String.IsNullOrEmpty(x)))
            {
                throw new ArgumentException($"At least one of the provided formats is empty", nameof(formats));
            }

            this.allowedFormats.AddRange(formats);
            return this;
        }

        /// <summary>
        /// Configures image variations for the configuration.
        /// </summary>
        /// <param name="identifier">The variation identifier.</param>
        /// <param name="configuration">The variation configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageUploadConfigurationBuilder Variation(String identifier, Func<ImageUploadVariationConfigurationBuilder, ImageUploadVariationConfigurationBuilder> configuration)
        {
            if (String.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException($"{nameof(identifier)} is null or empty", nameof(identifier));
            }

            if (this.variations.Any(x => x.Id == identifier))
            {
                throw new ArgumentException($"Variation '{identifier}' already exists.", nameof(identifier));
            }

            var variationBuilder = new ImageUploadVariationConfigurationBuilder(identifier);
            variationBuilder = configuration.Invoke(variationBuilder);
            this.variations.Add(variationBuilder.BuildConfiguration());

            return this;
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <returns>Image upload configuration.</returns>
        public ImageUploadConfiguration BuildConfiguration()
        {
            if (String.IsNullOrEmpty(this.containerName))
            {
                throw new InvalidOperationException("Container is not configured");
            }

            if (this.containerPrefix == null)
            {
                throw new InvalidOperationException("Container is not configured");
            }

            if (this.allowedFormats.Count == 0)
            {
                throw new InvalidOperationException("No image format was allowed");
            }

            if (this.variations.Count == 0)
            {
                throw new InvalidOperationException("No variation was defined");
            }
            
            return new ImageUploadConfiguration(this.identifier, this.version, this.containerName, this.containerPrefix, this.allowedFormats, this.variations);
        }
    }
}

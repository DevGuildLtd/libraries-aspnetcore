using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Uploads.Images.Permissions;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents image upload configurations manager.
    /// </summary>
    public sealed class ImageUploadConfigurationsManager
    {
        private readonly Dictionary<String, ImageUploadConfiguration> configurations = new Dictionary<String, ImageUploadConfiguration>();
        private String noImageUrl = null;

        /// <summary>
        /// Gets or sets the URL of the image that is served when no image is available.
        /// </summary>
        /// <value>
        /// The URL of the image that is served when no image is available.
        /// </value>
        /// <exception cref="InvalidOperationException">NoImageUrl is not configured</exception>
        public String NoImageUrl
        {
            get
            {
                if (this.noImageUrl == null)
                {
                    throw new InvalidOperationException("NoImageUrl is not configured");
                }

                return this.noImageUrl;
            }
            set => this.noImageUrl = value;
        }

        /// <summary>
        /// Adds the configuration.
        /// </summary>
        /// <param name="identifier">The configuration identifier.</param>
        /// <param name="version">The configuration version.</param>
        /// <param name="configuration">The configuration.</param>
        public void AddConfiguration(String identifier, Int32 version, Func<ImageUploadConfigurationBuilder, ImageUploadConfigurationBuilder> configuration)
        {
            if (String.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException($"{nameof(identifier)} is null or empty", nameof(identifier));
            }

            if (this.configurations.ContainsKey(identifier))
            {
                throw new ArgumentException($"Configuration '{identifier}' already exists.");
            }

            var builder = new ImageUploadConfigurationBuilder(identifier, version);
            builder = configuration.Invoke(builder);

            this.configurations.Add(identifier, builder.BuildConfiguration());
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="identifier">The configuration identifier.</param>
        /// <returns>Image upload configuration.</returns>
        public ImageUploadConfiguration GetConfiguration(String identifier)
        {
            if (String.IsNullOrEmpty(identifier))
            {
                throw new ArgumentException($"{nameof(identifier)} is null or empty", nameof(identifier));
            }

            return this.configurations.TryGetValue(identifier, out var configuration) ? configuration : null;
        }
    }
}

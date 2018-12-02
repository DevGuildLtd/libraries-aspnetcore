using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents image upload variation configuration builder.
    /// </summary>
    public class ImageUploadVariationConfigurationBuilder
    {
        private readonly String identifier;
        private readonly List<ImageProcessorConfiguration> processors = new List<ImageProcessorConfiguration>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadVariationConfigurationBuilder"/> class.
        /// </summary>
        /// <param name="identifier">The variation identifier.</param>
        public ImageUploadVariationConfigurationBuilder(String identifier)
        {
            this.identifier = identifier;
        }

        /// <summary>
        /// Configures scaling of the image.
        /// </summary>
        /// <param name="configuration">The scaling configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageUploadVariationConfigurationBuilder Scale(Func<ImageScalingProcessorConfigurationBuilder, ImageScalingProcessorConfigurationBuilder> configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException($"{nameof(configuration)} is null", nameof(configuration));
            }

            var builder = new ImageScalingProcessorConfigurationBuilder();
            builder = configuration.Invoke(builder);

            this.processors.Add(builder.BuildConfiguration());
            return this;
        }

        /// <summary>
        /// Configures converting of the image to specific format.
        /// </summary>
        /// <param name="targetFormat">The target format.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageUploadVariationConfigurationBuilder Convert(String targetFormat)
        {
            if (String.IsNullOrEmpty(targetFormat))
            {
                throw new ArgumentException($"{nameof(targetFormat)} is null or empty", nameof(targetFormat));
            }

            var convert = new ImageConvertProcessorConfiguration(targetFormat, null);
            this.processors.Add(convert);

            return this;
        }

        /// <summary>
        /// Configures converting of the image to specific format and quality.
        /// </summary>
        /// <param name="targetFormat">The target format.</param>
        /// <param name="targetQuality">The target quality.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageUploadVariationConfigurationBuilder Convert(String targetFormat, Int32 targetQuality)
        {
            if (String.IsNullOrEmpty(targetFormat))
            {
                throw new ArgumentException($"{nameof(targetFormat)} is null or empty", nameof(targetFormat));
            }

            var convert = new ImageConvertProcessorConfiguration(targetFormat, targetQuality);
            this.processors.Add(convert);

            return this;
        }

        /// <summary>
        /// Configures image quality.
        /// </summary>
        /// <param name="configuration">The quality configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageUploadVariationConfigurationBuilder Quality(Func<ImageQualityProcessorConfigurationBuilder, ImageQualityProcessorConfigurationBuilder> configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException($"{nameof(configuration)} is null", nameof(configuration));
            }

            var builder = new ImageQualityProcessorConfigurationBuilder();
            builder = configuration.Invoke(builder);
            this.processors.Add(builder.BuildConfiguration());

            return this;
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <returns>Image upload variation configuration.</returns>
        public ImageUploadVariationConfiguration BuildConfiguration()
        {
            return new ImageUploadVariationConfiguration(this.identifier, this.processors);
        }
    }
}

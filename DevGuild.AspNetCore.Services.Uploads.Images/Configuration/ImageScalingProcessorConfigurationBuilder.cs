using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents scaling image processor configuration builder.
    /// </summary>
    public class ImageScalingProcessorConfigurationBuilder
    {
        private ImageScalingProcessorConfiguration configuration;

        /// <summary>
        /// Scales image to the maximum width and height.
        /// </summary>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="maxHeight">The maximum height.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageScalingProcessorConfigurationBuilder WidthAndHeight(Int32 maxWidth, Int32 maxHeight)
        {
            if (this.configuration != null)
            {
                throw new InvalidOperationException("Already configured");
            }

            this.configuration = new ImageWidthHeightScalingProcessorConfiguration(maxWidth, maxHeight);
            return this;
        }

        /// <summary>
        /// Configures custom scaling image processor configuration.
        /// </summary>
        /// <param name="configuration">The custom configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageScalingProcessorConfigurationBuilder Custom(ImageScalingProcessorConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException($"{nameof(configuration)} is null", nameof(configuration));
            }

            if (this.configuration != null)
            {
                throw new InvalidOperationException("Already configured");
            }

            this.configuration = configuration;
            return this;
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <returns>Scaling image processor configuration.</returns>
        public ImageScalingProcessorConfiguration BuildConfiguration()
        {
            if (this.configuration == null)
            {
                throw new InvalidOperationException("Configuration is not set");
            }

            return this.configuration;
        }
    }
}

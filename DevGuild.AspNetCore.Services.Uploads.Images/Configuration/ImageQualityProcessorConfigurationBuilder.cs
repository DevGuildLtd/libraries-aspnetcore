using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents quality image processor configuration builder.
    /// </summary>
    public class ImageQualityProcessorConfigurationBuilder
    {
        private readonly Dictionary<String, Int32?> qualities = new Dictionary<String, Int32?>();

        /// <summary>
        /// Sets quality for the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        /// <returns>An instance of this builder.</returns>
        public ImageQualityProcessorConfigurationBuilder For(String format, Int32? quality)
        {
            if (String.IsNullOrEmpty(format))
            {
                throw new ArgumentException($"{nameof(format)} is null or empty", nameof(format));
            }

            if (this.qualities.ContainsKey(format))
            {
                throw new ArgumentException($"Quality for {format} is already set", nameof(format));
            }
            
            this.qualities.Add(format, quality);
            return this;
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <returns>Quality image processor configuration.</returns>
        public ImageQualityProcessorConfiguration BuildConfiguration()
        {
            return new ImageQualityProcessorConfiguration(this.qualities);
        }
    }
}

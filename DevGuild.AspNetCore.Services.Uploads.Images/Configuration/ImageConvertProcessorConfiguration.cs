using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents convert image processor configuration.
    /// </summary>
    /// <seealso cref="ImageProcessorConfiguration" />
    public class ImageConvertProcessorConfiguration : ImageProcessorConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageConvertProcessorConfiguration"/> class.
        /// </summary>
        /// <param name="targetFormat">The target format.</param>
        /// <param name="targetQuality">The target quality.</param>
        public ImageConvertProcessorConfiguration(String targetFormat, Int32? targetQuality)
        {
            this.TargetFormat = targetFormat;
            this.TargetQuality = targetQuality;
        }

        /// <summary>
        /// Gets the target format.
        /// </summary>
        /// <value>
        /// The target format.
        /// </value>
        public String TargetFormat { get; }

        /// <summary>
        /// Gets the target quality.
        /// </summary>
        /// <value>
        /// The target quality.
        /// </value>
        public Int32? TargetQuality { get; }
    }
}

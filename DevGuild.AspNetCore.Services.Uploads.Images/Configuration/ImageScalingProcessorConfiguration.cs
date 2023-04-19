using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents base scaling image processor configuration.
    /// </summary>
    /// <seealso cref="ImageProcessorConfiguration" />
    public abstract class ImageScalingProcessorConfiguration : ImageProcessorConfiguration
    {
        /// <summary>
        /// Scales the image.
        /// </summary>
        /// <param name="originalSize">Size of the original.</param>
        /// <returns>Scaled size.</returns>
        public abstract ImageSize ScaleImage(ImageSize originalSize);
    }
}

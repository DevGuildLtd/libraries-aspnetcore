using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Uploads.Images.Configuration;
using SkiaSharp;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Processing
{
    /// <summary>
    /// Represents base image processor.
    /// </summary>
    public abstract class ImageProcessor
    {
        /// <summary>
        /// Creates the processors from the provided procesors configurations.
        /// </summary>
        /// <param name="configurations">The processors configurations.</param>
        /// <returns>Created image processors.</returns>
        public static ImageProcessor[] CreateProcessors(IEnumerable<ImageProcessorConfiguration> configurations)
        {
            return configurations.Select(ImageProcessor.CreateProcessorForConfiguration).ToArray();
        }

        /// <summary>
        /// Creates the processor from the specified configuration.
        /// </summary>
        /// <param name="configuration">The processor configuration.</param>
        /// <returns>A created processor.</returns>
        /// <exception cref="System.ArgumentException">Specified configuration is not supported.</exception>
        public static ImageProcessor CreateProcessorForConfiguration(ImageProcessorConfiguration configuration)
        {
            switch (configuration)
            {
                case ImageScalingProcessorConfiguration scaling:
                    return new ScalingImageProcessor(scaling);
                case ImageConvertProcessorConfiguration convert:
                    return new ConvertImageProcessor(convert);
                case ImageQualityProcessorConfiguration quality:
                    return new QualityImageProcessor(quality);
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Asynchronously processes the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>A task that represents the operation.</returns>
        public abstract Task<SKImage> ProcessImageAsync(SKImage image, Dictionary<String, Object> metadata);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Uploads.Images.Configuration;
using SkiaSharp;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Processing
{
    /// <summary>
    /// Represents image processor that converts image from one format to another.
    /// </summary>
    /// <seealso cref="ImageProcessor" />
    public class ConvertImageProcessor : ImageProcessor
    {
        private readonly ImageConvertProcessorConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertImageProcessor"/> class.
        /// </summary>
        /// <param name="configuration">The processor configuration.</param>
        public ConvertImageProcessor(ImageConvertProcessorConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public override Task<SKImage> ProcessImageAsync(SKImage image, Dictionary<String, Object> metadata)
        {
            metadata["TargetFormat"] = this.configuration.TargetFormat;
            metadata[$"TargetQuality_{this.configuration.TargetFormat.ToLowerInvariant()}"] = this.configuration.TargetQuality;

            return Task.FromResult(image);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Uploads.Images.Configuration;
using SkiaSharp;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Processing
{
    /// <summary>
    /// Represents image processor that changes image quality.
    /// </summary>
    /// <seealso cref="ImageProcessor" />
    public class QualityImageProcessor : ImageProcessor
    {
        private readonly ImageQualityProcessorConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="QualityImageProcessor"/> class.
        /// </summary>
        /// <param name="configuration">The processor configuration.</param>
        public QualityImageProcessor(ImageQualityProcessorConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public override Task<SKImage> ProcessImageAsync(SKImage image, Dictionary<String, Object> metadata)
        {
            foreach (var quality in this.configuration.Qualities)
            {
                metadata[$"TargetQuality_{quality.Key.ToLowerInvariant()}"] = quality.Value;
            }

            return Task.FromResult(image);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Uploads.Images.Configuration;
using SkiaSharp;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Processing
{
    /// <summary>
    /// Represents image processor that scales images.
    /// </summary>
    /// <seealso cref="ImageProcessor" />
    public class ScalingImageProcessor : ImageProcessor
    {
        private readonly ImageScalingProcessorConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScalingImageProcessor"/> class.
        /// </summary>
        /// <param name="configuration">The processor configuration.</param>
        public ScalingImageProcessor(ImageScalingProcessorConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public override Task<SKImage> ProcessImageAsync(SKImage image, Dictionary<String, Object> metadata)
        {
            var oldSize = new ImageSize(image.Width, image.Height);
            var newSize = this.configuration.ScaleImage(oldSize);

            if (newSize.Equals(oldSize))
            {
                return Task.FromResult(image);
            }

            using (var bitmap = SKBitmap.FromImage(image))
            using (var newBitmap = bitmap.Resize(new SKImageInfo(newSize.Width, newSize.Height), SKBitmapResizeMethod.Lanczos3))
            {
                image.Dispose();
                return Task.FromResult(SKImage.FromBitmap(newBitmap));
            }
        }
    }
}

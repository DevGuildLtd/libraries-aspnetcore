using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents width and height scaling image processor configuration.
    /// </summary>
    /// <seealso cref="ImageScalingProcessorConfiguration" />
    public class ImageWidthHeightScalingProcessorConfiguration : ImageScalingProcessorConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageWidthHeightScalingProcessorConfiguration"/> class.
        /// </summary>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="maxHeight">The maximum height.</param>
        public ImageWidthHeightScalingProcessorConfiguration(Int32 maxWidth, Int32 maxHeight)
        {
            this.MaxWidth = maxWidth;
            this.MaxHeight = maxHeight;
        }

        /// <summary>
        /// Gets the maximum width.
        /// </summary>
        /// <value>
        /// The maximum width.
        /// </value>
        public Int32 MaxWidth { get; }

        /// <summary>
        /// Gets the maximum height.
        /// </summary>
        /// <value>
        /// The maximum height.
        /// </value>
        public Int32 MaxHeight { get; }

        /// <inheritdoc />
        public override ImageSize ScaleImage(ImageSize originalSize)
        {
            if (originalSize.Width < this.MaxWidth && originalSize.Height < this.MaxHeight)
            {
                return originalSize;
            }

            var widthScale = (Double)this.MaxWidth / originalSize.Width;
            var heightScale = (Double)this.MaxHeight / originalSize.Height;
            if (widthScale < heightScale)
            {
                return new ImageSize(
                    width: this.MaxWidth,
                    height: (Int32)(originalSize.Height * widthScale));
            }
            else
            {
                return new ImageSize(
                    width: (Int32)(originalSize.Width * heightScale),
                    height: this.MaxHeight);
            }
        }
    }
}

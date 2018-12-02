using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents quality image processor configuration.
    /// </summary>
    /// <seealso cref="ImageProcessorConfiguration" />
    public class ImageQualityProcessorConfiguration : ImageProcessorConfiguration
    {
        private readonly Dictionary<String, Int32?> qualities;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageQualityProcessorConfiguration"/> class.
        /// </summary>
        /// <param name="qualities">The qualities.</param>
        public ImageQualityProcessorConfiguration(Dictionary<String, Int32?> qualities)
        {
            this.qualities = qualities.ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Gets the qualities.
        /// </summary>
        /// <value>
        /// The qualities.
        /// </value>
        public IReadOnlyDictionary<String, Int32?> Qualities => new ReadOnlyDictionary<String, Int32?>(this.qualities);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents image upload variation configuration.
    /// </summary>
    public class ImageUploadVariationConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadVariationConfiguration"/> class.
        /// </summary>
        /// <param name="id">The variation identifier.</param>
        /// <param name="processors">The variation processors.</param>
        public ImageUploadVariationConfiguration(String id, IEnumerable<ImageProcessorConfiguration> processors)
        {
            this.Id = id;
            this.Processors = processors.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the variation identifier.
        /// </summary>
        /// <value>
        /// The variation identifier.
        /// </value>
        public String Id { get; }

        /// <summary>
        /// Gets the variation processors.
        /// </summary>
        /// <value>
        /// The variation processors.
        /// </value>
        public IReadOnlyCollection<ImageProcessorConfiguration> Processors { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Models
{
    /// <summary>
    /// Represents uploaded image variation.
    /// </summary>
    public sealed class UploadedImageVariation : IEquatable<UploadedImageVariation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadedImageVariation"/> class.
        /// </summary>
        /// <param name="id">The variation identifier.</param>
        /// <param name="extension">The variation extension.</param>
        public UploadedImageVariation(String id, String extension)
        {
            this.Id = id;
            this.Extension = extension;
        }

        /// <summary>
        /// Gets the variation identifier.
        /// </summary>
        /// <value>
        /// The variation identifier.
        /// </value>
        public String Id { get; }

        /// <summary>
        /// Gets the variation extension.
        /// </summary>
        /// <value>
        /// The variation extension.
        /// </value>
        public String Extension { get; }

        /// <inheritdoc />
        public Boolean Equals(UploadedImageVariation other)
        {
            if (Object.ReferenceEquals(null, other))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return String.Equals(this.Id, other.Id) && String.Equals(this.Extension, other.Extension);
        }

        /// <inheritdoc />
        public override Boolean Equals(Object obj)
        {
            if (Object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is UploadedImageVariation variation && this.Equals(variation);
        }

        /// <inheritdoc />
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((this.Id != null ? this.Id.GetHashCode() : 0) * 397) ^ (this.Extension != null ? this.Extension.GetHashCode() : 0);
            }
        }
    }
}

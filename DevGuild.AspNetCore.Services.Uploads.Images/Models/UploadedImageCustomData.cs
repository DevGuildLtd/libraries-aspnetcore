using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Models
{
    /// <summary>
    /// Represents uploaded image custom data.
    /// </summary>
    public sealed class UploadedImageCustomData : IEquatable<UploadedImageCustomData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadedImageCustomData"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public UploadedImageCustomData(String key, String value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public String Key { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public String Value { get; }

        /// <inheritdoc />
        public Boolean Equals(UploadedImageCustomData other)
        {
            if (Object.ReferenceEquals(null, other))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return String.Equals(this.Key, other.Key) && String.Equals(this.Value, other.Value);
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

            return obj is UploadedImageCustomData customData && this.Equals(customData);
        }

        /// <inheritdoc />
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((this.Key != null ? this.Key.GetHashCode() : 0) * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
            }
        }
    }
}

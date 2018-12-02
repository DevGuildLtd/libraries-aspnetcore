using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    /// <summary>
    /// Represents size of an image.
    /// </summary>
    public struct ImageSize : IEquatable<ImageSize>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSize"/> struct.
        /// </summary>
        /// <param name="width">The image width.</param>
        /// <param name="height">The image height.</param>
        public ImageSize(Int32 width, Int32 height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets the image width.
        /// </summary>
        /// <value>
        /// The image width.
        /// </value>
        public Int32 Width { get; }

        /// <summary>
        /// Gets the image height.
        /// </summary>
        /// <value>
        /// The image height.
        /// </value>
        public Int32 Height { get; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="first">The left operand.</param>
        /// <param name="second">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Boolean operator ==(ImageSize first, ImageSize second)
        {
            return first.Height == second.Height && first.Width == second.Width;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="first">The left operand.</param>
        /// <param name="second">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Boolean operator !=(ImageSize first, ImageSize second)
        {
            return first.Height != second.Height || first.Width != second.Width;
        }

        /// <inheritdoc />
        public Boolean Equals(ImageSize other)
        {
            return this.Width == other.Width && this.Height == other.Height;
        }

        /// <inheritdoc />
        public override Boolean Equals(Object obj)
        {
            if (Object.ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is ImageSize image && this.Equals(image);
        }

        /// <inheritdoc />
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return (this.Width * 397) ^ this.Height;
            }
        }
    }
}

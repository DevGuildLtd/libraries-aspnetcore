using System;
using DevGuild.AspNetCore.Services.Uploads.Images.Models;

namespace DevGuild.AspNetCore.Services.Uploads.Images
{
    /// <summary>
    /// Represents image upload result.
    /// </summary>
    public sealed class ImageUploadResult
    {
        private ImageUploadResult(Boolean succeeded, UploadedImage image, String errorCode)
        {
            this.Succeeded = succeeded;
            this.Image = image;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets a value indicating whether the image uploaded succeeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if image uploaded succeeded; otherwise, <c>false</c>.
        /// </value>
        public Boolean Succeeded { get; }

        /// <summary>
        /// Gets the uploaded image.
        /// </summary>
        /// <value>
        /// The uploaded image.
        /// </value>
        public UploadedImage Image { get; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public String ErrorCode { get; }

        /// <summary>
        /// Create successful image upload result with the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>An image upload result.</returns>
        public static ImageUploadResult Succeed(UploadedImage image)
        {
            return new ImageUploadResult(true, image, null);
        }

        /// <summary>
        /// Created failed image upload result with the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>An image upload result.</returns>
        public static ImageUploadResult Fail(String errorCode)
        {
            return new ImageUploadResult(false, null, errorCode);
        }
    }
}

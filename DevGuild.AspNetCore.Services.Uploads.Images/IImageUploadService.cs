using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Uploads.Images.Models;
using Microsoft.AspNetCore.Http;

namespace DevGuild.AspNetCore.Services.Uploads.Images
{
    /// <summary>
    /// Defines interface of the image upload service.
    /// </summary>
    public interface IImageUploadService
    {
        /// <summary>
        /// Asynchronously gets the uploaded image.
        /// </summary>
        /// <param name="id">The image identifier.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<UploadedImage> GetUploadedImageAsync(Guid id);

        /// <summary>
        /// Asynchronously processes the image upload.
        /// </summary>
        /// <param name="configuration">The configuration name.</param>
        /// <param name="file">The uploaded file.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<ImageUploadResult> ProcessImageUploadAsync(String configuration, IFormFile file);

        /// <summary>
        /// Asynchronously creates the uploaded image.
        /// </summary>
        /// <param name="configuration">The configuration name.</param>
        /// <param name="imageName">Name of the image.</param>
        /// <param name="imageStream">The image stream.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<ImageUploadResult> CreateUploadedImageAsync(String configuration, String imageName, Stream imageStream);

        /// <summary>
        /// Asynchronously gets the image URL.
        /// </summary>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="variation">The variation.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<String> GetImageUrlAsync(Guid? imageId, String variation);

        /// <summary>
        /// Asynchronously gets the image URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="variation">The variation.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<String> GetImageUrlAsync(UploadedImage image, String variation);

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="variation">The variation.</param>
        /// <returns>A task that represents the operation.</returns>
        String GetImageUrl(UploadedImage image, String variation);
    }
}

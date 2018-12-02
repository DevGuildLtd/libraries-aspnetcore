using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Permissions
{
    /// <summary>
    /// Defines permissions that could be checked when uploading images using specific configuration.
    /// </summary>
    /// <seealso cref="PermissionsNamespace" />
    public class UploadImageConfigurationPermissionsNamespace : PermissionsNamespace
    {
        /// <summary>
        /// Gets the permission that is required to upload image using specific configuration.
        /// </summary>
        /// <value>
        /// The permission that is required to upload image using specific configuration.
        /// </value>
        public Permission UploadImage { get; } = new Permission("{FC8B14A8-1F33-44FE-A20E-E6F272053B59}", nameof(UploadImageConfigurationPermissionsNamespace.UploadImage), 1 << 0);
    }
}

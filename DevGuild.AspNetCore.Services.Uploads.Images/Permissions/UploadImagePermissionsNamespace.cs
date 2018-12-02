using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Permissions
{
    /// <summary>
    /// Defines permissions that could be checked when uploading images.
    /// </summary>
    /// <seealso cref="PermissionsNamespace" />
    public class UploadImagePermissionsNamespace : PermissionsNamespace
    {
        /// <summary>
        /// Gets the permissions that allows to upload any image.
        /// </summary>
        /// <value>
        /// The permissions that allows to upload any image.
        /// </value>
        public Permission UploadAnyImage { get; } = new Permission("{0252A2FF-A033-47EC-B970-B82235E88751}", nameof(UploadImagePermissionsNamespace.UploadAnyImage), 1 << 0);
    }
}

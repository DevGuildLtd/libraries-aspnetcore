using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Represents a result of a permissions check.
    /// </summary>
    public enum PermissionsResult
    {
        /// <summary>
        /// Indicates that access to the secured object could not be determined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Indicates that access to the secured object is allowed.
        /// </summary>
        Allow = 1 // Deny will not be implemented at the moment (Deny shall overrule Allow).
    }
}

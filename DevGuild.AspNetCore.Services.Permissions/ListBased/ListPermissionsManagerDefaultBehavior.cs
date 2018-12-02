using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.ListBased
{
    /// <summary>
    /// Represents list permissions manager default behavior.
    /// </summary>
    public enum ListPermissionsManagerDefaultBehavior
    {
        /// <summary>
        /// The access is allowed by default.
        /// </summary>
        Allow,

        /// <summary>
        /// The access is denied by default.
        /// </summary>
        Deny
    }
}

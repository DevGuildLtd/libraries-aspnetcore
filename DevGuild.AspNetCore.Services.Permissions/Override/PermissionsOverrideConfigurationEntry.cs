using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Override
{
    /// <summary>
    /// Represents permissions override configuration entry.
    /// </summary>
    public class PermissionsOverrideConfigurationEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsOverrideConfigurationEntry"/> class.
        /// </summary>
        /// <param name="childPermission">The child permission.</param>
        /// <param name="overridingPermissions">The overriding permissions.</param>
        public PermissionsOverrideConfigurationEntry(Permission childPermission, Permission[] overridingPermissions)
        {
            this.ChildPermission = childPermission;
            this.OverridingPermissions = overridingPermissions;
        }

        /// <summary>
        /// Gets the child permission.
        /// </summary>
        /// <value>
        /// The child permission.
        /// </value>
        public Permission ChildPermission { get; }

        /// <summary>
        /// Gets the overriding permissions.
        /// </summary>
        /// <value>
        /// The overriding permissions.
        /// </value>
        public Permission[] OverridingPermissions { get; }
    }
}

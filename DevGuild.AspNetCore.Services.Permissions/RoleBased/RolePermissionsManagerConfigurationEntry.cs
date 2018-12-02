using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.RoleBased
{
    /// <summary>
    /// Represents role-base permissions manager configuration entry.
    /// </summary>
    public class RolePermissionsManagerConfigurationEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RolePermissionsManagerConfigurationEntry"/> class.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="requiredRoles">The required roles.</param>
        public RolePermissionsManagerConfigurationEntry(Permission permission, String[] requiredRoles)
        {
            this.Permission = permission;
            this.RequiredRoles = requiredRoles;
        }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public Permission Permission { get; }

        /// <summary>
        /// Gets the required roles.
        /// </summary>
        /// <value>
        /// The required roles.
        /// </value>
        public String[] RequiredRoles { get; }
    }
}

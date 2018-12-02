using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Override
{
    /// <summary>
    /// Defines interface for overridable permissions manager configuration.
    /// </summary>
    public interface IOverridablePermissionsManagerConfiguration
    {
        /// <summary>
        /// Gets the override mode.
        /// </summary>
        /// <value>
        /// The override mode.
        /// </value>
        PermissionsOverrideMode OverrideMode { get; }

        /// <summary>
        /// Gets the overrides for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>A collection of overrides.</returns>
        IEnumerable<PermissionsOverrideConfigurationEntry> GetOverridesForPermission(Permission permission);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Override
{
    /// <summary>
    /// Defines interface for query overridable permissions manager configuration.
    /// </summary>
    public interface IQueryOverridablePermissionsManagerConfiguration
    {
        /// <summary>
        /// Gets the query override mode.
        /// </summary>
        /// <value>
        /// The query override mode.
        /// </value>
        QueryPermissionsOverrideMode QueryOverrideMode { get; }

        /// <summary>
        /// Gets the query overrides for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>A collection of overrides.</returns>
        IEnumerable<PermissionsOverrideConfigurationEntry> GetQueryOverridesForPermission(Permission permission);
    }
}

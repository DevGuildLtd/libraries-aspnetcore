using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;
using DevGuild.AspNetCore.Services.Permissions.Override;

namespace DevGuild.AspNetCore.Services.Permissions.RoleBased
{
    /// <summary>
    /// Represents role-based permissions manager configuration.
    /// </summary>
    /// <seealso cref="IOverridablePermissionsManagerConfiguration" />
    /// <seealso cref="IQueryOverridablePermissionsManagerConfiguration" />
    public class RolePermissionsManagerConfiguration : IOverridablePermissionsManagerConfiguration, IQueryOverridablePermissionsManagerConfiguration
    {
        private readonly List<RolePermissionsManagerConfigurationEntry> entries;
        private readonly List<Permission> anonymousPermissions;
        private readonly PermissionsOverrideConfiguration overrides;
        private readonly PermissionsOverrideConfiguration queryOverrideConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolePermissionsManagerConfiguration"/> class.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <param name="anonymousPermissions">The permissions that allows anonymous access.</param>
        /// <param name="overrideMode">The override mode.</param>
        /// <param name="overrides">The overrides configuration.</param>
        /// <param name="queryOverrideMode">The query override mode.</param>
        /// <param name="queryOverrideConfiguration">The query override configuration.</param>
        public RolePermissionsManagerConfiguration(List<RolePermissionsManagerConfigurationEntry> entries, List<Permission> anonymousPermissions, PermissionsOverrideMode overrideMode, PermissionsOverrideConfiguration overrides, QueryPermissionsOverrideMode queryOverrideMode, PermissionsOverrideConfiguration queryOverrideConfiguration)
        {
            this.entries = entries;
            this.anonymousPermissions = anonymousPermissions;
            this.OverrideMode = overrideMode;
            this.overrides = overrides;
            this.QueryOverrideMode = queryOverrideMode;
            this.queryOverrideConfiguration = queryOverrideConfiguration;
        }

        /// <summary>
        /// Gets the permissions that allows anonymous access.
        /// </summary>
        /// <value>
        /// The permissions that allows anonymous access.
        /// </value>
        public IList<Permission> AnonymousPermissions => this.anonymousPermissions.AsReadOnly();

        /// <inheritdoc />
        public PermissionsOverrideMode OverrideMode { get; }

        /// <inheritdoc />
        public QueryPermissionsOverrideMode QueryOverrideMode { get; }

        /// <summary>
        /// Gets the entries for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>A collection of configuration entries.</returns>
        public IEnumerable<RolePermissionsManagerConfigurationEntry> GetEntriesForPermission(Permission permission)
        {
            return this.entries.Where(x => Object.Equals(x.Permission, permission));
        }

        /// <inheritdoc />
        public IEnumerable<PermissionsOverrideConfigurationEntry> GetOverridesForPermission(Permission permission)
        {
            return this.overrides.GetOverridesForPermission(permission);
        }

        /// <inheritdoc />
        public IEnumerable<PermissionsOverrideConfigurationEntry> GetQueryOverridesForPermission(Permission permission)
        {
            return this.queryOverrideConfiguration.GetOverridesForPermission(permission);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;
using DevGuild.AspNetCore.Services.Permissions.Override;

namespace DevGuild.AspNetCore.Services.Permissions.RoleRelation
{
    /// <summary>
    /// Represents role and relation-based permissions manager configuration.
    /// </summary>
    /// <typeparam name="T">Type of the secured object.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="IOverridablePermissionsManagerConfiguration" />
    /// <seealso cref="IQueryOverridablePermissionsManagerConfiguration" />
    public class RoleRelationPermissionsManagerConfiguration<T, TKey> : IOverridablePermissionsManagerConfiguration, IQueryOverridablePermissionsManagerConfiguration
        where TKey : IEquatable<TKey>
    {
        private readonly List<RoleRelationPermissionsManagerConfigurationEntry<T, TKey>> entries;
        private readonly PermissionsOverrideConfiguration overrides;
        private readonly PermissionsOverrideConfiguration queryOverrideConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRelationPermissionsManagerConfiguration{T, TKey}"/> class.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <param name="overrideMode">The override mode.</param>
        /// <param name="overrides">The override configuration.</param>
        /// <param name="queryOverrideMode">The query override mode.</param>
        /// <param name="queryOverrideConfiguration">The query override configuration.</param>
        public RoleRelationPermissionsManagerConfiguration(List<RoleRelationPermissionsManagerConfigurationEntry<T, TKey>> entries, PermissionsOverrideMode overrideMode, PermissionsOverrideConfiguration overrides, QueryPermissionsOverrideMode queryOverrideMode, PermissionsOverrideConfiguration queryOverrideConfiguration)
        {
            this.entries = entries;
            this.OverrideMode = overrideMode;
            this.overrides = overrides;
            this.QueryOverrideMode = queryOverrideMode;
            this.queryOverrideConfiguration = queryOverrideConfiguration;
        }

        /// <inheritdoc />
        public PermissionsOverrideMode OverrideMode { get; }

        /// <inheritdoc />
        public QueryPermissionsOverrideMode QueryOverrideMode { get; }

        /// <summary>
        /// Gets the entries for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>A list of configuration entries.</returns>
        public IEnumerable<RoleRelationPermissionsManagerConfigurationEntry<T, TKey>> GetEntriesForPermission(Permission permission)
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

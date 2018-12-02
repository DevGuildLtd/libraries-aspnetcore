using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;
using DevGuild.AspNetCore.Services.Permissions.Override;

namespace DevGuild.AspNetCore.Services.Permissions.RoleBased
{
    /// <summary>
    /// Represents role-based permissions manager configuration builder.
    /// </summary>
    public class RolePermissionsManagerConfigurationBuilder
    {
        private readonly List<RolePermissionsManagerConfigurationEntry> entries = new List<RolePermissionsManagerConfigurationEntry>();
        private readonly List<Permission> anonymousPermissions = new List<Permission>();
        private PermissionsOverrideMode overrideMode = PermissionsOverrideMode.Disabled;
        private PermissionsOverrideConfiguration overrideConfiguration;
        private QueryPermissionsOverrideMode queryOverrideMode = QueryPermissionsOverrideMode.Disabled;
        private PermissionsOverrideConfiguration queryOverrideConfiguration;

        /// <summary>
        /// Allows the anonymous access for the specified permissions.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <returns>An instance of this builder.</returns>
        public RolePermissionsManagerConfigurationBuilder AllowAnonymous(params Permission[] permissions)
        {
            this.anonymousPermissions.AddRange(permissions);
            return this;
        }

        /// <summary>
        /// Requires the specified roles for the specified permissions.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="roles">The list of roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RolePermissionsManagerConfigurationBuilder RequireRoles(Permission permission, params String[] roles)
        {
            this.entries.Add(new RolePermissionsManagerConfigurationEntry(permission, roles));
            return this;
        }

        /// <summary>
        /// Requires the specified roles for the specified permissions.
        /// </summary>
        /// <param name="permissions">The list of permissions.</param>
        /// <param name="roles">The list of roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RolePermissionsManagerConfigurationBuilder RequireRoles(IEnumerable<Permission> permissions, params String[] roles)
        {
            var temp = permissions.Select(x => new RolePermissionsManagerConfigurationEntry(x, roles)).ToList();
            this.entries.AddRange(temp);
            return this;
        }

        /// <summary>
        /// Requires the specified roles for the specified permissions namespace.
        /// </summary>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="roles">The list of roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RolePermissionsManagerConfigurationBuilder RequireRoles(PermissionsNamespace permissionsNamespace, params String[] roles)
        {
            return this.RequireRoles(permissionsNamespace.Permissions, roles);
        }

        /// <summary>
        /// Configures the overrides.
        /// </summary>
        /// <param name="overrideMode">The override mode.</param>
        /// <param name="configure">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public RolePermissionsManagerConfigurationBuilder ConfigureOverrides(PermissionsOverrideMode overrideMode, Func<PermissionsOverrideConfigurationBuilder, PermissionsOverrideConfigurationBuilder> configure)
        {
            if (this.overrideConfiguration != null)
            {
                throw new InvalidOperationException("Overrides are already configured");
            }

            this.overrideMode = overrideMode;
            var builder = new PermissionsOverrideConfigurationBuilder();
            this.overrideConfiguration = configure(builder).BuildConfiguration();
            return this;
        }

        /// <summary>
        /// Configures the query overrides.
        /// </summary>
        /// <param name="overrideMode">The override mode.</param>
        /// <param name="configure">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public RolePermissionsManagerConfigurationBuilder ConfigureQueryOverrides(QueryPermissionsOverrideMode overrideMode, Func<PermissionsOverrideConfigurationBuilder, PermissionsOverrideConfigurationBuilder> configure)
        {
            if (this.queryOverrideConfiguration != null)
            {
                throw new InvalidOperationException("Query overrides are already configured");
            }

            this.queryOverrideMode = overrideMode;
            var builder = new PermissionsOverrideConfigurationBuilder();
            this.queryOverrideConfiguration = configure(builder).BuildConfiguration();
            return this;
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <returns>Role-based permissions manager configuration.</returns>
        public RolePermissionsManagerConfiguration BuildConfiguration()
        {
            return new RolePermissionsManagerConfiguration(this.entries, this.anonymousPermissions, this.overrideMode, this.overrideConfiguration ?? new PermissionsOverrideConfiguration(), this.queryOverrideMode, this.queryOverrideConfiguration ?? new PermissionsOverrideConfiguration());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Override
{
    /// <summary>
    /// Represents permissions override configuration builder.
    /// </summary>
    public class PermissionsOverrideConfigurationBuilder
    {
        private readonly List<PermissionsOverrideConfigurationEntry> entries = new List<PermissionsOverrideConfigurationEntry>();

        /// <summary>
        /// Adds the simple override.
        /// </summary>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="overridingParentPermissions">The overriding parent permissions.</param>
        /// <returns>An instance of this builder.</returns>
        public PermissionsOverrideConfigurationBuilder AddSimpleOverride(PermissionsNamespace permissionsNamespace, params Permission[] overridingParentPermissions)
        {
            if (permissionsNamespace == null)
            {
                throw new ArgumentNullException($"{nameof(permissionsNamespace)} is null", nameof(permissionsNamespace));
            }

            if (overridingParentPermissions == null)
            {
                throw new ArgumentNullException($"{nameof(overridingParentPermissions)} is null", nameof(overridingParentPermissions));
            }

            if (overridingParentPermissions.Length == 0)
            {
                throw new ArgumentException($"{nameof(overridingParentPermissions)} must have at lease one permission.", nameof(overridingParentPermissions));
            }

            foreach (var permission in permissionsNamespace.Permissions)
            {
                this.entries.Add(new PermissionsOverrideConfigurationEntry(permission, overridingParentPermissions));
            }
            return this;
        }

        /// <summary>
        /// Adds the simple override.
        /// </summary>
        /// <param name="childPermission">The child permission.</param>
        /// <param name="overridingParentPermissions">The overriding parent permissions.</param>
        /// <returns>An instance of this builder.</returns>
        public PermissionsOverrideConfigurationBuilder AddSimpleOverride(Permission childPermission, params Permission[] overridingParentPermissions)
        {
            if (childPermission == null)
            {
                throw new ArgumentNullException($"{nameof(childPermission)} is null", nameof(childPermission));
            }

            if (overridingParentPermissions == null)
            {
                throw new ArgumentNullException($"{nameof(overridingParentPermissions)} is null", nameof(overridingParentPermissions));
            }

            if (overridingParentPermissions.Length == 0)
            {
                throw new ArgumentException($"{nameof(overridingParentPermissions)} must have at lease one permission.", nameof(overridingParentPermissions));
            }
            
            this.entries.Add(new PermissionsOverrideConfigurationEntry(childPermission, overridingParentPermissions));
            return this;
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <returns>Permissions override configuration.</returns>
        public PermissionsOverrideConfiguration BuildConfiguration()
        {
            return new PermissionsOverrideConfiguration(this.entries);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Override
{
    /// <summary>
    /// Represents permissions override configuration.
    /// </summary>
    public class PermissionsOverrideConfiguration
    {
        private readonly List<PermissionsOverrideConfigurationEntry> entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsOverrideConfiguration"/> class.
        /// </summary>
        public PermissionsOverrideConfiguration()
        {
            this.entries = new List<PermissionsOverrideConfigurationEntry>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsOverrideConfiguration"/> class.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public PermissionsOverrideConfiguration(List<PermissionsOverrideConfigurationEntry> entries)
        {
            this.entries = entries;
        }

        /// <summary>
        /// Gets the overrides for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>A collection of overrides.</returns>
        public IEnumerable<PermissionsOverrideConfigurationEntry> GetOverridesForPermission(Permission permission)
        {
            return this.entries.Where(x => Object.Equals(x.ChildPermission, permission));
        }
    }
}

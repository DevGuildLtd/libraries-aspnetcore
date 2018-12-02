using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Entity
{
    /// <summary>
    /// Defines permissions that could be checked when accessing a single hierarchical entity.
    /// </summary>
    /// <seealso cref="EntityPermissionsNamespace" />
    public class HierarchicalEntityPermissionsNamespace : EntityPermissionsNamespace
    {
        /// <summary>
        /// Gets the permission that is required to create a child entity.
        /// </summary>
        /// <value>
        /// The permission that is required to create a child entity.
        /// </value>
        public Permission CreateChild { get; } = new Permission("{ACCAAF0F-9DBD-4F17-AFFB-C1F772EE4C37}", "CreateChild", 1 << 3);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Entity
{
    /// <summary>
    /// Defines permissions that could be checked when accessing an entire hierarchical entity type.
    /// </summary>
    /// <seealso cref="EntityTypePermissionsNamespace" />
    public class HierarchicalEntityTypePermissionsNamespace : EntityTypePermissionsNamespace
    {
        /// <summary>
        /// Gets the permission that allows to create a child for any entity of this type.
        /// </summary>
        /// <value>
        /// The permission that allows to create a child for any entity of this type.
        /// </value>
        public Permission CreateChildForAnyEntity { get; } = new Permission("{6E0669B2-7375-4718-8F80-75FEE8F37E18}", "CreateChildForAnyEntity", 1 << 9);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Entity
{
    /// <summary>
    /// Defines permissions that could be checked when accessing an entire parent entity type.
    /// </summary>
    /// <seealso cref="EntityTypePermissionsNamespace" />
    public class ParentEntityTypePermissionsNamespace : EntityTypePermissionsNamespace
    {
        /// <summary>
        /// Gets the permission that allows to create dependent entity for any entity.
        /// </summary>
        /// <value>
        /// The permission that allows to create dependent entity for any entity.
        /// </value>
        public Permission CreateDependentForAnyEntity { get; } = new Permission("{EDD8225C-DCF3-40B7-8D5C-38BFD7DDF7A1}", "CreateDependentForAnyEntity", 1 << 9);
    }
}

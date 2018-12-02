using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Entity
{
    /// <summary>
    /// Defines permissions that could be checked when accessing a single parent entity.
    /// </summary>
    /// <seealso cref="EntityPermissionsNamespace" />
    public class ParentEntityPermissionsNamespace : EntityPermissionsNamespace
    {
        /// <summary>
        /// Gets the permission that is required to create dependent entities.
        /// </summary>
        /// <value>
        /// The permission that is required to create dependent entities
        /// </value>
        public Permission CreateDependent { get; } = new Permission("{3B66298B-4AB5-47EB-93A2-E38ECC681B3D}", "CreateDependent", 1 << 3);
    }
}

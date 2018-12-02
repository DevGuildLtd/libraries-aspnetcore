using System;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Entity
{
    /// <summary>
    /// Defines permissions that could be checked when accessing a single entity.
    /// </summary>
    /// <seealso cref="PermissionsNamespace" />
    public class EntityPermissionsNamespace : PermissionsNamespace
    {
        /// <summary>
        /// Gets the permission that is required to read the entity.
        /// </summary>
        /// <value>
        /// The permission that is required to read the entity.
        /// </value>
        public Permission Read { get; } = new Permission("{2E8D96B5-2B42-4969-9EF2-13A2525E9D6C}", "Read", 1 << 0);

        /// <summary>
        /// Gets the permission that is required to update the entity.
        /// </summary>
        /// <value>
        /// The permission that is required to update the entity.
        /// </value>
        public Permission Update { get; } = new Permission("{1E236CD6-670C-4498-8A47-7436C5673D7B}", "Update", 1 << 1);

        /// <summary>
        /// Gets the permission that is required to delete the entity.
        /// </summary>
        /// <value>
        /// The permission that is required to delete the entity.
        /// </value>
        public Permission Delete { get; } = new Permission("{5B4DBD8F-45B6-4318-B42B-2F587A66126E}", "Delete", 1 << 2);
    }
}

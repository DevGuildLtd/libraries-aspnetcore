using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Entity
{
    /// <summary>
    /// Defines permissions that could be checked when accessing an entity property.
    /// </summary>
    /// <seealso cref="PermissionsNamespace" />
    public class EntityPropertyPermissionsNamespace : PermissionsNamespace
    {
        /// <summary>
        /// Gets the permission that is required to read property value.
        /// </summary>
        /// <value>
        /// Permission that is required to read property value.
        /// </value>
        public Permission Read { get; } = new Permission("{7709DB6F-F6CE-4F4D-93DE-CBB2495A6530}", "Read", 1 << 0);

        /// <summary>
        /// Gets the permission that is required to initialize property value on entity creation.
        /// </summary>
        /// <value>
        /// Permission that is required to initialize property value on entity creation.
        /// </value>
        public Permission Initialize { get; } = new Permission("{BBB15118-CD07-4752-AA17-8D0B7BCE17A2}", "Initialize", 1 << 1);

        /// <summary>
        /// Gets the permission that is required to update property value.
        /// </summary>
        /// <value>
        /// Permission that is required to update property value.
        /// </value>
        public Permission Update { get; } = new Permission("{AC4C7FD8-B81D-4899-B7CD-6E4D02E1D52B}", "Update", 1 << 2);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.Entity
{
    /// <summary>
    /// Defines permissions that could be checked when accessing an entire entity type.
    /// </summary>
    /// <seealso cref="PermissionsNamespace" />
    public class EntityTypePermissionsNamespace : PermissionsNamespace
    {
        /// <summary>
        /// Gets the permission that is required to access entity type.
        /// </summary>
        /// <value>
        /// The permission that is required to access entity type.
        /// </value>
        public Permission Access { get; } = new Permission("{AC6A4DE5-1D57-4F9C-A3BE-B1F5ABD76C05}", "Access", 1 << 0);

        /// <summary>
        /// Gets the permission that is required to create a new entity of an entity type.
        /// </summary>
        /// <value>
        /// The permission that is required to create a new entity of an entity type.
        /// </value>
        public Permission Create { get; } = new Permission("{6F915AA3-7E83-4E27-BC48-0CC01477AEF6}", "Create", 1 << 1);

        /// <summary>
        /// Gets the permission that allows to read any entity of this type.
        /// </summary>
        /// <value>
        /// The permissions that allows to read any entity of this type.
        /// </value>
        public Permission ReadAnyEntity { get; } = new Permission("{50BC8FDC-A77C-424F-8CCC-8E8BF23EA2EA}", "ReadAnyEntity", 1 << 2);

        /// <summary>
        /// Gets the permission that allows to create any entity of this type.
        /// </summary>
        /// <value>
        /// The permission that allows to create any entity of this type.
        /// </value>
        public Permission CreateAnyEntity { get; } = new Permission("{2B1FE65E-A2A3-4FF9-B4C5-3907779799E3}", "CreateAnyEntity", 1 << 3);

        /// <summary>
        /// Gets the permission that allows to update any entity of this type.
        /// </summary>
        /// <value>
        /// The permission that allows to update any entity of this type.
        /// </value>
        public Permission UpdateAnyEntity { get; } = new Permission("{925F679A-4DD7-4C21-9D91-47EE48EE5AE6}", "UpdateAnyEntity", 1 << 4);

        /// <summary>
        /// Gets the permission that allows to delete any entity of this type.
        /// </summary>
        /// <value>
        /// The permission that allows to delete any entity of this type.
        /// </value>
        public Permission DeleteAnyEntity { get; } = new Permission("{893507B3-2424-4C1D-B71D-FD5AF246065B}", "DeleteAnyEntity", 1 << 5);

        /// <summary>
        /// Gets the permission that allows to read any property of this type.
        /// </summary>
        /// <value>
        /// The permission that allows to read any property of this type.
        /// </value>
        public Permission ReadAnyProperty { get; } = new Permission("{6C377408-BEC0-48CE-8945-4DC24722F7BB}", "ReadAnyProperty", 1 << 6);

        /// <summary>
        /// Gets the permission that allows to initialize any property of this type.
        /// </summary>
        /// <value>
        /// The permission that allows to initialize any property of this type.
        /// </value>
        public Permission InitializeAnyProperty { get; } = new Permission("{C3308D2E-816B-45DC-AB9E-00E7CE3AD535}", "InitializeAnyProperty", 1 << 7);

        /// <summary>
        /// Gets the permission that allows to update any property of this type.
        /// </summary>
        /// <value>
        /// The permission that allows to update any property of this type.
        /// </value>
        public Permission UpdateAnyProperty { get; } = new Permission("{A3165233-D299-4454-AEC2-E46B42A46717}", "UpdateAnyProperty", 1 << 8);
    }
}

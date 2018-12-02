using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.Entity
{
    /// <summary>
    /// Represents an entity permissions.
    /// </summary>
    public static class EntityPermissions
    {
        /// <summary>
        /// Gets the single entity permissions.
        /// </summary>
        /// <value>
        /// The single entity permissions.
        /// </value>
        public static EntityPermissionsNamespace Entity { get; } = new EntityPermissionsNamespace();

        /// <summary>
        /// Gets the entity type permissions.
        /// </summary>
        /// <value>
        /// The entity type permissions.
        /// </value>
        public static EntityTypePermissionsNamespace EntityType { get; } = new EntityTypePermissionsNamespace();

        /// <summary>
        /// Gets the entity property permissions.
        /// </summary>
        /// <value>
        /// The entity property permissions.
        /// </value>
        public static EntityPropertyPermissionsNamespace EntityProperty { get; } = new EntityPropertyPermissionsNamespace();

        /// <summary>
        /// Gets the single hierarchical entity permissions.
        /// </summary>
        /// <value>
        /// The single hierarchical entity permissions.
        /// </value>
        public static HierarchicalEntityPermissionsNamespace HierarchicalEntity { get; } = new HierarchicalEntityPermissionsNamespace();

        /// <summary>
        /// Gets the hierarchical entity type permissions.
        /// </summary>
        /// <value>
        /// The hierarchical entity type permissions.
        /// </value>
        public static HierarchicalEntityTypePermissionsNamespace HierarchicalEntityType { get; } = new HierarchicalEntityTypePermissionsNamespace();

        /// <summary>
        /// Gets the single parent entity permissions.
        /// </summary>
        /// <value>
        /// The single parent entity permissions.
        /// </value>
        public static ParentEntityPermissionsNamespace ParentEntity { get; } = new ParentEntityPermissionsNamespace();

        /// <summary>
        /// Gets the parent entity type permissions.
        /// </summary>
        /// <value>
        /// The parent entity type permissions.
        /// </value>
        public static ParentEntityTypePermissionsNamespace ParentEntityType { get; } = new ParentEntityTypePermissionsNamespace();
    }
}

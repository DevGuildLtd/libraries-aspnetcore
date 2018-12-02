using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.RoleRelation
{
    /// <summary>
    /// Represents role and relation-based permissions manager configuration entry.
    /// </summary>
    /// <typeparam name="T">Type of the secured object.</typeparam>
    /// <typeparam name="TKey">The type of the user key.</typeparam>
    public class RoleRelationPermissionsManagerConfigurationEntry<T, TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRelationPermissionsManagerConfigurationEntry{T, TKey}"/> class.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="requiredRoles">The required roles.</param>
        /// <param name="relation">The relation.</param>
        public RoleRelationPermissionsManagerConfigurationEntry(Permission permission, String[] requiredRoles, UserRelation<T, TKey> relation)
        {
            this.Permission = permission;
            this.RequiredRoles = requiredRoles;
            this.Relation = relation;
        }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public Permission Permission { get; }

        /// <summary>
        /// Gets the required roles.
        /// </summary>
        /// <value>
        /// The required roles.
        /// </value>
        public String[] RequiredRoles { get; }

        /// <summary>
        /// Gets the relation.
        /// </summary>
        /// <value>
        /// The relation.
        /// </value>
        public UserRelation<T, TKey> Relation { get; }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public Int32 Priority => this.RequiredRoles == null && this.Relation == null ? 0 : this.Relation == null ? 1 : this.RequiredRoles != null ? 2 : 3;
    }
}

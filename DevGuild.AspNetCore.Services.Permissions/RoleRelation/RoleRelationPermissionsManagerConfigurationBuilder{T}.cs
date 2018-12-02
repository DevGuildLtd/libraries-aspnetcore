using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;
using DevGuild.AspNetCore.Services.Permissions.Override;

namespace DevGuild.AspNetCore.Services.Permissions.RoleRelation
{
    /// <summary>
    /// Represents role and relation-based permissions manager configuration builder.
    /// </summary>
    /// <typeparam name="T">The type of the secured object.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class RoleRelationPermissionsManagerConfigurationBuilder<T, TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly List<RoleRelationPermissionsManagerConfigurationEntry<T, TKey>> entries = new List<RoleRelationPermissionsManagerConfigurationEntry<T, TKey>>();
        private PermissionsOverrideMode overrideMode = PermissionsOverrideMode.Disabled;
        private PermissionsOverrideConfiguration overrideConfiguration;
        private QueryPermissionsOverrideMode queryOverrideMode = QueryPermissionsOverrideMode.Disabled;
        private PermissionsOverrideConfiguration queryOverrideConfiguration;

        /// <summary>
        /// Requires nothing for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireNothing(Permission permission)
        {
            this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, null, null));
            return this;
        }

        /// <summary>
        /// Requires that the user is authenticated for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireAuthentication(Permission permission)
        {
            this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, new String[] { }, null));
            return this;
        }

        /// <summary>
        /// Requires the specified roles for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRoles(Permission permission, params String[] roles)
        {
            this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, roles, null));
            return this;
        }

        /// <summary>
        /// Requires reference-based user relation for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRelations(Permission permission, Expression<Func<T, TKey>> userIdExpression)
        {
            this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, null, new ReferenceUserRelation<T, TKey>(userIdExpression)));
            return this;
        }

        /// <summary>
        /// Requires collection-based user relation for the specified permission.
        /// </summary>
        /// <typeparam name="TCollectionItem">The type of the collection item.</typeparam>
        /// <param name="permission">The permission.</param>
        /// <param name="collectionExpression">The expression that points to the collection.</param>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRelations<TCollectionItem>(Permission permission, Expression<Func<T, IEnumerable<TCollectionItem>>> collectionExpression, Expression<Func<TCollectionItem, TKey>> userIdExpression)
        {
            this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, null, new CollectionUserRelation<T, TCollectionItem, TKey>(collectionExpression, userIdExpression)));
            return this;
        }

        /// <summary>
        /// Requires the specified roles and user relation for the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        /// <param name="roles">The required roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRolesAndRelations(Permission permission, Expression<Func<T, TKey>> userIdExpression, params String[] roles)
        {
            this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, roles, new ReferenceUserRelation<T, TKey>(userIdExpression)));
            return this;
        }

        /// <summary>
        /// Requires the specified roles and user relation for the specified permission.
        /// </summary>
        /// <typeparam name="TCollectionItem">The type of the collection item.</typeparam>
        /// <param name="permission">The permission.</param>
        /// <param name="collectionExpression">The expression that points to the collection.</param>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        /// <param name="roles">The required roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRolesAndRelations<TCollectionItem>(Permission permission, Expression<Func<T, IEnumerable<TCollectionItem>>> collectionExpression, Expression<Func<TCollectionItem, TKey>> userIdExpression, params String[] roles)
        {
            this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, roles, new CollectionUserRelation<T, TCollectionItem, TKey>(collectionExpression, userIdExpression)));
            return this;
        }

        /// <summary>
        /// Requires nothing for the specified permissions namespace.
        /// </summary>
        /// <param name="permissions">The permissions namespace.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireNothing(PermissionsNamespace permissions)
        {
            foreach (var permission in permissions.Permissions)
            {
                this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, null, null));
            }

            return this;
        }

        /// <summary>
        /// Requires that the user is authenticated for the specified permissions namespace.
        /// </summary>
        /// <param name="permissions">The permissions namespace.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireAuthentication(PermissionsNamespace permissions)
        {
            foreach (var permission in permissions.Permissions)
            {
                this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, new String[] { }, null));
            }

            return this;
        }

        /// <summary>
        /// Requires the specified roles for the specified permissions namespace.
        /// </summary>
        /// <param name="permissions">The permissions namespace.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRoles(PermissionsNamespace permissions, params String[] roles)
        {
            foreach (var permission in permissions.Permissions)
            {
                this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, roles, null));
            }

            return this;
        }

        /// <summary>
        /// Requires user relation for the specified permissions namespace.
        /// </summary>
        /// <param name="permissions">The permissions namespace.</param>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRelations(PermissionsNamespace permissions, Expression<Func<T, TKey>> userIdExpression)
        {
            foreach (var permission in permissions.Permissions)
            {
                this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, null, new ReferenceUserRelation<T, TKey>(userIdExpression)));
            }

            return this;
        }

        /// <summary>
        /// Requires user relation for the specified permissions namespace.
        /// </summary>
        /// <typeparam name="TCollectionItem">The type of the collection item.</typeparam>
        /// <param name="permissions">The permissions namespace.</param>
        /// <param name="collectionExpression">The expression that points to the collection.</param>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRelations<TCollectionItem>(PermissionsNamespace permissions, Expression<Func<T, IEnumerable<TCollectionItem>>> collectionExpression, Expression<Func<TCollectionItem, TKey>> userIdExpression)
        {
            foreach (var permission in permissions.Permissions)
            {
                this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, null, new CollectionUserRelation<T, TCollectionItem, TKey>(collectionExpression, userIdExpression)));
            }

            return this;
        }

        /// <summary>
        /// Requires the specified roles and user relation for the specified permission.
        /// </summary>
        /// <param name="permissions">The permissions namespace.</param>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        /// <param name="roles">The required roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRolesAndRelations(PermissionsNamespace permissions, Expression<Func<T, TKey>> userIdExpression, params String[] roles)
        {
            foreach (var permission in permissions.Permissions)
            {
                this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, roles, new ReferenceUserRelation<T, TKey>(userIdExpression)));
            }

            return this;
        }

        /// <summary>
        /// Requires the specified roles and user relation for the specified permission.
        /// </summary>
        /// <typeparam name="TCollectionItem">The type of the collection item.</typeparam>
        /// <param name="permissions">The permissions namespace.</param>
        /// <param name="collectionExpression">The expression that points to the collection.</param>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        /// <param name="roles">The required roles.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> RequireRolesAndRelations<TCollectionItem>(PermissionsNamespace permissions, Expression<Func<T, IEnumerable<TCollectionItem>>> collectionExpression, Expression<Func<TCollectionItem, TKey>> userIdExpression, params String[] roles)
        {
            foreach (var permission in permissions.Permissions)
            {
                this.entries.Add(new RoleRelationPermissionsManagerConfigurationEntry<T, TKey>(permission, roles, new CollectionUserRelation<T, TCollectionItem, TKey>(collectionExpression, userIdExpression)));
            }

            return this;
        }

        /// <summary>
        /// Configures the overrides.
        /// </summary>
        /// <param name="overrideMode">The override mode.</param>
        /// <param name="configure">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> ConfigureOverrides(PermissionsOverrideMode overrideMode, Func<PermissionsOverrideConfigurationBuilder, PermissionsOverrideConfigurationBuilder> configure)
        {
            if (this.overrideConfiguration != null)
            {
                throw new InvalidOperationException("Overrides are already configured");
            }

            this.overrideMode = overrideMode;
            var builder = new PermissionsOverrideConfigurationBuilder();
            this.overrideConfiguration = configure(builder).BuildConfiguration();
            return this;
        }

        /// <summary>
        /// Configures the query overrides.
        /// </summary>
        /// <param name="overrideMode">The override mode.</param>
        /// <param name="configure">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public RoleRelationPermissionsManagerConfigurationBuilder<T, TKey> ConfigureQueryOverrides(QueryPermissionsOverrideMode overrideMode, Func<PermissionsOverrideConfigurationBuilder, PermissionsOverrideConfigurationBuilder> configure)
        {
            if (this.queryOverrideConfiguration != null)
            {
                throw new InvalidOperationException("Query overrides are already configured");
            }

            this.queryOverrideMode = overrideMode;
            var builder = new PermissionsOverrideConfigurationBuilder();
            this.queryOverrideConfiguration = configure(builder).BuildConfiguration();
            return this;
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <returns>Role and relation-based permissions manager configuration.</returns>
        public RoleRelationPermissionsManagerConfiguration<T, TKey> BuildConfiguration()
        {
            return new RoleRelationPermissionsManagerConfiguration<T, TKey>(this.entries, this.overrideMode, this.overrideConfiguration, this.queryOverrideMode, this.queryOverrideConfiguration);
        }
    }
}

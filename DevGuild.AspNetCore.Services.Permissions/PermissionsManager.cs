using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.ListBased;
using DevGuild.AspNetCore.Services.Permissions.ListRoleBased;
using DevGuild.AspNetCore.Services.Permissions.RoleBased;
using DevGuild.AspNetCore.Services.Permissions.RoleRelation;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Allows configuring different types of permissions managers.
    /// </summary>
    public static class PermissionsManager
    {
        /// <summary>
        /// Configures role-based permissions manager and returns its constructor.
        /// </summary>
        /// <param name="configure">The configuration.</param>
        /// <returns>Permissions manager constructor.</returns>
        public static PermissionsManagerConstructor FixedByRole(Func<RolePermissionsManagerConfigurationBuilder, RolePermissionsManagerConfigurationBuilder> configure)
        {
            var configurationBuilder = new RolePermissionsManagerConfigurationBuilder();
            var configuration = configure(configurationBuilder).BuildConfiguration();

            return (hub, parent, ns, provider) => new RolePermissionsManager(hub, parent, ns, provider, configuration);
        }

        /// <summary>
        /// Configures role-based permissions manager and returns its constructor.
        /// </summary>
        /// <typeparam name="T">Type of the secured object.</typeparam>
        /// <param name="configure">The configuration.</param>
        /// <returns>Permissions manager constructor.</returns>
        public static PermissionsManagerConstructor FixedByRole<T>(Func<RolePermissionsManagerConfigurationBuilder, RolePermissionsManagerConfigurationBuilder> configure)
        {
            var configurationBuilder = new RolePermissionsManagerConfigurationBuilder();
            var configuration = configure(configurationBuilder).BuildConfiguration();

            return (hub, parent, ns, provider) => new RolePermissionsManager<T>(hub, parent, ns, provider, configuration);
        }

        /// <summary>
        /// Configures list and role-based permissions manager and returns its constructor.
        /// </summary>
        /// <typeparam name="T">Type of the secured object.</typeparam>
        /// <param name="configure">The configuration.</param>
        /// <returns>Permissions manager constructor.</returns>
        public static PermissionsManagerConstructor ListByRole<T>(Func<ListPermissionsManagerConfigurationBuilder<T, RoleList>, ListPermissionsManagerConfigurationBuilder<T, RoleList>> configure)
            where T : IEquatable<T>
        {
            var builder = new ListPermissionsManagerConfigurationBuilder<T, RoleList>();
            var configuration = configure(builder).BuildConfiguration();

            return (hub, parent, ns, provider) => new ListRolePermissionsManager<T>(hub, parent, ns, provider, configuration);
        }

        /// <summary>
        /// Configures role and relation-based permissions manager and returns its constructor.
        /// </summary>
        /// <typeparam name="T">Type of the secured object.</typeparam>
        /// <typeparam name="TKey">The type of the user key.</typeparam>
        /// <param name="configure">The configuration.</param>
        /// <returns>Permissions manager constructor.</returns>
        public static PermissionsManagerConstructor ByRoleOrRelation<T, TKey>(Func<RoleRelationPermissionsManagerConfigurationBuilder<T, TKey>, RoleRelationPermissionsManagerConfigurationBuilder<T, TKey>> configure)
            where TKey : IEquatable<TKey>
        {
            var configurationBuilder = new RoleRelationPermissionsManagerConfigurationBuilder<T, TKey>();
            var configuration = configure(configurationBuilder).BuildConfiguration();

            return (hub, parent, ns, provider) => new RoleRelationPermissionsManager<T, TKey>(hub, parent, ns, provider, configuration);
        }
    }
}

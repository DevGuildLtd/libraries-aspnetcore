using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Identity;
using DevGuild.AspNetCore.Services.Permissions.Base;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.RoleRelation
{
    /// <summary>
    /// Represents role and relation-based permissions manager.
    /// </summary>
    /// <typeparam name="T">Type of the secured object.</typeparam>
    /// <typeparam name="TKey">The type of the user key.</typeparam>
    /// <seealso cref="BasePermissionsManager{TConfig, T}" />
    /// <seealso cref="IQueryFilteringPermissionsManager{T}" />
    public class RoleRelationPermissionsManager<T, TKey> : BasePermissionsManager<RoleRelationPermissionsManagerConfiguration<T, TKey>, T>, IQueryFilteringPermissionsManager<T>
        where TKey : IEquatable<TKey>
    {
        private readonly IAuthenticationStatusService authenticationStatus;
        private readonly IPrincipalUserAccessorService principalUserAccessor;
        private readonly IAuthenticatedUserIdAccessorService<TKey> authenticatedUserIdAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRelationPermissionsManager{T, TKey}"/> class.
        /// </summary>
        /// <param name="hub">The permissions hub.</param>
        /// <param name="parentManager">The parent permissions manager.</param>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="configuration">The manager configuration.</param>
        public RoleRelationPermissionsManager(
            IPermissionsHub hub,
            ICorePermissionsManager parentManager,
            PermissionsNamespace permissionsNamespace,
            IServiceProvider serviceProvider,
            RoleRelationPermissionsManagerConfiguration<T, TKey> configuration)
            : base(hub, parentManager, permissionsNamespace, serviceProvider, configuration)
        {
            this.authenticationStatus = (IAuthenticationStatusService)this.ServiceProvider.GetService(typeof(IAuthenticationStatusService));
            this.principalUserAccessor = (IPrincipalUserAccessorService)this.ServiceProvider.GetService(typeof(IPrincipalUserAccessorService));
            this.authenticatedUserIdAccessor = (IAuthenticatedUserIdAccessorService<TKey>)this.ServiceProvider.GetService(typeof(IAuthenticatedUserIdAccessorService<TKey>));
        }

        /// <inheritdoc />
        public async Task<IQueryable<T>> ApplyQueryFilterAsync(IQueryable<T> query, Permission permission)
        {
            this.ValidatePermissionSupport(permission);
            var overrideResult = await this.CheckQueryPermissionOverrideAsync(permission);
            if (overrideResult == PermissionsResult.Allow)
            {
                return query;
            }

            var entries = this.Configuration.GetEntriesForPermission(permission).OrderBy(x => x.Priority).ToList();
            if (entries.Any(x => x.RequiredRoles == null && x.Relation == null))
            {
                return query;
            }

            var isAuthenticated = await this.authenticationStatus.GetAuthenticationStatusAsync();
            var principal = await this.principalUserAccessor.GetPrincipalUserAsync();

            if (!isAuthenticated || principal == null)
            {
                return query.Where(x => false);
            }

            var roleOnlyEntries = entries.Where(x => x.RequiredRoles != null && x.Relation == null);
            foreach (var entry in roleOnlyEntries)
            {
                var accepted = entry.RequiredRoles.All(x => principal.IsInRole(x));
                if (accepted)
                {
                    return query;
                }
            }

            var relations = new List<UserRelation<T, TKey>>();
            var roleAndRelationEntries = entries.Where(x => x.RequiredRoles != null && x.Relation != null);
            foreach (var entry in roleAndRelationEntries)
            {
                var accepted = entry.RequiredRoles.All(x => principal.IsInRole(x));
                if (accepted)
                {
                    relations.Add(entry.Relation);
                }
            }

            var relationOnlyEntries = entries.Where(x => x.RequiredRoles == null && x.Relation != null);
            relations.AddRange(relationOnlyEntries.Select(x => x.Relation));

            if (relations.Any())
            {
                var userId = await this.authenticatedUserIdAccessor.GetUserIdAsync();
                var expression = UserRelation.CombineExpressions(relations.Select(x => x.BuildExpression(userId)));

                return query.Where(expression);
            }

            return query.Where(x => false);
        }

        /// <inheritdoc />
        public async Task<IQueryable<T>> ApplyQueryFilterAsync(IQueryable<T> query, IEnumerable<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                query = await this.ApplyQueryFilterAsync(query, permission);
            }

            return query;
        }

        /// <inheritdoc />
        protected override async Task<PermissionsResult> CheckExplicitPermissionAsync(T securedObject, Permission permission)
        {
            var entries = this.Configuration.GetEntriesForPermission(permission).OrderBy(x => x.Priority).ToList();
            if (entries.Any(x => x.RequiredRoles == null && x.Relation == null))
            {
                return PermissionsResult.Allow;
            }

            var isAuthenticated = await this.authenticationStatus.GetAuthenticationStatusAsync();
            var principal = await this.principalUserAccessor.GetPrincipalUserAsync();

            if (isAuthenticated && principal != null)
            {
                var userId = await this.authenticatedUserIdAccessor.GetUserIdAsync();

                foreach (var entry in entries)
                {
                    var rolesPass = entry.RequiredRoles == null || entry.RequiredRoles.All(x => principal.IsInRole(x));
                    var relationPass = entry.Relation == null || entry.Relation.TestUser(securedObject, userId);

                    if (rolesPass && relationPass)
                    {
                        return PermissionsResult.Allow;
                    }
                }
            }

            return PermissionsResult.Undefined;
        }
    }
}

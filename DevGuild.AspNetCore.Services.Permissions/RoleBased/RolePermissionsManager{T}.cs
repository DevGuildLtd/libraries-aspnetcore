using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Identity;
using DevGuild.AspNetCore.Services.Permissions.Base;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.RoleBased
{
    /// <summary>
    /// Represents role-based permissions manager.
    /// </summary>
    /// <typeparam name="T">Type of the secured object.</typeparam>
    /// <seealso cref="BasePermissionsManager{TConfig, T}" />
    /// <seealso cref="IQueryFilteringPermissionsManager{T}" />
    public class RolePermissionsManager<T> : BasePermissionsManager<RolePermissionsManagerConfiguration, T>, IQueryFilteringPermissionsManager<T>
    {
        private readonly IAuthenticationStatusService authenticationStatus;
        private readonly IPrincipalUserAccessorService principalUserAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolePermissionsManager{T}"/> class.
        /// </summary>
        /// <param name="hub">The permissions hub.</param>
        /// <param name="parentManager">The parent permissions manager.</param>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="configuration">The manager configuration.</param>
        public RolePermissionsManager(
            IPermissionsHub hub,
            ICorePermissionsManager parentManager,
            PermissionsNamespace permissionsNamespace,
            IServiceProvider serviceProvider,
            RolePermissionsManagerConfiguration configuration)
            : base(hub, parentManager, permissionsNamespace, serviceProvider, configuration)
        {
            this.authenticationStatus = (IAuthenticationStatusService)this.ServiceProvider.GetService(typeof(IAuthenticationStatusService));
            this.principalUserAccessor = (IPrincipalUserAccessorService)this.ServiceProvider.GetService(typeof(IPrincipalUserAccessorService));
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

            if (this.Configuration.AnonymousPermissions.Contains(permission))
            {
                return query;
            }

            var isAuthenticated = await this.authenticationStatus.GetAuthenticationStatusAsync();
            var principal = await this.principalUserAccessor.GetPrincipalUserAsync();

            if (isAuthenticated && principal != null)
            {
                var entries = this.Configuration.GetEntriesForPermission(permission);
                foreach (var entry in entries)
                {
                    var accepted = entry.RequiredRoles.All(x => principal.IsInRole(x));
                    if (accepted)
                    {
                        return query;
                    }
                }
            }

            return query.Where(x => false);
        }

        /// <inheritdoc />
        public async Task<IQueryable<T>> ApplyQueryFilterAsync(IQueryable<T> query, IEnumerable<Permission> permissions)
        {
            var permissionsList = permissions as IList<Permission> ?? permissions.ToList();
            this.ValidatePermissionsSupport(permissionsList);

            var isAuthenticated = await this.authenticationStatus.GetAuthenticationStatusAsync();
            var principal = await this.principalUserAccessor.GetPrincipalUserAsync();

            foreach (var permission in permissionsList)
            {
                var result = PermissionsResult.Undefined;

                var overrideResult = await this.CheckQueryPermissionOverrideAsync(permission);
                if (overrideResult == PermissionsResult.Allow)
                {
                    result = PermissionsResult.Allow;
                }

                if (this.Configuration.AnonymousPermissions.Contains(permission))
                {
                    result = PermissionsResult.Allow;
                }

                if (isAuthenticated && principal != null)
                {
                    var entries = this.Configuration.GetEntriesForPermission(permission);
                    foreach (var entry in entries)
                    {
                        var accepted = entry.RequiredRoles.All(x => principal.IsInRole(x));
                        if (accepted)
                        {
                            result = PermissionsResult.Allow;
                        }
                    }
                }

                if (result == PermissionsResult.Undefined)
                {
                    return query.Where(x => false);
                }
            }

            return query;
        }

        /// <inheritdoc />
        protected override async Task<PermissionsResult> CheckExplicitPermissionAsync(T securedObject, Permission permission)
        {
            if (this.Configuration.AnonymousPermissions.Contains(permission))
            {
                return PermissionsResult.Allow;
            }

            var isAuthenticated = await this.authenticationStatus.GetAuthenticationStatusAsync();
            var principal = await this.principalUserAccessor.GetPrincipalUserAsync();

            if (isAuthenticated && principal != null)
            {
                var entries = this.Configuration.GetEntriesForPermission(permission);
                foreach (var entry in entries)
                {
                    var accepted = entry.RequiredRoles.All(x => principal.IsInRole(x));
                    if (accepted)
                    {
                        return PermissionsResult.Allow;
                    }
                }
            }

            return PermissionsResult.Undefined;
        }
    }
}

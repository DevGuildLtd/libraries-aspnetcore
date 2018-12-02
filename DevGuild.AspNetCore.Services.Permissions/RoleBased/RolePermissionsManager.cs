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
    /// <seealso cref="BasePermissionsManager{TConfig}" />
    public class RolePermissionsManager : BasePermissionsManager<RolePermissionsManagerConfiguration>
    {
        private readonly IAuthenticationStatusService authenticationStatus;
        private readonly IPrincipalUserAccessorService principalUserAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolePermissionsManager"/> class.
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
        protected override async Task<PermissionsResult> CheckExplicitPermissionAsync(Permission permission)
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

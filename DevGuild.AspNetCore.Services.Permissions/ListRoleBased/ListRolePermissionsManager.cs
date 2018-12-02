using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Identity;
using DevGuild.AspNetCore.Services.Permissions.ListBased;
using DevGuild.AspNetCore.Services.Permissions.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Permissions.ListRoleBased
{
    /// <summary>
    /// Represents list and role-based permissions manager.
    /// </summary>
    /// <typeparam name="T">Type of the secured object.</typeparam>
    /// <seealso cref="ListPermissionsManager{TSecuredObject,TAuthorizationConfiguration}" />
    public class ListRolePermissionsManager<T> : ListPermissionsManager<T, RoleList>
        where T : IEquatable<T>
    {
        private readonly IAuthenticationStatusService authenticationStatus;
        private readonly IPrincipalUserAccessorService principalUserAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListRolePermissionsManager{T}"/> class.
        /// </summary>
        /// <param name="hub">The permissions hub.</param>
        /// <param name="parentManager">The parent permissions manager.</param>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="configuration">The manager configuration.</param>
        public ListRolePermissionsManager(
            IPermissionsHub hub,
            ICorePermissionsManager parentManager,
            PermissionsNamespace permissionsNamespace,
            IServiceProvider serviceProvider,
            ListPermissionsManagerConfiguration<T, RoleList> configuration)
            : base(hub, parentManager, permissionsNamespace, serviceProvider, configuration)
        {

            this.authenticationStatus = this.ServiceProvider.GetService<IAuthenticationStatusService>();
            this.principalUserAccessor = this.ServiceProvider.GetService<IPrincipalUserAccessorService>();
        }

        /// <inheritdoc />
        protected override async Task<PermissionsResult> ValidateExceptionAsync(ListPermissionsManagerDefaultBehavior defaultBehavior, T securedObject, Permission permission, RoleList authorizationConfiguration)
        {
            var isAuthenticated = await this.authenticationStatus.GetAuthenticationStatusAsync();
            if (!isAuthenticated)
            {
                if (authorizationConfiguration.AllowAnonymous)
                {
                    return PermissionsResult.Allow;
                }
                else
                {
                    return PermissionsResult.Undefined;
                }
            }

            var principal = await this.principalUserAccessor.GetPrincipalUserAsync();
            if (principal != null && authorizationConfiguration.Roles.All(role => principal.IsInRole(role)))
            {
                return PermissionsResult.Allow;
            }

            return PermissionsResult.Undefined;
        }
    }
}

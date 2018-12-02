using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions.Base;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.ListBased
{
    /// <summary>
    /// Repesents base list-based permissions manager.
    /// </summary>
    /// <typeparam name="TSecuredObject">The type of the secured object.</typeparam>
    /// <typeparam name="TAuthorizationConfiguration">The type of the authorization configuration.</typeparam>
    /// <seealso cref="BasePermissionsManager{TConfig, TSecuredObject}" />
    public abstract class ListPermissionsManager<TSecuredObject, TAuthorizationConfiguration> : BasePermissionsManager<ListPermissionsManagerConfiguration<TSecuredObject, TAuthorizationConfiguration>, TSecuredObject>
        where TSecuredObject : IEquatable<TSecuredObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListPermissionsManager{TSecuredObject, TAuthorizationConfiguration}"/> class.
        /// </summary>
        /// <param name="hub">The permissions hub.</param>
        /// <param name="parentManager">The parent permissions manager.</param>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="configuration">The manager configuration.</param>
        protected ListPermissionsManager(
            IPermissionsHub hub,
            ICorePermissionsManager parentManager,
            PermissionsNamespace permissionsNamespace,
            IServiceProvider serviceProvider,
            ListPermissionsManagerConfiguration<TSecuredObject, TAuthorizationConfiguration> configuration)
            : base(hub, parentManager, permissionsNamespace, serviceProvider, configuration)
        {
        }

        /// <inheritdoc />
        protected override async Task<PermissionsResult> CheckExplicitPermissionAsync(TSecuredObject securedObject, Permission permission)
        {
            var exceptions = this.Configuration.GetExceptionsFor(securedObject, permission).ToList();
            switch (this.Configuration.DefaultBehavior)
            {
                case ListPermissionsManagerDefaultBehavior.Allow:
                    if (exceptions.Count == 0)
                    {
                        return PermissionsResult.Allow;
                    }

                    foreach (var exception in exceptions)
                    {
                        var exceptionResult = await this.ValidateExceptionAsync(ListPermissionsManagerDefaultBehavior.Allow, securedObject, permission, exception.Configuration);
                        if (exceptionResult == PermissionsResult.Allow)
                        {
                            return PermissionsResult.Allow;
                        }
                    }

                    break;
                case ListPermissionsManagerDefaultBehavior.Deny:
                    foreach (var exception in exceptions)
                    {
                        var exceptionResult = await this.ValidateExceptionAsync(ListPermissionsManagerDefaultBehavior.Deny, securedObject, permission, exception.Configuration);
                        if (exceptionResult == PermissionsResult.Allow)
                        {
                            return PermissionsResult.Allow;
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return PermissionsResult.Undefined;
        }

        /// <summary>
        /// Asynchronously validates the authorization exception.
        /// </summary>
        /// <param name="defaultBehavior">The default behavior.</param>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permission">The permission.</param>
        /// <param name="authorizationConfiguration">The authorization configuration.</param>
        /// <returns>A result of permissions check.</returns>
        protected abstract Task<PermissionsResult> ValidateExceptionAsync(ListPermissionsManagerDefaultBehavior defaultBehavior, TSecuredObject securedObject, Permission permission, TAuthorizationConfiguration authorizationConfiguration);
    }
}

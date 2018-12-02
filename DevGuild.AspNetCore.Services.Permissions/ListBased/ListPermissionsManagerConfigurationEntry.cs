using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions.ListBased
{
    /// <summary>
    /// Represents list-based permissions manager configuration entry.
    /// </summary>
    /// <typeparam name="TSecuredObject">The type of the secured object.</typeparam>
    /// <typeparam name="TAuthorizationConfiguration">The type of the authorization configuration.</typeparam>
    public class ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>
        where TSecuredObject : IEquatable<TSecuredObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListPermissionsManagerConfigurationEntry{TSecuredObject, TAuthorizationConfiguration}"/> class.
        /// </summary>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permission">The permission.</param>
        /// <param name="configuration">The authorization configuration.</param>
        public ListPermissionsManagerConfigurationEntry(TSecuredObject securedObject, Permission permission, TAuthorizationConfiguration configuration)
        {
            this.SecuredObject = securedObject;
            this.Permission = permission;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the secured object.
        /// </summary>
        /// <value>
        /// The secured object.
        /// </value>
        public TSecuredObject SecuredObject { get; }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public Permission Permission { get; }

        /// <summary>
        /// Gets the authorization configuration.
        /// </summary>
        /// <value>
        /// The authorization configuration.
        /// </value>
        public TAuthorizationConfiguration Configuration { get; }
    }
}

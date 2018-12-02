using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;
using DevGuild.AspNetCore.Services.Permissions.Override;

namespace DevGuild.AspNetCore.Services.Permissions.ListBased
{
    /// <summary>
    /// Represents list-based permissions manager configuration.
    /// </summary>
    /// <typeparam name="TSecuredObject">The type of the secured object.</typeparam>
    /// <typeparam name="TAuthorizationConfiguration">The type of the authorization configuration.</typeparam>
    /// <seealso cref="IOverridablePermissionsManagerConfiguration" />
    public class ListPermissionsManagerConfiguration<TSecuredObject, TAuthorizationConfiguration> : IOverridablePermissionsManagerConfiguration
        where TSecuredObject : IEquatable<TSecuredObject>
    {
        private readonly List<ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>> exceptions;
        private readonly PermissionsOverrideConfiguration overrideConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPermissionsManagerConfiguration{TSecuredObject, TAuthorizationConfiguration}"/> class.
        /// </summary>
        /// <param name="defaultBehavior">The default behavior.</param>
        /// <param name="exceptions">The exceptions.</param>
        /// <param name="overrideMode">The override mode.</param>
        /// <param name="overrideConfiguration">The override configuration.</param>
        public ListPermissionsManagerConfiguration(
            ListPermissionsManagerDefaultBehavior defaultBehavior,
            List<ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>> exceptions,
            PermissionsOverrideMode overrideMode,
            PermissionsOverrideConfiguration overrideConfiguration)
        {
            this.DefaultBehavior = defaultBehavior;
            this.OverrideMode = overrideMode;
            this.exceptions = exceptions;
            this.overrideConfiguration = overrideConfiguration;
        }

        /// <summary>
        /// Gets the default behavior.
        /// </summary>
        /// <value>
        /// The default behavior.
        /// </value>
        public ListPermissionsManagerDefaultBehavior DefaultBehavior { get; }

        /// <inheritdoc />
        public PermissionsOverrideMode OverrideMode { get; }

        /// <summary>
        /// Gets the exceptions for the specified secured object and permission.
        /// </summary>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permission">The permission.</param>
        /// <returns>A collection of the exceptions.</returns>
        public IEnumerable<ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>> GetExceptionsFor(TSecuredObject securedObject, Permission permission)
        {
            return this.exceptions.Where(x => x.SecuredObject.Equals(securedObject) && x.Permission.Equals(permission));
        }

        /// <inheritdoc />
        public IEnumerable<PermissionsOverrideConfigurationEntry> GetOverridesForPermission(Permission permission)
        {
            return this.overrideConfiguration.GetOverridesForPermission(permission);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;
using DevGuild.AspNetCore.Services.Permissions.Override;

namespace DevGuild.AspNetCore.Services.Permissions.ListBased
{
    /// <summary>
    /// Represents list-based permissions manager configuration builder.
    /// </summary>
    /// <typeparam name="TSecuredObject">The type of the secured object.</typeparam>
    /// <typeparam name="TAuthorizationConfiguration">The type of the authorization configuration.</typeparam>
    public class ListPermissionsManagerConfigurationBuilder<TSecuredObject, TAuthorizationConfiguration>
        where TSecuredObject : IEquatable<TSecuredObject>
    {
        private readonly List<ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>> entries = new List<ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>>();
        private ListPermissionsManagerDefaultBehavior defaultBehavior = ListPermissionsManagerDefaultBehavior.Deny;
        private PermissionsOverrideConfiguration overrideConfiguration;
        private PermissionsOverrideMode overrideMode;

        /// <summary>
        /// Sets the default behavior.
        /// </summary>
        /// <param name="defaultBehavior">The default behavior.</param>
        /// <returns>An instance of this builder.</returns>
        public ListPermissionsManagerConfigurationBuilder<TSecuredObject, TAuthorizationConfiguration> SetDefaultBehavior(ListPermissionsManagerDefaultBehavior defaultBehavior)
        {
            this.defaultBehavior = defaultBehavior;
            return this;
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="securedObjects">The secured objects.</param>
        /// <param name="permission">The permission.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ListPermissionsManagerConfigurationBuilder<TSecuredObject, TAuthorizationConfiguration> AddException(TSecuredObject[] securedObjects, Permission permission, TAuthorizationConfiguration configuration)
        {
            foreach (var securedObject in securedObjects)
            {
                this.entries.Add(new ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>(securedObject, permission, configuration));
            }

            return this;
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="securedObjects">The secured objects.</param>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ListPermissionsManagerConfigurationBuilder<TSecuredObject, TAuthorizationConfiguration> AddException(TSecuredObject[] securedObjects, PermissionsNamespace permissionsNamespace, TAuthorizationConfiguration configuration)
        {
            foreach (var securedObject in securedObjects)
            {
                foreach (var permission in permissionsNamespace.Permissions)
                {
                    this.entries.Add(new ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>(securedObject, permission, configuration));
                }
            }

            return this;
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permission">The permission.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ListPermissionsManagerConfigurationBuilder<TSecuredObject, TAuthorizationConfiguration> AddException(TSecuredObject securedObject, Permission permission, TAuthorizationConfiguration configuration)
        {
            this.entries.Add(new ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>(securedObject, permission, configuration));
            return this;
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permissionsNamespace">The permissions namespace.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ListPermissionsManagerConfigurationBuilder<TSecuredObject, TAuthorizationConfiguration> AddException(TSecuredObject securedObject, PermissionsNamespace permissionsNamespace, TAuthorizationConfiguration configuration)
        {
            foreach (var permission in permissionsNamespace.Permissions)
            {
                this.entries.Add(new ListPermissionsManagerConfigurationEntry<TSecuredObject, TAuthorizationConfiguration>(securedObject, permission, configuration));
            }

            return this;
        }

        /// <summary>
        /// Configures the overrides.
        /// </summary>
        /// <param name="overrideMode">The override mode.</param>
        /// <param name="configure">The configuration.</param>
        /// <returns>An instance of this builder.</returns>
        public ListPermissionsManagerConfigurationBuilder<TSecuredObject, TAuthorizationConfiguration> ConfigureOverrides(PermissionsOverrideMode overrideMode, Func<PermissionsOverrideConfigurationBuilder, PermissionsOverrideConfigurationBuilder> configure)
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
        /// Builds the configuration.
        /// </summary>
        /// <returns>List-based permissions manager configuration.</returns>
        public ListPermissionsManagerConfiguration<TSecuredObject, TAuthorizationConfiguration> BuildConfiguration()
        {
            return new ListPermissionsManagerConfiguration<TSecuredObject, TAuthorizationConfiguration>(this.defaultBehavior, this.entries, this.overrideMode, this.overrideConfiguration);
        }
    }
}

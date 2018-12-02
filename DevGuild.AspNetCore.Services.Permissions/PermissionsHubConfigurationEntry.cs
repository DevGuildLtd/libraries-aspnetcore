using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Represents permissions hub configuration entry.
    /// </summary>
    public sealed class PermissionsHubConfigurationEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsHubConfigurationEntry"/> class.
        /// </summary>
        /// <param name="path">The path to the permissions manager.</param>
        /// <param name="constructor">The permissions manager constructor.</param>
        /// <param name="ns">The permissions namespace.</param>
        public PermissionsHubConfigurationEntry(String path, PermissionsManagerConstructor constructor, PermissionsNamespace ns)
        {
            this.Path = path;
            this.Constructor = constructor;
            this.Namespace = ns;
        }

        /// <summary>
        /// Gets the path to the permissions manager.
        /// </summary>
        /// <value>
        /// The path to the permissions manager.
        /// </value>
        public String Path { get; }

        /// <summary>
        /// Gets the permissions manager constructor.
        /// </summary>
        /// <value>
        /// The permissions manager constructor.
        /// </value>
        public PermissionsManagerConstructor Constructor { get; }

        /// <summary>
        /// Gets the permissions namespace.
        /// </summary>
        /// <value>
        /// The permissions namespace.
        /// </value>
        public PermissionsNamespace Namespace { get; }
    }
}

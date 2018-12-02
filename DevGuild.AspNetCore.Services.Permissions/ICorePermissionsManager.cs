using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Base interface for all permissions managers interfaces. Name is subject to change.
    /// </summary>
    public interface ICorePermissionsManager
    {
        /// <summary>
        /// Gets the hub that contains this manager.
        /// </summary>
        /// <value>
        /// The hub that contains this manager.
        /// </value>
        IPermissionsHub Hub { get; }

        /// <summary>
        /// Gets the permissions manager of the parent security scope.
        /// </summary>
        /// <value>
        /// The permissions manager of the parent security scope.
        /// </value>
        ICorePermissionsManager ParentManager { get; }

        /// <summary>
        /// Gets the permissions namespace of this permissions manager.
        /// </summary>
        /// <value>
        /// The permissions namespace of this permissions manager.
        /// </value>
        PermissionsNamespace PermissionsNamespace { get; }
    }
}

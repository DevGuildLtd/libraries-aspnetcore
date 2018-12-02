using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Represents permissions manager constructor.
    /// </summary>
    /// <param name="hub">The permissions hub.</param>
    /// <param name="parent">The parent permissions manager.</param>
    /// <param name="permissionsNamespace">The permissions namespace.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>Created permissions manager.</returns>
    public delegate ICorePermissionsManager PermissionsManagerConstructor(IPermissionsHub hub, ICorePermissionsManager parent, PermissionsNamespace permissionsNamespace, IServiceProvider serviceProvider);
}

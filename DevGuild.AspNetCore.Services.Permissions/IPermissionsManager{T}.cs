using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Provides ability to check user permissions for specific object in specific security scope.
    /// </summary>
    /// <typeparam name="T">Type of the secured objects.</typeparam>
    /// <seealso cref="ICorePermissionsManager" />
    public interface IPermissionsManager<in T> : ICorePermissionsManager
    {
        /// <summary>
        /// Asynchronously checks that current user has provided permission for provided object in manager's scope.
        /// </summary>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permission">The permission to be checked.</param>
        /// <returns>Task that represents asynchronous operation and contains permission check result.</returns>
        Task<PermissionsResult> CheckPermissionAsync(T securedObject, Permission permission);

        /// <summary>
        /// Asynchronously checks that current user has provided permissions for provided object in manager's scope.
        /// </summary>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permissions">The permissions to be checked.</param>
        /// <returns>Task that represents asynchronous operation and contains permissions check result.</returns>
        Task<PermissionsResult> CheckPermissionsAsync(T securedObject, IEnumerable<Permission> permissions);

        /// <summary>
        /// Asynchronously demands that current user has provided permission for provided object and throws exception if he/she does not.
        /// </summary>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permission">The permission to be checked.</param>
        /// <returns>Task that represents asynchronous operation.</returns>
        Task DemandPermissionAsync(T securedObject, Permission permission);

        /// <summary>
        /// Asynchronously demands that current user has provided permissions for provided object and throws exception if he/she does not.
        /// </summary>
        /// <param name="securedObject">The secured object.</param>
        /// <param name="permissions">The permissions to be checked.</param>
        /// <returns>Task that represents asynchronous operation.</returns>
        Task DemandPermissionsAsync(T securedObject, IEnumerable<Permission> permissions);
    }
}

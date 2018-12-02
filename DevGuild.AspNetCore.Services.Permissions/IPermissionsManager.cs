using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Provides ability to check user permissions in specific security scope.
    /// </summary>
    public interface IPermissionsManager : ICorePermissionsManager
    {
        /// <summary>
        /// Asynchronously checks that current user has provided permission in manager's scope.
        /// </summary>
        /// <param name="permission">The permission to be checked.</param>
        /// <returns>Task that represents asynchronous operation and contains permission check result.</returns>
        Task<PermissionsResult> CheckPermissionAsync(Permission permission);

        /// <summary>
        /// Asynchronously checks that current user has provided permissions in manager's scope.
        /// </summary>
        /// <param name="permissions">The permissions to be checked.</param>
        /// <returns>Task that represents asynchronous operation and contains permissions check result.</returns>
        Task<PermissionsResult> CheckPermissionsAsync(IEnumerable<Permission> permissions);

        /// <summary>
        /// Asynchronously demands that current user has provided permission and throws exception if he/she does not.
        /// </summary>
        /// <param name="permission">The permission to be checked.</param>
        /// <returns>Task that represents asynchronous operation.</returns>
        Task DemandPermissionAsync(Permission permission);

        /// <summary>
        /// Asynchronously demands that current user has provided permissions and throws exception if he/she does not.
        /// </summary>
        /// <param name="permissions">The permissions to be checked.</param>
        /// <returns>Task that represents asynchronous operation.</returns>
        Task DemandPermissionsAsync(IEnumerable<Permission> permissions);
    }
}

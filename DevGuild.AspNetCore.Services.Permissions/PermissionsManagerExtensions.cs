using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Contains extensions for the permissions manager.
    /// </summary>
    public static class PermissionsManagerExtensions
    {
        /// <summary>
        /// Asynchronously determines whether the current user has the specified permission.
        /// </summary>
        /// <param name="manager">The permissions manager.</param>
        /// <param name="permission">The required permission.</param>
        /// <returns>A task that represents the operation.</returns>
        public static Task<Boolean> HasPermissionAsync(this IPermissionsManager manager, Permission permission)
        {
            return manager.CheckPermissionAsync(permission).Then(x => x == PermissionsResult.Allow);
        }

        /// <summary>
        /// Asynchronously demands the current user has specified the permission.
        /// </summary>
        /// <param name="manager">The permissions manager.</param>
        /// <param name="permission">The required permission.</param>
        /// <returns>A task that represents the operation.</returns>
        public static Task DemandPermissionOrDefaultAsync(this IPermissionsManager manager, Permission permission)
        {
            if (manager != null)
            {
                return manager.DemandPermissionAsync(permission);
            }
            else
            {
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Asynchronously demands the current user has specified the permission for the specified entity.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <param name="manager">The permisions manager.</param>
        /// <param name="entity">The secured entity.</param>
        /// <param name="permission">The required permission.</param>
        /// <returns>A task that represents the operation.</returns>
        public static Task DemandPermissionOrDefaultAsync<T>(this IPermissionsManager<T> manager, T entity, Permission permission)
        {
            if (manager != null)
            {
                return manager.DemandPermissionAsync(entity, permission);
            }
            else
            {
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Asynchronously applies the permissions query filter.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <param name="manager">The permissions manager.</param>
        /// <param name="query">The query.</param>
        /// <param name="permission">The required permission.</param>
        /// <returns>A task that represents the operation.</returns>
        public static Task<IQueryable<T>> ApplyQueryFilterOrDefaultAsync<T>(this IPermissionsManager<T> manager, IQueryable<T> query, Permission permission)
        {
            if (manager is IQueryFilteringPermissionsManager<T> filteringManager)
            {
                return filteringManager.ApplyQueryFilterAsync(query, permission);
            }
            else
            {
                return Task.FromResult(query);
            }
        }
    }
}

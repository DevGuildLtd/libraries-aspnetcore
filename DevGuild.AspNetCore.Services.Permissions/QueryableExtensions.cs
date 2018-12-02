using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Contains permissions-based query filters.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Asynchronously applies permissions requirements to the provided query, or returns original query if permissions manager does not implement required interface.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <param name="query">The query that need to be filtered.</param>
        /// <param name="permissionsManager">The permissions manager to be used.</param>
        /// <param name="permission">The required permission.</param>
        /// <returns>A filtered query.</returns>
        public static Task<IQueryable<T>> RequirePermissionAsync<T>(this IQueryable<T> query, IPermissionsManager<T> permissionsManager, Permission permission)
        {
            if (permissionsManager is IQueryFilteringPermissionsManager<T> filteringManager)
            {
                return filteringManager.ApplyQueryFilterAsync(query, permission);
            }

            return Task.FromResult(query);
        }

        /// <summary>
        /// Asynchronously applies permissions requirements to the provided query, or returns original query if permissions manager does not implement required interface.
        /// </summary>
        /// <typeparam name="T">Type of the entity</typeparam>
        /// <param name="query">The query that need to be filtered.</param>
        /// <param name="permissionsManager">The permissions manager to be used.</param>
        /// <param name="permissions">The required permissions.</param>
        /// <returns>A filtered query.</returns>
        public static Task<IQueryable<T>> RequirePermissionsAsync<T>(this IQueryable<T> query, IPermissionsManager<T> permissionsManager, IEnumerable<Permission> permissions)
        {
            if (permissionsManager is IQueryFilteringPermissionsManager<T> filteringManager)
            {
                return filteringManager.ApplyQueryFilterAsync(query, permissions);
            }

            return Task.FromResult(query);
        }

        /// <summary>
        /// Asynchronously applies permissions requirements to the provided query, or returns original query if permissions manager does not implement required interface.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <param name="query">The query that need to be filtered.</param>
        /// <param name="permissionsManager">The permissions manager.</param>
        /// <param name="permission">The required permission.</param>
        /// <returns>A filtered query.</returns>
        public static async Task<IQueryable<T>> RequirePermissionAsync<T>(this Task<IQueryable<T>> query, IPermissionsManager<T> permissionsManager, Permission permission)
        {
            var awaitedQuery = await query;
            if (permissionsManager is IQueryFilteringPermissionsManager<T> filteringManager)
            {
                return await filteringManager.ApplyQueryFilterAsync(awaitedQuery, permission);
            }

            return awaitedQuery;
        }

        /// <summary>
        /// Asynchronously applies permissions requirements to the provided query, or returns original query if permissions manager does not implement required interface.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <param name="query">The query that need to be filtered.</param>
        /// <param name="permissionsManager">The permissions manager.</param>
        /// <param name="permissions">The required permissions.</param>
        /// <returns>A filtered query.</returns>
        public static async Task<IQueryable<T>> RequirePermissionsAsync<T>(this Task<IQueryable<T>> query, IPermissionsManager<T> permissionsManager, IEnumerable<Permission> permissions)
        {
            var awaitedQuery = await query;
            if (permissionsManager is IQueryFilteringPermissionsManager<T> filteringManager)
            {
                return await filteringManager.ApplyQueryFilterAsync(awaitedQuery, permissions);
            }

            return awaitedQuery;
        }
    }
}

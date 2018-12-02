using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Provides ability to check user permissions for queried objects and filter query accordingly.
    /// </summary>
    /// <typeparam name="T">Type of the secured objects.</typeparam>
    public interface IQueryFilteringPermissionsManager<T> : ICorePermissionsManager
    {
        /// <summary>
        /// Asynchronously applies the query filter to the provided query with requirements for provided permission.
        /// </summary>
        /// <param name="query">The query that need to be filtered.</param>
        /// <param name="permission">The permission to be checked.</param>
        /// <returns>Task that represents asynchronous operation and contains filtered query as a result.</returns>
        Task<IQueryable<T>> ApplyQueryFilterAsync(IQueryable<T> query, Permission permission);

        /// <summary>
        /// Asynchronously applies the query filter to the provided query with requirements for provided permission.
        /// </summary>
        /// <param name="query">The query that need to be filtered.</param>
        /// <param name="permissions">The permissions to be checked.</param>
        /// <returns>Task that represents asynchronous operation and contains filtered query as a result.</returns>
        Task<IQueryable<T>> ApplyQueryFilterAsync(IQueryable<T> query, IEnumerable<Permission> permissions);
    }
}

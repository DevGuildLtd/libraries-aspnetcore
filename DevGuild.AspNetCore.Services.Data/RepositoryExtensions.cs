using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Data
{
    /// <summary>
    /// Contains repository extensions.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Queries the repository.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <returns>A query.</returns>
        public static IQueryable<T> Query<T>(this IRepository repository)
            where T : class
        {
            return repository.GetEntityStore<T>().Query();
        }

        /// <summary>
        /// Queries the repository.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="includes">The paths to be included.</param>
        /// <returns>A query.</returns>
        public static IQueryable<T> Query<T>(this IRepository repository, params String[] includes)
            where T : class
        {
            return repository.GetEntityStore<T>().Query(includes);
        }

        /// <summary>
        /// Queries the repository without tracking.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <returns>A query.</returns>
        public static IQueryable<T> QueryWithoutTracking<T>(this IRepository repository)
            where T : class
        {
            return repository.GetEntityStore<T>().QueryWithoutTracking();
        }

        /// <summary>
        /// Queries the repository without tracking.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="includes">The paths to be included.</param>
        /// <returns>A query.</returns>
        public static IQueryable<T> QueryWithoutTracking<T>(this IRepository repository, params String[] includes)
            where T : class
        {
            return repository.GetEntityStore<T>().QueryWithoutTracking(includes);
        }

        /// <summary>
        /// Asynchronously inserts the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents insert operation.</returns>
        public static Task InsertAsync<T>(this IRepository repository, T entity)
            where T : class
        {
            return repository.GetEntityStore<T>().InsertAsync(entity);
        }

        /// <summary>
        /// Asynchronously updates the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents update operation.</returns>
        public static Task UpdateAsync<T>(this IRepository repository, T entity)
            where T : class
        {
            return repository.GetEntityStore<T>().UpdateAsync(entity);
        }

        /// <summary>
        /// Asynchronously deletes the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents delete operation.</returns>
        public static Task DeleteAsync<T>(this IRepository repository, T entity)
            where T : class
        {
            return repository.GetEntityStore<T>().DeleteAsync(entity);
        }
    }
}

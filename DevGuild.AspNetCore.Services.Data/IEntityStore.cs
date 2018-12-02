using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Data
{
    /// <summary>
    /// Defines interface of an entity store.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IEntityStore<T>
        where T : class
    {
        /// <summary>
        /// Queries the entity store.
        /// </summary>
        /// <returns>A query.</returns>
        IQueryable<T> Query();

        /// <summary>
        /// Queries the entity store.
        /// </summary>
        /// <param name="includes">The paths to be included.</param>
        /// <returns>A query.</returns>
        IQueryable<T> Query(params String[] includes);

        /// <summary>
        /// Queries the entity store without tracking.
        /// </summary>
        /// <returns>A query.</returns>
        IQueryable<T> QueryWithoutTracking();

        /// <summary>
        /// Queries the entity store without tracking.
        /// </summary>
        /// <param name="includes">The paths to be included.</param>
        /// <returns>A query.</returns>
        IQueryable<T> QueryWithoutTracking(params String[] includes);

        /// <summary>
        /// Asynchronously inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents insert operation.</returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Asynchronously updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents update operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Asynchronously deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents delete operation.</returns>
        Task DeleteAsync(T entity);
    }
}

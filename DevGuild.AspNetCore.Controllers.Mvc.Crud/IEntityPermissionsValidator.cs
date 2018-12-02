using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud
{
    /// <summary>
    /// Defines interface for entity permissions validator.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IEntityPermissionsValidator<TEntity>
    {
        /// <summary>
        /// Asynchronously determines whether the current user has Index permissions.
        /// </summary>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanIndexAsync();

        /// <summary>
        /// Asynchronously determines whether the current user has Create permissions.
        /// </summary>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanCreateAsync();

        /// <summary>
        /// Asynchronously determines whether the current user has Details permissions for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanDetailsAsync(TEntity entity);

        /// <summary>
        /// Asynchronously determines whether the current user has Edit permissions for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanEditAsync(TEntity entity);

        /// <summary>
        /// Asynchronously determines whether the current user has Delete permissions for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanDeleteAsync(TEntity entity);

        /// <summary>
        /// Asynchronously applies Read permissions requirements to the provided query.
        /// </summary>
        /// <param name="query">The query that should be filtered.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<IQueryable<TEntity>> RequireReadAccessAsync(IQueryable<TEntity> query);

        /// <summary>
        /// Asynchronously demands that the current user has Index permissions.
        /// </summary>
        /// <returns>A task that represents the operation.</returns>
        Task DemandCanIndexAsync();

        /// <summary>
        /// Asynchronously demands that the current user has Create permissions.
        /// </summary>
        /// <returns>A task that represents the operation.</returns>
        Task DemandCanCreateAsync();

        /// <summary>
        /// Asynchronously demands that the current user has Details permissions for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        Task DemandCanDetailsAsync(TEntity entity);

        /// <summary>
        /// Asynchronously demands that the current user has Edit permissions for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        Task DemandCanEditAsync(TEntity entity);

        /// <summary>
        /// Asynchronously demands that the current user has Delete permissions for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        Task DemandCanDeleteAsync(TEntity entity);

        /// <summary>
        /// Asynchronously determines whether the current user has Read permissions for the specified property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanReadPropertyAsync(String propertyName);

        /// <summary>
        /// Asynchronously determines whether the current user has Initialize permissions for the specified property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanInitializePropertyAsync(String propertyName);

        /// <summary>
        /// Asynchronously determines whether the current user has Update permissions for the specified property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanUpdatePropertyAsync(String propertyName);
    }
}

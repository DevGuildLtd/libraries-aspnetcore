using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud
{
    /// <summary>
    /// Defines interface for hierarchical entity permissions validator.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="DevGuild.AspNet.Controllers.Mvc.Crud.IEntityPermissionsValidator{TEntity}" />
    public interface IHierarchicalEntityPermissionsValidator<TEntity> : IEntityPermissionsValidator<TEntity>
    {
        /// <summary>
        /// Asynchronously determines whether the current user has CreateChild permissions for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<Boolean> CanCreateChildAsync(TEntity entity);

        /// <summary>
        /// Asynchronously demands that the current user has CreateChild permissions for the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        Task DemandCanCreateChildAsync(TEntity entity);
    }
}

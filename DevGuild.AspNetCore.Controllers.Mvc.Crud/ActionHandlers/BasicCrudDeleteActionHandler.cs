using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic Delete action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDeleteModel">The type of the delete model.</typeparam>
    public class BasicCrudDeleteActionHandler<TIdentifier, TEntity, TDeleteModel> : BaseCrudActionHandler<TIdentifier, TEntity, BasicCrudDeleteActionOverrides<TIdentifier, TEntity, TDeleteModel>>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCrudDeleteActionHandler{TIdentifier, TEntity, TDeleteModel}"/> class.
        /// </summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicCrudDeleteActionHandler(Controller controller, IEntityControllerServices controllerServices, IEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
        }

        /// <summary>
        /// Gets the action handler overrides.
        /// </summary>
        /// <value>
        /// The action handler overrides.
        /// </value>
        public override BasicCrudDeleteActionOverrides<TIdentifier, TEntity, TDeleteModel> Overrides { get; } = new BasicCrudDeleteActionOverrides<TIdentifier, TEntity, TDeleteModel>();

        /// <summary>
        /// Handles the GET request for the Delete action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Delete action form.</returns>
        public async Task<IActionResult> Delete(TIdentifier id)
        {
            var entity = await this.QuerySingleEntityAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.PermissionsValidator.DemandCanDeleteAsync(entity);

            var model = await this.ConvertToDeleteModelAsync(entity);
            return this.View(model);
        }

        /// <summary>
        /// Handles the POST request for the Delete action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that redirects to another page on success or renders Delete action form again on failure.</returns>
        public async Task<IActionResult> DeleteConfirmed(TIdentifier id)
        {
            var entity = await this.QuerySingleEntityAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.PermissionsValidator.DemandCanDeleteAsync(entity);

            // Performing pre-deletion actions
            var additionalData = new Dictionary<String, Object>();
            await this.BeforeEntityDeletedAsync(entity, additionalData);

            // Deleting entity from repository
            await this.DeleteEntityAsync(entity, additionalData);

            // Performing post-deletion actions
            await this.AfterEntityDeletedAsync(entity, additionalData);

            return await this.GetDeleteSuccessResultAsync(entity, additionalData);
        }

        /// <summary>
        /// Asynchronously converts the specified entity to the delete model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation and contains converted delete model as a result.</returns>
        protected virtual Task<TDeleteModel> ConvertToDeleteModelAsync(TEntity entity)
        {
            if (this.Overrides.ConvertToDeleteModel != null)
            {
                return this.Overrides.ConvertToDeleteModel(entity);
            }

            async Task<TDeleteModel> DefaultImplementation()
            {
                var propertiesAllowedToRead = await this.GetAllowedEntityPropertiesAsync(EntityPermissions.EntityProperty.Read);
                return this.ControllerServices.MappingManager.GetModelMapper<TEntity, TDeleteModel>().ConvertEntityToViewModel(entity, propertiesAllowedToRead);
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously executes additional actions just before the entity is deleted from the entity store.
        /// </summary>
        /// <param name="entity">The deleted entity.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task BeforeEntityDeletedAsync(TEntity entity, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.BeforeEntityDeleted != null)
            {
                return this.Overrides.BeforeEntityDeleted(entity, additionalData);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously executes additional actions after the entity is deleted from the entity store.
        /// </summary>
        /// <param name="entity">The deleted entity.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task AfterEntityDeletedAsync(TEntity entity, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.AfterEntityDeleted != null)
            {
                return this.Overrides.AfterEntityDeleted(entity, additionalData);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously deletes the entity from the entity store.
        /// </summary>
        /// <param name="entity">The deleted entity.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task DeleteEntityAsync(TEntity entity, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.DeleteEntity != null)
            {
                return this.Overrides.DeleteEntity(entity, additionalData);
            }

            async Task DefaultImplementation()
            {
                await this.Store.DeleteAsync(entity);
                await this.Repository.SaveChangesAsync();
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously gets the successful action result for the Delete action.
        /// </summary>
        /// <param name="entity">The deleted entity.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method redirects to Index action.</remarks>
        protected virtual Task<ActionResult> GetDeleteSuccessResultAsync(TEntity entity, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.GetDeleteSuccessResult != null)
            {
                return this.Overrides.GetDeleteSuccessResult(entity, additionalData);
            }

            return Task.FromResult<ActionResult>(this.RedirectToAction("Index"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic Details action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDetailsModel">The type of the details model.</typeparam>
    public class BasicCrudDetailsActionHandler<TIdentifier, TEntity, TDetailsModel> : BaseCrudActionHandler<TIdentifier, TEntity, BasicCrudDetailsActionOverrides<TIdentifier, TEntity, TDetailsModel>>
        where TEntity : class
        where TDetailsModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCrudDetailsActionHandler{TIdentifier, TEntity, TDetailsModel}"/> class.
        /// </summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicCrudDetailsActionHandler(Controller controller, IEntityControllerServices controllerServices, IEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
        }

        /// <summary>
        /// Gets the action overrides.
        /// </summary>
        /// <value>
        /// The action overrides.
        /// </value>
        public override BasicCrudDetailsActionOverrides<TIdentifier, TEntity, TDetailsModel> Overrides { get; } = new BasicCrudDetailsActionOverrides<TIdentifier, TEntity, TDetailsModel>();

        /// <summary>
        /// Handles the GET request for the Details action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Details page.</returns>
        public async Task<IActionResult> Details(TIdentifier id)
        {
            var entity = await this.QuerySingleEntityAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.PermissionsValidator.DemandCanDetailsAsync(entity);

            var detailsModel = await this.ConvertToDetailsModelAsync(entity);
            return await this.GetDetailsViewResultAsync(id, entity, detailsModel);
        }

        /// <summary>
        /// Asynchronously converts the specified entity to the details model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation and contains converted details model as a result.</returns>
        protected virtual async Task<TDetailsModel> ConvertToDetailsModelAsync(TEntity entity)
        {
            if (this.Overrides.ConvertToDetailsModel != null)
            {
                return await this.Overrides.ConvertToDetailsModel(entity);
            }

            var allowedProperties = await this.GetAllowedEntityPropertiesAsync(EntityPermissions.EntityProperty.Read);
            return this.ControllerServices.MappingManager.GetModelMapper<TEntity, TDetailsModel>().ConvertEntityToViewModel(entity, allowedProperties);
        }

        /// <summary>
        /// Asynchronously gets the action result that is used to display the form for the Details action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="model">The details model.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method creates the ViewResult with the specified model.</remarks>
        protected virtual Task<IActionResult> GetDetailsViewResultAsync(TIdentifier id, TEntity entity, TDetailsModel model)
        {
            if (this.Overrides.GetDetailsViewResult != null)
            {
                return this.Overrides.GetDetailsViewResult(id, entity, model);
            }

            IActionResult result = this.View(model);
            return Task.FromResult(result);
        }
    }
}

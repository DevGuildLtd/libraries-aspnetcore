using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic custom operation action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TOperationModel">The type of the operation model.</typeparam>
    public class BasicCrudCustomOperationActionHandler<TIdentifier, TEntity, TOperationModel> : BaseCrudActionHandler<TIdentifier, TEntity, BasicCrudCustomOperationActionOverrides<TIdentifier, TEntity, TOperationModel>>
        where TEntity : class
        where TOperationModel : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier, TEntity, TOperationModel}"/> class.
        /// </summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicCrudCustomOperationActionHandler(Controller controller, IEntityControllerServices controllerServices, IEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
        }

        /// <summary>
        /// Gets the action handler overrides.
        /// </summary>
        /// <value>
        /// The action handler overrides.
        /// </value>
        public override BasicCrudCustomOperationActionOverrides<TIdentifier, TEntity, TOperationModel> Overrides { get; } = new BasicCrudCustomOperationActionOverrides<TIdentifier, TEntity, TOperationModel>();

        /// <summary>
        /// Handles the GET request for the custom operation action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="IActionResult"/> that renders custom operation action form.</returns>
        public async Task<IActionResult> Execute(TIdentifier id)
        {
            var entity = await this.QuerySingleEntityAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.PermissionsValidator.DemandCanDetailsAsync(entity);
            await this.DemandPermissionsAsync(id, entity);

            var model = new TOperationModel();
            await this.InitializeOperationModelAsync(id, entity, model, true);
            return await this.GetOperationViewResultAsync(id, entity, model);
        }

        /// <summary>
        /// Handles the POST request for the custom operation action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="model">The operation model.</param>
        /// <returns>An <see cref="IActionResult"/> that redirects to another page on success or renders custom operation action form again on failure.</returns>
        public async Task<IActionResult> Execute(TIdentifier id, TOperationModel model)
        {
            var entity = await this.QuerySingleEntityAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.PermissionsValidator.DemandCanDetailsAsync(entity);
            await this.DemandPermissionsAsync(id, entity);

            if (await this.ValidateOperationModelAsync(id, entity, model) && this.ModelState.IsValid)
            {
                await this.ExecuteOperationAsync(id, entity, model);
                return await this.GetOperationSuccessResultAsync(id, entity, model);
            }

            await this.InitializeOperationModelAsync(id, entity, model, false);
            return await this.GetOperationViewResultAsync(id, entity, model);
        }

        /// <summary>
        /// Asynchronously demands that the current user has required permissions to perform the operation against the specified entity.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task DemandPermissionsAsync(TIdentifier id, TEntity entity)
        {
            if (this.Overrides.DemandPermissions != null)
            {
                return this.Overrides.DemandPermissions(id, entity);
            }

            async Task DefaultImplementation()
            {
                await this.PermissionsValidator.DemandCanEditAsync(entity);
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously initializes the operation model.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="model">The operation model.</param>
        /// <param name="initial">if set to <c>true</c> the model is initialized first time; otherwise the model is reinitialized after form submit.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeOperationModelAsync(TIdentifier id, TEntity entity, TOperationModel model, Boolean initial)
        {
            if (this.Overrides.InitializeOperationModel != null)
            {
                return this.Overrides.InitializeOperationModel(id, entity, model, initial);
            }

            if (model is IEditModelOriginalEntity<TEntity> originalable)
            {
                originalable.Original = entity;
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously validates the operation model.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="model">The operation model.</param>
        /// <returns>A task that represents the operation and contains result of the validation as a result.</returns>
        protected virtual Task<Boolean> ValidateOperationModelAsync(TIdentifier id, TEntity entity, TOperationModel model)
        {
            if (this.Overrides.ValidateOperationModel != null)
            {
                return this.Overrides.ValidateOperationModel(id, entity, model);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously executes the operation.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="model">The operation model.</param>
        /// <returns>A task that represents the operation.</returns>
        /// <remarks>This method does nothing by default.</remarks>
        protected virtual Task ExecuteOperationAsync(TIdentifier id, TEntity entity, TOperationModel model)
        {
            if (this.Overrides.ExecuteOperation != null)
            {
                return this.Overrides.ExecuteOperation(id, entity, model);
            }

            // Do nothing by default
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets the action result that is used to display the form for the custom operation.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="model">The operation model.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method creates the ViewResult with the specified model.</remarks>
        protected virtual Task<IActionResult> GetOperationViewResultAsync(TIdentifier id, TEntity entity, TOperationModel model)
        {
            if (this.Overrides.GetOperationViewResult != null)
            {
                return this.Overrides.GetOperationViewResult(id, entity, model);
            }

            IActionResult result = this.View(model);
            return Task.FromResult(result);
        }

        /// <summary>
        /// Asynchronously gets the successful action result for the custom operation.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="model">The operation model.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method redirects to Details action.</remarks>
        protected virtual Task<IActionResult> GetOperationSuccessResultAsync(TIdentifier id, TEntity entity, TOperationModel model)
        {
            if (this.Overrides.GetOperationSuccessResult != null)
            {
                return this.Overrides.GetOperationSuccessResult(id, entity, model);
            }

            IActionResult result = this.RedirectToAction("Details", new { id = id });
            return Task.FromResult(result);
        }
    }
}

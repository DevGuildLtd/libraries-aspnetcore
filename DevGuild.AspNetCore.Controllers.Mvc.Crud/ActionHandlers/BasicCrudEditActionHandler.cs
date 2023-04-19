using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.ObjectModel;
using DevGuild.AspNetCore.Services.ModelMapping;
using DevGuild.AspNetCore.Services.Permissions.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic Edit action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDetailsModel">The type of the details model.</typeparam>
    /// <typeparam name="TEditModel">The type of the edit model.</typeparam>
    public class BasicCrudEditActionHandler<TIdentifier, TEntity, TDetailsModel, TEditModel> : BaseCrudActionHandler<TIdentifier, TEntity, BasicCrudEditActionOverrides<TIdentifier, TEntity, TDetailsModel, TEditModel>>
        where TEntity : class
        where TEditModel : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCrudEditActionHandler{TIdentifier, TEntity, TDetailsModel, TEditModel}"/> class.
        /// </summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicCrudEditActionHandler(Controller controller, IEntityControllerServices controllerServices, IEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
            this.ModelMapper = this.ControllerServices.MappingManager.GetModelMapper<TEntity, TEditModel>();
            this.DetailsModelMapper = this.ControllerServices.MappingManager.GetModelMapper<TEntity, TDetailsModel>();
        }

        /// <summary>
        /// Gets the action overrides.
        /// </summary>
        /// <value>
        /// The action overrides.
        /// </value>
        public override BasicCrudEditActionOverrides<TIdentifier, TEntity, TDetailsModel, TEditModel> Overrides { get; } = new BasicCrudEditActionOverrides<TIdentifier, TEntity, TDetailsModel, TEditModel>();

        /// <summary>
        /// Gets the model mapper.
        /// </summary>
        /// <value>
        /// The model mapper.
        /// </value>
        protected IViewModelMapper<TEntity, TEditModel> ModelMapper { get; }

        /// <summary>
        /// Gets the details model mapper.
        /// </summary>
        /// <value>
        /// The details model mapper.
        /// </value>
        protected IViewModelMapper<TEntity, TDetailsModel> DetailsModelMapper { get; }

        /// <summary>
        /// Handles the GET request for the Edit action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Edit action form.</returns>
        public async Task<IActionResult> Edit(TIdentifier id)
        {
            var entity = await this.QuerySingleEntityAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.PermissionsValidator.DemandCanEditAsync(entity);

            var model = new TEditModel();
            await this.InitializeEditModelWithEntityAsync(entity, model);
            await this.InitializeEditModelAsync(entity, model, true);

            return await this.GetEditViewResultAsync(entity, model);
        }

        /// <summary>
        /// Handles the POST request for the Edit action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="model">The edit model.</param>
        /// <returns>An <see cref="ActionResult"/> that redirects to another page on success or renders Edit action form again on failure.</returns>
        public async Task<IActionResult> Edit(TIdentifier id, TEditModel model)
        {
            var entity = await this.QuerySingleEntityAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.PermissionsValidator.DemandCanEditAsync(entity);

            if (await this.ValidateConcurrencyAsync(entity, model) && await this.ValidateEditModelAsync(entity, model) && this.ModelState.IsValid)
            {
                // Applying model changes to found entity
                await this.UpdateExistingEntityAsync(entity, model);
                await this.UpdateExistingEntityConcurrencyAsync(entity);

                // Performing pre-updating actions
                var additionalData = new Dictionary<String, Object>();
                await this.BeforeEntityUpdatedAsync(entity, model, additionalData);

                // Saving changes to the repository
                await this.UpdateEntityAsync(entity, model);

                // Performing post-updating actions
                await this.AfterEntityUpdatedAsync(entity, model, additionalData);

                return await this.GetEditSuccessResultAsync(entity, model, additionalData);
            }

            await this.InitializeEditModelAsync(entity, model, false);
            return await this.GetEditViewResultAsync(entity, model);
        }

        /// <summary>
        /// Asynchronously initializes the edit model with the edited entity.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="editModel">The edit model.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeEditModelWithEntityAsync(TEntity entity, TEditModel editModel)
        {
            if (this.Overrides.InitializeEditModelWithEntity != null)
            {
                return this.Overrides.InitializeEditModelWithEntity(entity, editModel);
            }

            async Task DefaultImplementation()
            {
                var propertiesAllowedToRead = await this.GetAllowedEntityPropertiesAsync(EntityPermissions.EntityProperty.Read);
                var propertiesAllowedToUpdate = await this.GetAllowedEntityPropertiesAsync(EntityPermissions.EntityProperty.Update);

                var propertiesAllowedToReadAndUpdate = propertiesAllowedToRead.Intersect(propertiesAllowedToUpdate).ToArray();

                await this.ModelMapper.InitializeEditModelAsync(entity, editModel, propertiesAllowedToReadAndUpdate);
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously initializes the edit model.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="editModel">The edit model.</param>
        /// <param name="initial">if set to <c>true</c> the model is initialized first time; otherwise the model is reinitialized after form submit.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeEditModelAsync(TEntity entity, TEditModel editModel, Boolean initial)
        {
            if (this.Overrides.InitializeEditModel != null)
            {
                return this.Overrides.InitializeEditModel(entity, editModel, initial);
            }

            async Task DefaultImplementation()
            {
                if (editModel is IEditModelOriginalEntity<TDetailsModel> editModelWithOriginal)
                {
                    editModelWithOriginal.Original = await this.ConvertToDetailsModelAsync(entity);
                }
            }

            return DefaultImplementation();
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
            return this.DetailsModelMapper.ConvertEntityToViewModel(entity, allowedProperties);
        }

        /// <summary>
        /// Asynchronously validates that the concurrency token in the edited entity and the concurrency token in the edit model are equal.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="editModel">The edit model.</param>
        /// <returns>A task that represents the operation and contains result of the validation as a result.</returns>
        /// <remarks>This method does nothing if concurrency is not configured for both entity and edit model.</remarks>
        protected virtual Task<Boolean> ValidateConcurrencyAsync(TEntity entity, TEditModel editModel)
        {
            if (this.Overrides.ValidateConcurrency != null)
            {
                return this.Overrides.ValidateConcurrency(entity, editModel);
            }

            if (entity is IConcurrencyCheck concurrencyCheckedItem && editModel is IEditModelConcurrencyCheck concurrencyCheckedEditModel)
            {
                if (concurrencyCheckedItem.ConcurrencyToken == concurrencyCheckedEditModel.ConcurrencyToken)
                {
                    return Task.FromResult(true);
                }

                return this.FailConcurrencyCheckAsync(entity, editModel).Then(() => false);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously fails the concurrency check.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="editModel">The edit model.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task FailConcurrencyCheckAsync(TEntity entity, TEditModel editModel)
        {
            if (this.Overrides.FailConcurrencyCheck != null)
            {
                return this.Overrides.FailConcurrencyCheck(entity, editModel);
            }

            this.ModelState.AddModelError(String.Empty, "This entity was changed elsewhere");
            this.ModelState.Remove(nameof(IEditModelConcurrencyCheck.ConcurrencyToken));
            ((IEditModelConcurrencyCheck)editModel).ConcurrencyToken = ((IConcurrencyCheck)entity).ConcurrencyToken;

            // TODO: Compare properties include new current values as model state errors
            return Task.FromResult(false);
        }

        /// <summary>
        /// Asynchronously validates the specified edit model.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="editModel">The edit model.</param>
        /// <returns>A task that represents the operation and contains result of the validation as a result.</returns>
        protected virtual Task<Boolean> ValidateEditModelAsync(TEntity entity, TEditModel editModel)
        {
            if (this.Overrides.ValidateEditModel != null)
            {
                return this.Overrides.ValidateEditModel(entity, editModel);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously updates the edited entity from the specified edit model.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="model">The edit model.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task UpdateExistingEntityAsync(TEntity entity, TEditModel model)
        {
            if (this.Overrides.UpdateExistingEntity != null)
            {
                return this.Overrides.UpdateExistingEntity(entity, model);
            }

            async Task DefaultImplementation()
            {
                var allowedProperties = await this.GetAllowedEntityPropertiesAsync(EntityPermissions.EntityProperty.Update);
                await this.ModelMapper.UpdateExistingEntityAsync(entity, model, allowedProperties);
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously updates the edited entity concurrency token.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task UpdateExistingEntityConcurrencyAsync(TEntity entity)
        {
            if (this.Overrides.UpdateExistingEntityConcurrency != null)
            {
                return this.Overrides.UpdateExistingEntityConcurrency(entity);
            }

            if (entity is IConcurrencyCheck concurrencyChecked)
            {
                concurrencyChecked.ConcurrencyToken = Guid.NewGuid();
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously performs entity update in the entity store.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="model">The edit model.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task UpdateEntityAsync(TEntity entity, TEditModel model)
        {
            if (this.Overrides.UpdateEntity != null)
            {
                return this.Overrides.UpdateEntity(entity, model);
            }

            async Task DefaultImplementation()
            {
                await this.Store.UpdateAsync(entity);
                await this.Repository.SaveChangesAsync();
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously executes additional actions just before the entity is updated in the entity store.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="model">The edit model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task BeforeEntityUpdatedAsync(TEntity entity, TEditModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.BeforeEntityUpdated != null)
            {
                return this.Overrides.BeforeEntityUpdated(entity, model, additionalData);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously executes additional actions after the entity is updated in the entity store.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="model">The edit model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task AfterEntityUpdatedAsync(TEntity entity, TEditModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.AfterEntityUpdated != null)
            {
                return this.Overrides.AfterEntityUpdated(entity, model, additionalData);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets the successful action result for the Edit action.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="model">The edit model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method redirects to Index action.</remarks>
        protected virtual Task<IActionResult> GetEditSuccessResultAsync(TEntity entity, TEditModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.GetEditSuccessResult != null)
            {
                return this.Overrides.GetEditSuccessResult(entity, model, additionalData);
            }

            return Task.FromResult<IActionResult>(this.RedirectToAction("Index"));
        }

        /// <summary>
        /// Asynchronously gets the action result that is used to display the form for the Edit action.
        /// </summary>
        /// <param name="entity">The edited entity.</param>
        /// <param name="model">The edit model.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method creates the ViewResult with the specified model.</remarks>
        protected virtual Task<IActionResult> GetEditViewResultAsync(TEntity entity, TEditModel model)
        {
            if (this.Overrides.GetEditViewResult != null)
            {
                return this.Overrides.GetEditViewResult(entity, model);
            }

            return Task.FromResult<IActionResult>(this.View(model));
        }
    }
}

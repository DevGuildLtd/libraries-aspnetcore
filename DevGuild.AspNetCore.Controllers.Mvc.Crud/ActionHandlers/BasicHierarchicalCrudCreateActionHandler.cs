using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.ObjectModel;
using DevGuild.AspNetCore.Services.Permissions.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents hierarchical Create action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TCreateModel">The type of the create model.</typeparam>
    public class BasicHierarchicalCrudCreateActionHandler<TIdentifier, TEntity, TCreateModel> : BaseCrudActionHandler<TIdentifier, TEntity, BasicHierarchicalCrudCreateActionOverrides<TIdentifier, TEntity, TCreateModel>>
        where TIdentifier : struct
        where TEntity : class, IHierarchicalEntity<TIdentifier, TEntity>, new()
        where TCreateModel : class, IHierarchicalCreateModel<TEntity>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier, TEntity, TCreateModel}"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicHierarchicalCrudCreateActionHandler(Controller controller, IEntityControllerServices controllerServices, IHierarchicalEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
            this.PermissionsValidator = permissionsValidator;
        }

        /// <summary>
        /// Gets the action overrides.
        /// </summary>
        /// <value>
        /// The action overrides.
        /// </value>
        public override BasicHierarchicalCrudCreateActionOverrides<TIdentifier, TEntity, TCreateModel> Overrides { get; } = new BasicHierarchicalCrudCreateActionOverrides<TIdentifier, TEntity, TCreateModel>();

        /// <summary>
        /// Gets the permissions validator.
        /// </summary>
        /// <value>
        /// The permissions validator.
        /// </value>
        protected new IHierarchicalEntityPermissionsValidator<TEntity> PermissionsValidator { get; }

        /// <summary>
        /// Handles the GET request for the Create action.
        /// </summary>
        /// <param name="parentId">The identifier of the parent entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Create action form.</returns>
        public async Task<IActionResult> Create(TIdentifier? parentId)
        {
            await this.PermissionsValidator.DemandCanCreateAsync();

            TEntity parent = null;
            if (parentId.HasValue)
            {
                parent = await this.QuerySingleEntityAsync(parentId.Value);
                if (parent == null)
                {
                    return this.NotFound();
                }

                await this.PermissionsValidator.DemandCanCreateChildAsync(parent);
            }

            var model = new TCreateModel();
            model.Parent = parent;
            await this.InitializeCreateModelAsync(model, true);

            return this.View(model);
        }

        /// <summary>
        /// Handles the POST request for the Create action
        /// </summary>
        /// <param name="parentId">The identifier of the parent enti.</param>
        /// <param name="model">The create model.</param>
        /// <returns>An <see cref="ActionResult"/> that redirects to another page on success or renders Create action form again on failure.</returns>
        public async Task<IActionResult> Create(TIdentifier? parentId, TCreateModel model)
        {
            await this.PermissionsValidator.DemandCanCreateAsync();

            TEntity parent = null;
            if (parentId.HasValue)
            {
                parent = await this.QuerySingleEntityAsync(parentId.Value);
                if (parent == null)
                {
                    return this.NotFound();
                }

                await this.PermissionsValidator.DemandCanCreateChildAsync(parent);
            }

            if (await this.ValidateCreateModelAsync(model) && this.ModelState.IsValid)
            {
                var entity = new TEntity();
                entity.ParentId = parentId;
                await this.InitializeNewEntityAsync(entity, model);
                await this.InitializeNewEntityConcurrencyAsync(entity);

                // Performing pre-creation actions
                var additionalData = new Dictionary<String, Object>();
                await this.BeforeEntityCreatedAsync(entity, model, additionalData);

                // Saving entity to the repository
                await this.Store.InsertAsync(entity);
                await this.Repository.SaveChangesAsync();

                // Performing post-creation actions
                await this.AfterEntityCreatedAsync(entity, model, additionalData);

                return await this.GetCreateSuccessResultAsync(entity, model, additionalData);
            }

            model.Parent = parent;
            await this.InitializeCreateModelAsync(model, false);
            return this.View(model);
        }

        /// <summary>
        /// Asynchronously initializes the create model.
        /// </summary>
        /// <param name="model">The model that need to be initialized.</param>
        /// <param name="initial">if set to <c>true</c> the model is initialized first time; otherwise the model is reinitialized after form submit.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeCreateModelAsync(TCreateModel model, Boolean initial)
        {
            if (this.Overrides.InitializeCreateModel != null)
            {
                return this.Overrides.InitializeCreateModel(model, initial);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously initializes the new entity from the specified create model.
        /// </summary>
        /// <param name="entity">The newly created entity.</param>
        /// <param name="model">The create model that contains information required for entity initialization.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeNewEntityAsync(TEntity entity, TCreateModel model)
        {
            if (this.Overrides.InitializeNewEntity != null)
            {
                return this.Overrides.InitializeNewEntity(entity, model);
            }

            async Task DefaultImplementation()
            {
                if (entity is IConcurrencyCheck concurrencyCheckedEntity)
                {
                    concurrencyCheckedEntity.ConcurrencyToken = Guid.NewGuid();
                }

                var allowedProperties = await this.GetAllowedEntityPropertiesAsync(EntityPermissions.EntityProperty.Initialize);
                await this.ControllerServices.MappingManager.GetModelMapper<TEntity, TCreateModel>().InitializeNewEntityAsync(entity, model, allowedProperties);
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously initializes the new entity concurrency token.
        /// </summary>
        /// <param name="entity">The newly created entity.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeNewEntityConcurrencyAsync(TEntity entity)
        {
            if (this.Overrides.InitializeNewEntityConcurrency != null)
            {
                return this.Overrides.InitializeNewEntityConcurrency(entity);
            }

            if (entity is IConcurrencyCheck concurrencyCheckedEntity)
            {
                concurrencyCheckedEntity.ConcurrencyToken = Guid.NewGuid();
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously validates the specified create model.
        /// </summary>
        /// <param name="model">The create model.</param>
        /// <returns>A task that represents the operation and contains result of the validation as a result.</returns>
        protected virtual Task<Boolean> ValidateCreateModelAsync(TCreateModel model)
        {
            if (this.Overrides.ValidateCreateModel != null)
            {
                return this.Overrides.ValidateCreateModel(model);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously executes additional actions just before the entity is inserted into the entity store.
        /// </summary>
        /// <param name="entity">The created entity.</param>
        /// <param name="model">The create model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task BeforeEntityCreatedAsync(TEntity entity, TCreateModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.BeforeEntityCreated != null)
            {
                return this.Overrides.BeforeEntityCreated(entity, model, additionalData);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously executes additional actions after the entity is inserted into the entity store.
        /// </summary>
        /// <param name="entity">The created entity.</param>
        /// <param name="model">The create model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task AfterEntityCreatedAsync(TEntity entity, TCreateModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.AfterEntityCreated != null)
            {
                return this.Overrides.AfterEntityCreated(entity, model, additionalData);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets the successful action result for the Create action.
        /// </summary>
        /// <param name="entity">The created entity.</param>
        /// <param name="model">The create model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method redirects to Index action.</remarks>
        protected virtual Task<ActionResult> GetCreateSuccessResultAsync(TEntity entity, TCreateModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.GetCreateSuccessResult != null)
            {
                return this.Overrides.GetCreateSuccessResult(entity, model, additionalData);
            }

            return Task.FromResult<ActionResult>(this.RedirectToAction("Index"));
        }
    }
}

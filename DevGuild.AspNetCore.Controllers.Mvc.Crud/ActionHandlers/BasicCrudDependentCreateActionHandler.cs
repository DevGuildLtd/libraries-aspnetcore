﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Contracts;
using DevGuild.AspNetCore.ObjectModel;
using DevGuild.AspNetCore.Services.Permissions.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents dependent Create action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TParentIdentifier">The type of the parent identifier.</typeparam>
    /// <typeparam name="TParentEntity">The type of the parent entity.</typeparam>
    /// <typeparam name="TCreateModel">The type of the create model.</typeparam>
    public class BasicCrudDependentCreateActionHandler<TIdentifier, TEntity, TParentIdentifier, TParentEntity, TCreateModel>
        : BaseCrudDependentActionHandler<TIdentifier, TEntity, TParentIdentifier, TParentEntity, BasicCrudDependentCreateActionOverrides<TIdentifier, TEntity, TParentIdentifier, TParentEntity, TCreateModel>>
        where TEntity : class, new()
        where TParentEntity : class
        where TCreateModel : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCrudDependentCreateActionHandler{TIdentifier, TEntity, TParentIdentifier, TParentEntity, TCreateModel}"/> class.
        /// </summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicCrudDependentCreateActionHandler(Controller controller, IEntityControllerServices controllerServices, IDependentEntityPermissionsValidator<TEntity, TParentEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
        }

        /// <summary>
        /// Gets the action overrides.
        /// </summary>
        /// <value>
        /// The action overrides.
        /// </value>
        public override BasicCrudDependentCreateActionOverrides<TIdentifier, TEntity, TParentIdentifier, TParentEntity, TCreateModel> Overrides { get; } =
            new BasicCrudDependentCreateActionOverrides<TIdentifier, TEntity, TParentIdentifier, TParentEntity, TCreateModel>();

        /// <summary>
        /// Handles the GET request for the Create action.
        /// </summary>
        /// <param name="parentId">The identifier of the parent entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Create action form.</returns>
        public async Task<IActionResult> Create(TParentIdentifier parentId)
        {
            var parent = await this.QuerySingleParentEntityAsync(parentId);
            if (parent == null)
            {
                return this.NotFound();
            }

            await this.DemandCanCreateAsync(parent);

            var model = new TCreateModel();
            await this.InitializeCreateModelAsync(parent, model, true);

            return await this.GetCreateViewResultAsync(parent, model);
        }

        /// <summary>
        /// Handles the POST request for the Create action.
        /// </summary>
        /// <param name="parentId">The identifier of the parent entity.</param>
        /// <param name="model">The create model.</param>
        /// <returns>An <see cref="ActionResult"/> that redirects to another page on success or renders Create action form again on failure.</returns>
        public async Task<IActionResult> Create(TParentIdentifier parentId, TCreateModel model)
        {
            var parent = await this.QuerySingleParentEntityAsync(parentId);
            if (parent == null)
            {
                return this.NotFound();
            }

            await this.DemandCanCreateAsync(parent);

            if (await this.ValidateCreateModelAsync(parent, model) && this.ModelState.IsValid)
            {
                var entity = new TEntity();
                await this.InitializeNewEntityAsync(parent, entity, model);
                await this.InitializeNewEntityParentAsync(parent, entity, model);
                await this.InitializeNewEntityConcurrencyAsync(parent, entity);

                // Performing pre-creation actions
                var additionalData = new Dictionary<String, Object>();
                await this.BeforeEntityCreatedAsync(parent, entity, model, additionalData);

                // Saving entity to the repository
                await this.Store.InsertAsync(entity);
                await this.Repository.SaveChangesAsync();

                // Performing post-creation actions
                await this.AfterEntityCreatedAsync(parent, entity, model, additionalData);

                return await this.GetCreateSuccessResultAsync(parent, entity, model, additionalData);
            }

            await this.InitializeCreateModelAsync(parent, model, false);
            return await this.GetCreateViewResultAsync(parent, model);
        }

        /// <summary>
        /// Asynchronously demands that the current user has required permissions to perform the create operation against the specified parent entity.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task DemandCanCreateAsync(TParentEntity parent)
        {
            if (this.Overrides.DemandCanCreate != null)
            {
                return this.Overrides.DemandCanCreate(parent);
            }

            return this.PermissionsValidator.DemandCanCreateAsync(parent);
        }

        /// <summary>
        /// Asynchronously initializes the create model.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="model">The model that need to be initialized.</param>
        /// <param name="initial">if set to <c>true</c> the model is initialized first time; otherwise the model is reinitialized after form submit.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeCreateModelAsync(TParentEntity parent, TCreateModel model, Boolean initial)
        {
            if (this.Overrides.InitializeCreateModel != null)
            {
                return this.Overrides.InitializeCreateModel(parent, model, initial);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously initializes the new entity from the specified create model.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="entity">The newly created entity.</param>
        /// <param name="model">The create model that contains information required for entity initialization.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeNewEntityAsync(TParentEntity parent, TEntity entity, TCreateModel model)
        {
            if (this.Overrides.InitializeNewEntity != null)
            {
                return this.Overrides.InitializeNewEntity(parent, entity, model);
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
        /// Asynchronously initializes the new entity's parent.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="entity">The newly created entity.</param>
        /// <param name="model">The create model.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeNewEntityParentAsync(TParentEntity parent, TEntity entity, TCreateModel model)
        {
            if (this.Overrides.InitializeNewEntityParent != null)
            {
                return this.Overrides.InitializeNewEntityParent(parent, entity, model);
            }

            var parentType = typeof(TParentEntity);
            var parentIdProperty = parentType.GetProperty("Id");
            Ensure.State.NotNull(parentIdProperty);
            var parentId = (TParentIdentifier)parentIdProperty.GetValue(parent);

            var entityType = typeof(TEntity);
            var entityParentProperty = entityType.GetProperties().Single(x => x.PropertyType == typeof(TParentEntity));
            var entityParentIdProperty = entityType.GetProperty($"{entityParentProperty.Name}Id");
            Ensure.State.NotNull(entityParentIdProperty);

            entityParentIdProperty.SetValue(entity, parentId);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously initializes the new entity concurrency token.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="entity">The newly created entity.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task InitializeNewEntityConcurrencyAsync(TParentEntity parent, TEntity entity)
        {
            if (this.Overrides.InitializeNewEntityConcurrency != null)
            {
                return this.Overrides.InitializeNewEntityConcurrency(parent, entity);
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
        /// <param name="parent">The parent entity.</param>
        /// <param name="model">The create model.</param>
        /// <returns>A task that represents the operation and contains result of the validation as a result.</returns>
        protected virtual Task<Boolean> ValidateCreateModelAsync(TParentEntity parent, TCreateModel model)
        {
            if (this.Overrides.ValidateCreateModel != null)
            {
                return this.Overrides.ValidateCreateModel(parent, model);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Asynchronously executes additional actions just before the entity is inserted into the entity store.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="entity">The created entity.</param>
        /// <param name="model">The create model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task BeforeEntityCreatedAsync(TParentEntity parent, TEntity entity, TCreateModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.BeforeEntityCreated != null)
            {
                return this.Overrides.BeforeEntityCreated(parent, entity, model, additionalData);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously executes additional actions after the entity is inserted into the entity store.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="entity">The created entity.</param>
        /// <param name="model">The create model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation.</returns>
        protected virtual Task AfterEntityCreatedAsync(TParentEntity parent, TEntity entity, TCreateModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.AfterEntityCreated != null)
            {
                return this.Overrides.AfterEntityCreated(parent, entity, model, additionalData);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets the action result that is used to display the form for the Create action.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="model">The create model.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method creates the ViewResult with the specified model.</remarks>
        protected virtual Task<IActionResult> GetCreateViewResultAsync(TParentEntity parent, TCreateModel model)
        {
            if (this.Overrides.GetCreateViewResult != null)
            {
                return this.Overrides.GetCreateViewResult(parent, model);
            }

            return Task.FromResult<IActionResult>(this.View(model));
        }

        /// <summary>
        /// Asynchronously gets the successful action result for the Create action.
        /// </summary>
        /// <param name="parent">The parent entity.</param>
        /// <param name="entity">The created entity.</param>
        /// <param name="model">The create model.</param>
        /// <param name="additionalData">The additional data dictionary that could be used to pass additional data.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method redirects to Index action.</remarks>
        protected virtual Task<IActionResult> GetCreateSuccessResultAsync(TParentEntity parent, TEntity entity, TCreateModel model, Dictionary<String, Object> additionalData)
        {
            if (this.Overrides.GetCreateSuccessResult != null)
            {
                return this.Overrides.GetCreateSuccessResult(parent, entity, model, additionalData);
            }

            var parentType = typeof(TParentEntity);
            var parentIdProperty = parentType.GetProperty("Id");
            Ensure.State.NotNull(parentIdProperty);
            var parentId = (TParentIdentifier)parentIdProperty.GetValue(parent);

            return Task.FromResult<IActionResult>(this.RedirectToAction("Index", new { parentId = parentId }));
        }
    }
}

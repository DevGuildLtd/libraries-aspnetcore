using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.ObjectModel;
using DevGuild.AspNetCore.Services.Permissions.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic Index action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TIndexViewModel">The type of the index view model.</typeparam>
    /// <typeparam name="TIndexItemModel">The type of the index item model.</typeparam>
    public class BasicCrudIndexActionHandler<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel> : BaseCrudActionHandler<TIdentifier, TEntity, BasicCrudIndexActionOverrides<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel>>
        where TEntity : class
        where TIndexViewModel : class, IEntityIndexModel<TIndexItemModel>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCrudIndexActionHandler{TIdentifier, TEntity, TIndexViewModel, TIndexItemModel}"/> class.
        /// </summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicCrudIndexActionHandler(Controller controller, IEntityControllerServices controllerServices, IEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
        }

        /// <summary>
        /// Gets the action overrides.
        /// </summary>
        /// <value>
        /// The action overrides.
        /// </value>
        public override BasicCrudIndexActionOverrides<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel> Overrides { get; } = new BasicCrudIndexActionOverrides<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel>();

        /// <summary>
        /// Handles the GET request for the Index action.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> that renders Index action page.</returns>
        public async Task<IActionResult> Index()
        {
            await this.PermissionsValidator.DemandCanIndexAsync();

            var query = await this.PrepareItemsQueryAsync();
            query = await this.PermissionsValidator.RequireReadAccessAsync(query);

            var entities = await query.ToListAsync();
            var allowedProperties = await this.GetAllowedEntityPropertiesAsync(EntityPermissions.EntityProperty.Read);
            var items = await this.ConvertEntitiesToIndexItemModelAsync(entities, allowedProperties);

            var model = new TIndexViewModel
            {
                Items = items
            };

            return this.View(model);
        }

        /// <summary>
        /// Asynchronously prepares the entities retrieval query.
        /// </summary>
        /// <returns>A task that represents the operation and contains entities retrieval query as a result.</returns>
        protected virtual Task<IQueryable<TEntity>> PrepareItemsQueryAsync()
        {
            if (this.Overrides.PrepareItemsQuery != null)
            {
                return this.Overrides.PrepareItemsQuery();
            }

            return Task.FromResult(this.Store.Query());
        }

        /// <summary>
        /// Asynchronously converts the entities to the index item model.
        /// </summary>
        /// <param name="entities">The collection of entities.</param>
        /// <param name="allowedProperties">The allowed properties.</param>
        /// <returns>A task tha represents the operation and contains a collection of converted entities as a result.</returns>
        protected virtual Task<ICollection<TIndexItemModel>> ConvertEntitiesToIndexItemModelAsync(ICollection<TEntity> entities, String[] allowedProperties)
        {
            if (this.Overrides.ConvertEntitiesToIndexItemModel != null)
            {
                return this.Overrides.ConvertEntitiesToIndexItemModel(entities, allowedProperties);
            }

            var mappingManager = this.ControllerServices.MappingManager.GetModelMapper<TEntity, TIndexItemModel>();
            return Task.FromResult<ICollection<TIndexItemModel>>(entities.Select(x => mappingManager.ConvertEntityToViewModel(x, allowedProperties)).ToList());
        }

        /// <summary>
        /// Asynchronously gets the action result that is used to display the Index action page.
        /// </summary>
        /// <param name="model">The index view model.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method creates the ViewResult with the specified model.</remarks>
        protected virtual Task<IActionResult> GetIndexViewResultAsync(TIndexViewModel model)
        {
            if (this.Overrides.GetIndexViewResult != null)
            {
                return this.Overrides.GetIndexViewResult(model);
            }

            return Task.FromResult<IActionResult>(this.View(model));
        }
    }
}

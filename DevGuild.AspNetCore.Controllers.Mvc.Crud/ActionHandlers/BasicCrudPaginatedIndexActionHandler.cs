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
    public class BasicCrudPaginatedIndexActionHandler<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel> : BaseCrudActionHandler<TIdentifier, TEntity, BasicCrudPaginatedIndexActionOverrides<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel>>
        where TEntity : class
        where TIndexViewModel : class, IEntityPaginatedIndexModel<TIndexItemModel>, new()
    {
        public BasicCrudPaginatedIndexActionHandler(Controller controller, IEntityControllerServices controllerServices, IEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
        }

        public override BasicCrudPaginatedIndexActionOverrides<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel> Overrides { get; } = new BasicCrudPaginatedIndexActionOverrides<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel>();

        /// <summary>
        /// Handles the GET request for the Index action.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> that renders Index action page.</returns>
        public async Task<IActionResult> Index(Int32? page)
        {
            await this.PermissionsValidator.DemandCanIndexAsync();

            var query = await this.PrepareItemsQueryAsync();
            query = await this.PermissionsValidator.RequireReadAccessAsync(query);

            var itemsPerPage = await this.GetItemsPerPageAsync();
            var paginationResult = await this.CalculatePaginationAsync(query, page ?? 1, itemsPerPage);

            if (paginationResult.ActionResult != null)
            {
                return paginationResult.ActionResult;
            }

            var allowedProperties = await this.GetAllowedEntityPropertiesAsync(EntityPermissions.EntityProperty.Read);
            var items = await this.ConvertEntitiesToIndexItemModelAsync(paginationResult.Entites, allowedProperties);

            var model = new TIndexViewModel
            {
                Items = new PaginationResult<TIndexItemModel>(items, paginationResult.Info),
            };

            return await this.GetIndexViewResultAsync(model);
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

        protected virtual Task<PaginationCalculationResult<TEntity>> CalculatePaginationAsync(IQueryable<TEntity> query, Int32 currentPage, Int32 itemsPerPage)
        {
            async Task<PaginationCalculationResult<TEntity>> DefaultImplementation()
            {
                var totalItems = await query.CountAsync();
                if (totalItems == 0)
                {
                    if (currentPage != 1)
                    {
                        return PaginationCalculationResult<TEntity>.Fail(await this.RedirectToPageAsync(1));
                    }

                    return PaginationCalculationResult<TEntity>.Succeed(new PaginationInfo(0, 0, itemsPerPage, currentPage), new List<TEntity>());
                }

                var totalPages = totalItems / itemsPerPage + (totalItems % itemsPerPage > 0 ? 1 : 0);
                if (currentPage < 1)
                {
                    return PaginationCalculationResult<TEntity>.Fail(await this.RedirectToPageAsync(1));
                }

                if (currentPage > totalPages)
                {
                    return PaginationCalculationResult<TEntity>.Fail(await this.RedirectToPageAsync(totalPages));
                }

                var entities = await query.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();

                return PaginationCalculationResult<TEntity>.Succeed(new PaginationInfo(totalItems, totalPages, itemsPerPage, currentPage), entities);
            }

            return DefaultImplementation();
        }

        protected virtual Task<Int32> GetItemsPerPageAsync()
        {
            return Task.FromResult(10);
        }

        protected virtual Task<IActionResult> RedirectToPageAsync(Int32 page)
        {
            if (page == 1)
            {
                return Task.FromResult<IActionResult>(this.RedirectToAction("Index"));
            }

            return Task.FromResult<IActionResult>(this.RedirectToAction("Index", new { page = page }));
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

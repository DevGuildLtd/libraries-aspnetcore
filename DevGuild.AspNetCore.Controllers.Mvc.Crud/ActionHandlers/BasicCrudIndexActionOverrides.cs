using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic Index action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TIndexViewModel">The type of the index view model.</typeparam>
    /// <typeparam name="TIndexItemModel">The type of the index item model.</typeparam>
    public class BasicCrudIndexActionOverrides<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudIndexActionHandler{TIdentifier,TEntity,TIndexViewModel,TIndexItemModel}.PrepareItemsQueryAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudIndexActionHandler{TIdentifier,TEntity,TIndexViewModel,TIndexItemModel}.PrepareItemsQueryAsync"/> method of the related action handler.
        /// </value>
        public Func<Task<IQueryable<TEntity>>> PrepareItemsQuery { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudIndexActionHandler{TIdentifier,TEntity,TIndexViewModel,TIndexItemModel}.ConvertEntitiesToIndexItemModelAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudIndexActionHandler{TIdentifier,TEntity,TIndexViewModel,TIndexItemModel}.ConvertEntitiesToIndexItemModelAsync"/> method of the related action handler.
        /// </value>
        public Func<ICollection<TEntity>, String[], Task<ICollection<TIndexItemModel>>> ConvertEntitiesToIndexItemModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudIndexActionHandler{TIdentifier,TEntity,TIndexViewModel,TIndexItemModel}.GetIndexViewResultAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudIndexActionHandler{TIdentifier,TEntity,TIndexViewModel,TIndexItemModel}.GetIndexViewResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TIndexViewModel, Task<ActionResult>> GetIndexViewResult { get; set; }
    }
}

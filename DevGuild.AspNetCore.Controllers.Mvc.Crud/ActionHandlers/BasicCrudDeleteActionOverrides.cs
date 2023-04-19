using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic Delete action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDeleteModel">The type of the delete model.</typeparam>
    /// <seealso cref="BaseCrudActionOverrides{TIdentifier,TEntity}" />
    public class BasicCrudDeleteActionOverrides<TIdentifier, TEntity, TDeleteModel> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.ConvertToDeleteModelAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.ConvertToDeleteModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Task<TDeleteModel>> ConvertToDeleteModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.BeforeEntityDeletedAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.BeforeEntityDeletedAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Dictionary<String, Object>, Task> BeforeEntityDeleted { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.AfterEntityDeletedAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.AfterEntityDeletedAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Dictionary<String, Object>, Task> AfterEntityDeleted { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.DeleteEntityAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.DeleteEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Dictionary<String, Object>, Task> DeleteEntity { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.GetDeleteViewResultAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.GetDeleteViewResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, TEntity, TDeleteModel, Task<IActionResult>> GetDeleteViewResult { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.GetDeleteSuccessResultAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudDeleteActionHandler{TIdentifier,TEntity,TDeleteModel}.GetDeleteSuccessResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Dictionary<String, Object>, Task<IActionResult>> GetDeleteSuccessResult { get; set; }
    }
}

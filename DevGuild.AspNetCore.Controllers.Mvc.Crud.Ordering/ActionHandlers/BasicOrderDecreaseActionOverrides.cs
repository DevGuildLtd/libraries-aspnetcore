using System;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers;
using DevGuild.AspNetCore.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.Ordering.ActionHandlers
{
    /// <summary>
    /// Represents basic order-decreasing operation action overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="BasicOrderDecreaseActionHandlerBase{TIdentifier, TEntity}" />
    public class BasicOrderDecreaseActionOverrides<TIdentifier, TEntity> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class, IOrderableEntity
    {
        /// <summary>Gets or sets the override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.DemandPermissionsAsync"/> method of the related action handler.</summary>
        /// <value>The override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.DemandPermissionsAsync"/> method of the related action handler.</value>
        public Func<TIdentifier, TEntity, Task> DemandPermissions { get; set; }

        /// <summary>Gets or sets the override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.FindPreviousAsync"/> method of the related action handler.</summary>
        /// <value>The override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.FindPreviousAsync"/> method of the related action handler.</value>
        public Func<TIdentifier, TEntity, Task<TEntity>> FindPrevious { get; set; }

        /// <summary>Gets or sets the override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.ExecuteOrderSwapAsync"/> method of the related action handler.</summary>
        /// <value>The override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.ExecuteOrderSwapAsync"/> method of the related action handler.</value>
        public Func<TEntity, TEntity, Task> ExecuteOrderSwap { get; set; }

        /// <summary>Gets or sets the override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetTableNameAsync"/> method of the related action handler.</summary>
        /// <value>The override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetTableNameAsync"/> method of the related action handler.</value>
        public Func<Task<String>> GetTableName { get; set; }

        /// <summary>Gets or sets the override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetOrderNoColumnNameAsync"/> method of the related action handler.</summary>
        /// <value>The override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetOrderNoColumnNameAsync"/> method of the related action handler.</value>
        public Func<Task<String>> GetOrderNoColumnName { get; set; }

        /// <summary>Gets or sets the override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetIdColumnNameAsync"/> method of the related action handler.</summary>
        /// <value>The override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetIdColumnNameAsync"/> method of the related action handler.</value>
        public Func<Task<String>> GetIdColumnName { get; set; }

        /// <summary>Gets or sets the override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetEntityIdentifierAsync"/> method of the related action handler.</summary>
        /// <value>The override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetEntityIdentifierAsync"/> method of the related action handler.</value>
        public Func<TEntity, Task<TIdentifier>> GetEntityIdentifier { get; set; }

        /// <summary>Gets or sets the override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetOrderChangeSuccessResultAsync"/> method of the related action handler.</summary>
        /// <value>The override implementation of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier,TEntity}.GetOrderChangeSuccessResultAsync"/> method of the related action handler.</value>
        public Func<TEntity, TEntity, Task<IActionResult>> GetOrderChangeSuccessResult { get; set; }
    }
}

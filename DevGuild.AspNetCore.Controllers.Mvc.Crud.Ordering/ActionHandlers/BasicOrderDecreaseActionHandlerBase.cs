using System;
using System.Linq;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers;
using DevGuild.AspNetCore.ObjectModel;
using DevGuild.AspNetCore.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.Ordering.ActionHandlers
{
    /// <summary>
    /// Base implementation of basic order-decreasing operation action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class BasicOrderDecreaseActionHandlerBase<TIdentifier, TEntity> : BaseCrudActionHandler<TIdentifier, TEntity, BasicOrderDecreaseActionOverrides<TIdentifier, TEntity>>
        where TEntity : class, IOrderableEntity
    {
        /// <summary>Initializes a new instance of the <see cref="BasicOrderDecreaseActionHandlerBase{TIdentifier, TEntity}" /> class.</summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicOrderDecreaseActionHandlerBase(
            Controller controller,
            IEntityControllerServices controllerServices,
            IEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
        }

        /// <summary>
        /// Gets the action handler overrides.
        /// </summary>
        /// <value>
        /// The action handler overrides.
        /// </value>
        public override BasicOrderDecreaseActionOverrides<TIdentifier, TEntity> Overrides { get; } = new ();

        /// <summary>
        /// Decreases the manual order number of an entity.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="IActionResult"/> with the result of the operation.</returns>
        public async Task<IActionResult> DecreaseOrder(TIdentifier id)
        {
            var entity = await this.QuerySingleEntityAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.DemandPermissionsAsync(id, entity);

            var previous = await this.FindPreviousAsync(id, entity);
            if (previous != null)
            {
                await this.ExecuteOrderSwapAsync(entity, previous);
            }

            return await this.GetOrderChangeSuccessResultAsync(entity, previous);
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
                return this.Overrides.DemandPermissions.Invoke(id, entity);
            }

            async Task DefaultImplementation()
            {
                await this.PermissionsValidator.DemandCanDetailsAsync(entity);
                await this.PermissionsValidator.DemandCanEditAsync(entity);
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously finds the previous entity based on their manual order.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation and contains its result.</returns>
        protected virtual Task<TEntity> FindPreviousAsync(TIdentifier id, TEntity entity)
        {
            if (this.Overrides.FindPrevious != null)
            {
                return this.Overrides.FindPrevious.Invoke(id, entity);
            }

            async Task<TEntity> DefaultImplementation()
            {
                var previousList = await this.Repository.Query<TEntity>()
                    .Where(x => x.OrderNo < entity.OrderNo)
                    .OrderByDescending(x => x.OrderNo)
                    .Take(1)
                    .ToListAsync();

                return previousList.Count > 0 ? previousList[0] : null;
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously executes the order number swap.
        /// </summary>
        /// <param name="entity">Current entity.</param>
        /// <param name="previous">Previous entity.</param>
        /// <returns>A task that represents the operation.</returns>
        /// <exception cref="NotSupportedException">This class does not provide default implementation of this method.</exception>
        protected virtual Task ExecuteOrderSwapAsync(TEntity entity, TEntity previous)
        {
            if (this.Overrides.ExecuteOrderSwap != null)
            {
                return this.Overrides.ExecuteOrderSwap.Invoke(entity, previous);
            }

            throw new NotSupportedException("This class does not provide default implementation of this method");
        }

        /// <summary>
        /// Asynchronously gets the name of the table that is used for storing entities.
        /// </summary>
        /// <returns>A task that represents the operation and contains its result.</returns>
        protected virtual Task<String> GetTableNameAsync()
        {
            if (this.Overrides.GetTableName != null)
            {
                return this.Overrides.GetTableName.Invoke();
            }

            var dbContext = this.ControllerServices.ServiceProvider.GetRequiredService<DbContext>();
            var entityType = dbContext.Model.FindEntityType(typeof(TEntity));
            var tableSchema = entityType.GetSchema();
            var tableName = entityType.GetTableName();

            var fullName = String.IsNullOrEmpty(tableSchema) ? $"[{tableName}]" : $"[{tableSchema}].[{tableName}]";
            return Task.FromResult(fullName);
        }

        /// <summary>
        /// Asynchronously gets the column name that stores manual order.
        /// </summary>
        /// <returns>A task that represents the operation and contains its result.</returns>
        protected virtual Task<String> GetOrderNoColumnNameAsync()
        {
            if (this.Overrides.GetOrderNoColumnName != null)
            {
                return this.Overrides.GetOrderNoColumnName.Invoke();
            }

            var dbContext = this.ControllerServices.ServiceProvider.GetRequiredService<DbContext>();
            var entityType = dbContext.Model.FindEntityType(typeof(TEntity));
            var tableSchema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            var column = entityType.FindProperty("OrderNo");
            var columnName = column.GetColumnName(StoreObjectIdentifier.Table(tableName, tableSchema));

            var fullName = $"[{columnName}]";
            return Task.FromResult(fullName);
        }

        /// <summary>
        /// Asynchronously gets the column name that stores the identifier of the entity.
        /// </summary>
        /// <returns>A task that represents the operation and contains its result.</returns>
        protected virtual Task<String> GetIdColumnNameAsync()
        {
            if (this.Overrides.GetIdColumnName != null)
            {
                return this.Overrides.GetIdColumnName.Invoke();
            }

            var dbContext = this.ControllerServices.ServiceProvider.GetRequiredService<DbContext>();
            var entityType = dbContext.Model.FindEntityType(typeof(TEntity));
            var tableSchema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            var column = entityType.FindProperty("Id");
            var columnName = column.GetColumnName(StoreObjectIdentifier.Table(tableName, tableSchema));

            var fullName = $"[{columnName}]";
            return Task.FromResult(fullName);
        }

        /// <summary>
        /// Asynchronously gets the entity identifier.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task that represents the operation and contains its result.</returns>
        protected virtual Task<TIdentifier> GetEntityIdentifierAsync(TEntity entity)
        {
            if (this.Overrides.GetEntityIdentifier != null)
            {
                return this.Overrides.GetEntityIdentifier.Invoke(entity);
            }

            return this.ModelIdentifierMapper.GetModelIdentifierAsync(entity);
        }

        /// <summary>
        /// Asynchronously gets the successful action result for the order decreasing.
        /// </summary>
        /// <param name="entity">Current entity.</param>
        /// <param name="previous">Previous entity.</param>
        /// <returns>A task that represents the operation and contains action result as a result.</returns>
        /// <remarks>By default this method redirects to Details action.</remarks>
        protected virtual Task<IActionResult> GetOrderChangeSuccessResultAsync(TEntity entity, TEntity previous)
        {
            if (this.Overrides.GetOrderChangeSuccessResult != null)
            {
                return this.Overrides.GetOrderChangeSuccessResult.Invoke(entity, previous);
            }

            return Task.FromResult<IActionResult>(this.Json(new Object { }));
        }
    }
}

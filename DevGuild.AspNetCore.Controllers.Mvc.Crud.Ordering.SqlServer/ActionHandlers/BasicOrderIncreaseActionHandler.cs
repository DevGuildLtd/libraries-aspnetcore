using System;
using System.Data;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Controllers.Mvc.Crud.Ordering.ActionHandlers;
using DevGuild.AspNetCore.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.Ordering.SqlServer.ActionHandlers
{
    /// <summary>
    /// SQL Server base implementation of basic order-decreasing operation action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="BasicOrderDecreaseActionHandlerBase{TIdentifier, TEntity}" />
    public class BasicOrderIncreaseActionHandler<TIdentifier, TEntity> : BasicOrderIncreaseActionHandlerBase<TIdentifier, TEntity>
        where TEntity : class, IOrderableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicOrderIncreaseActionHandler{TIdentifier, TEntity}"/> class.
        /// </summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        public BasicOrderIncreaseActionHandler(
            Controller controller,
            IEntityControllerServices controllerServices,
            IEntityPermissionsValidator<TEntity> permissionsValidator)
            : base(controller, controllerServices, permissionsValidator)
        {
        }

        /// <summary>
        /// Asynchronously executes the order number swap.
        /// </summary>
        /// <param name="entity">Current entity.</param>
        /// <param name="next">Next entity.</param>
        /// <returns>
        /// A task that represents the operation.
        /// </returns>
        protected override Task ExecuteOrderSwapAsync(TEntity entity, TEntity next)
        {
            if (this.Overrides.ExecuteOrderSwap != null)
            {
                return this.Overrides.ExecuteOrderSwap.Invoke(entity, next);
            }

            async Task DefaultImplementation()
            {
                var entityId = await this.GetEntityIdentifierAsync(entity);
                var nextId = await this.GetEntityIdentifierAsync(next);

                var dbContext = this.ControllerServices.ServiceProvider.GetService<DbContext>();

                var tableName = await this.GetTableNameAsync();
                var columnNameOrderNo = await this.GetOrderNoColumnNameAsync();
                var columnNameId = await this.GetIdColumnNameAsync();

                var queryText = $@"update {tableName}
  set {columnNameOrderNo} = case {columnNameId}
    when @previousId then @nextOrder
    when @nextId then @previousOrder
  end
  where {columnNameId} in (@previousId, @nextId)";

                await dbContext.Database.ExecuteSqlRawAsync(queryText, new Object[]
                {
                    new SqlParameter("previousId", SqlDbType.UniqueIdentifier) { Value = entityId },
                    new SqlParameter("nextId", SqlDbType.UniqueIdentifier) { Value = nextId },
                    new SqlParameter("previousOrder", SqlDbType.Int) { Value = entity.OrderNo },
                    new SqlParameter("nextOrder", SqlDbType.Int) { Value = next.OrderNo },
                });
            }

            return DefaultImplementation();
        }
    }
}

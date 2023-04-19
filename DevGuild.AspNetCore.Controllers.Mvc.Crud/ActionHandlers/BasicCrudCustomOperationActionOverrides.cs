using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic custom operation action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TOperationModel">The type of the operation model.</typeparam>
    public class BasicCrudCustomOperationActionOverrides<TIdentifier, TEntity, TOperationModel> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class
        where TOperationModel : class, new()
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.DemandPermissionsAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.DemandPermissionsAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, TEntity, Task> DemandPermissions { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.InitializeOperationModelAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.InitializeOperationModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, TEntity, TOperationModel, Boolean, Task> InitializeOperationModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.ValidateOperationModelAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.ValidateOperationModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, TEntity, TOperationModel, Task<Boolean>> ValidateOperationModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.ExecuteOperationAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.ExecuteOperationAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, TEntity, TOperationModel, Task> ExecuteOperation { get; set; }


        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.GetOperationViewResultAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.GetOperationViewResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, TEntity, TOperationModel, Task<IActionResult>> GetOperationViewResult { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.GetOperationSuccessResultAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudCustomOperationActionHandler{TIdentifier,TEntity,TOperationModel}.GetOperationSuccessResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, TEntity, TOperationModel, Task<IActionResult>> GetOperationSuccessResult { get; set; }
    }
}

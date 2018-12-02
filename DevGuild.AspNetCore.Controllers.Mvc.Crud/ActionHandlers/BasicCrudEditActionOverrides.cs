using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic Edit action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDetailsModel">The type of the details model.</typeparam>
    /// <typeparam name="TEditModel">The type of the edit model.</typeparam>
    public class BasicCrudEditActionOverrides<TIdentifier, TEntity, TDetailsModel, TEditModel> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.InitializeEditModelWithEntityAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.InitializeEditModelWithEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Task> InitializeEditModelWithEntity { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.InitializeEditModelAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.InitializeEditModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Boolean, Task> InitializeEditModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.ConvertToDetailsModelAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.ConvertToDetailsModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Task<TDetailsModel>> ConvertToDetailsModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.ValidateConcurrencyAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.ValidateConcurrencyAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Task<Boolean>> ValidateConcurrency { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.FailConcurrencyCheckAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.FailConcurrencyCheckAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Task> FailConcurrencyCheck { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.ValidateEditModelAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.ValidateEditModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Task<Boolean>> ValidateEditModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.UpdateExistingEntityAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.UpdateExistingEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Task> UpdateExistingEntity { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.UpdateExistingEntityConcurrencyAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.UpdateExistingEntityConcurrencyAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Task> UpdateExistingEntityConcurrency { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.UpdateEntityAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.UpdateEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Task> UpdateEntity { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.BeforeEntityUpdatedAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.BeforeEntityUpdatedAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Dictionary<String, Object>, Task> BeforeEntityUpdated { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.AfterEntityUpdatedAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.AfterEntityUpdatedAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Dictionary<String, Object>, Task> AfterEntityUpdated { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.GetEditSuccessResultAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.GetEditSuccessResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Dictionary<String, Object>, Task<ActionResult>> GetEditSuccessResult { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.GetEditViewResultAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicCrudEditActionHandler{TIdentifier,TEntity,TDetailsModel,TEditModel}.GetEditViewResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TEditModel, Task<ActionResult>> GetEditViewResult { get; set; }
    }
}

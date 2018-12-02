using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Controllers.Mvc.ActionHandlers;
using DevGuild.AspNetCore.Services.Permissions.Models;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents base CRUD action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class BaseCrudActionOverrides<TIdentifier, TEntity> : BaseActionOverrides
        where TEntity : class
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.QuerySingleEntityAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.QuerySingleEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, Task<TEntity>> QuerySingleEntity { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.BuildSingleQueryExpressionAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.BuildSingleQueryExpressionAsync"/> method of the related action handler.
        /// </value>
        public Func<TIdentifier, Task<Expression<Func<TEntity, Boolean>>>> BuildSingleQueryExpression { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.GetAllowedEntityPropertiesAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.GetAllowedEntityPropertiesAsync"/> method of the related action handler.
        /// </value>
        public Func<Permission, Task<String[]>> GetAllowedEntityProperties { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.GetAllEntityPropertiesAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.GetAllEntityPropertiesAsync"/> method of the related action handler.
        /// </value>
        public Func<Task<String[]>> GetAllEntityProperties { get; set; }
    }
}

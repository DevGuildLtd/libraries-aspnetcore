using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents base dependent CRUD action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TParentIdentifier">The type of the parent identifier.</typeparam>
    /// <typeparam name="TParentEntity">The type of the parent entity.</typeparam>
    /// <seealso cref="BaseCrudActionOverrides{TIdentifier,TEntity}" />
    public class BaseCrudDependentActionOverrides<TIdentifier, TEntity, TParentIdentifier, TParentEntity> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class
        where TParentEntity : class
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BaseCrudDependentActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TOverrides}.QuerySingleParentEntityAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BaseCrudDependentActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TOverrides}.QuerySingleParentEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentIdentifier, Task<TParentEntity>> QuerySingleParentEntity { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BaseCrudDependentActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TOverrides}.BuildSingleParentQueryExpressionAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BaseCrudDependentActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TOverrides}.BuildSingleParentQueryExpressionAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentIdentifier, Task<Expression<Func<TParentEntity, Boolean>>>> BuildSingleParentQueryExpression { get; set; }
    }
}

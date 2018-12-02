using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents basic Details action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDetailsModel">The type of the details model.</typeparam>
    /// <seealso cref="BaseCrudActionOverrides{TIdentifier,TEntity}" />
    public class BasicCrudDetailsActionOverrides<TIdentifier, TEntity, TDetailsModel> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.QuerySingleEntityAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BaseCrudActionHandler{TIdentifier,TEntity,TOverrides}.QuerySingleEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Task<TDetailsModel>> ConvertToDetailsModel { get; set; }
    }
}

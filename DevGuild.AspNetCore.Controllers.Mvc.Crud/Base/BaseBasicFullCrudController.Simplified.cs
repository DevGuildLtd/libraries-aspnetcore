using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.ObjectModel;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.Base
{
    /// <summary>
    /// Represents base controller that provides overridable basic CRUD actions implementations as a number of action handlers.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TCreateEditModel">The type of the create edit model.</typeparam>
    public abstract class BaseBasicFullCrudController<TIdentifier, TEntity, TCreateEditModel> : BaseBasicFullCrudController<TIdentifier, TEntity, DefaultEntityIndexModel<TEntity>, TEntity, TEntity, TCreateEditModel, TCreateEditModel, TEntity>
        where TEntity : class, new()
        where TCreateEditModel : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBasicFullCrudController{TIdentifier, TEntity, TCreateEditModel}"/> class.
        /// </summary>
        /// <param name="controllerServices">The controller services.</param>
        protected BaseBasicFullCrudController(IEntityControllerServices controllerServices)
            : base(controllerServices)
        {
        }
    }
}

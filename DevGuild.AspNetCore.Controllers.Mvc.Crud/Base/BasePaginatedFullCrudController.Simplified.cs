using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.ObjectModel;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.Base
{
    public abstract class BasePaginatedFullCrudController<TIdentifier, TEntity, TCreateEditModel> : BasePaginatedFullCrudController<TIdentifier, TEntity, DefaultEntityPaginatedIndexModel<TEntity>, TEntity, TEntity, TCreateEditModel, TCreateEditModel, TEntity>
        where TEntity : class, new()
        where TCreateEditModel : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePaginatedFullCrudController{TIdentifier, TEntity, TCreateEditModel}"/> class.
        /// </summary>
        /// <param name="controllerServices">The controller services.</param>
        protected BasePaginatedFullCrudController(IEntityControllerServices controllerServices)
            : base(controllerServices)
        {
        }
    }
}

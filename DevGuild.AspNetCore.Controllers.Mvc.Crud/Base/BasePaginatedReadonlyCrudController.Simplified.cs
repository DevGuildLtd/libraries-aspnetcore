using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.Base
{
    public abstract class BasePaginatedReadonlyCrudController<TIdentifier, TEntity> : BasePaginatedReadonlyCrudController<TIdentifier, TEntity, DefaultEntityPaginatedIndexModel<TEntity>, TEntity, TEntity>
        where TEntity : class, new()
    {
        protected BasePaginatedReadonlyCrudController(IEntityControllerServices controllerServices)
            : base(controllerServices)
        {
        }
    }
}

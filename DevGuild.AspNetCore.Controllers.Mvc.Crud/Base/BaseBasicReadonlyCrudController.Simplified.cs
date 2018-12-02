using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.ObjectModel;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.Base
{
    public abstract class BaseBasicReadonlyCrudController<TIdentifier, TEntity> : BaseBasicReadonlyCrudController<TIdentifier, TEntity, DefaultEntityIndexModel<TEntity>, TEntity, TEntity>
        where TEntity : class, new()
    {
        protected BaseBasicReadonlyCrudController(IEntityControllerServices controllerServices)
            : base(controllerServices)
        {
        }
    }
}

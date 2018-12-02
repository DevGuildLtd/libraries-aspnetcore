using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    public class BasicCrudPaginatedIndexActionOverrides<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class
    {
        public Func<Task<IQueryable<TEntity>>> PrepareItemsQuery { get; set; }

        public Func<ICollection<TEntity>, String[], Task<ICollection<TIndexItemModel>>> ConvertEntitiesToIndexItemModel { get; set; }

        public Func<TIndexViewModel, Task<IActionResult>> GetIndexViewResult { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud
{
    public sealed class PaginationCalculationResult<T>
    {
        private PaginationCalculationResult(IActionResult actionResult, IPaginationInfo info, List<T> entites)
        {
            this.ActionResult = actionResult;
            this.Info = info;
            this.Entites = entites;
        }

        public IActionResult ActionResult { get; }

        public IPaginationInfo Info { get; }

        public List<T> Entites { get; }

        public static PaginationCalculationResult<T> Succeed(IPaginationInfo info, List<T> entities)
        {
            return new PaginationCalculationResult<T>(null, info, entities);
        }

        public static PaginationCalculationResult<T> Fail(IActionResult actionResult)
        {
            return new PaginationCalculationResult<T>(actionResult, null, null);
        }
    }
}

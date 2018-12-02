using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Controllers.Mvc.Filters
{
    /// <summary>
    /// Restricts an action method so that the method handles only HTTP PUT requests that are made via AJAX.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AjaxPutAttribute : AjaxMethodSelectorAttribute
    {
        /// <inheritdoc />
        protected override Boolean IsValidMethod(String method)
        {
            return String.Equals(method, "PUT", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Controllers.Mvc.Filters
{
    /// <summary>
    /// Restricts an action method so that the method handles only HTTP GET requests that are made via AJAX.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AjaxGetAttribute : AjaxMethodSelectorAttribute
    {
        /// <inheritdoc />
        protected override Boolean IsValidMethod(String method)
        {
            return String.Equals(method, "GET", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

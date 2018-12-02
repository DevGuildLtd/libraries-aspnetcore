using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace DevGuild.AspNetCore.Controllers.Mvc.Filters
{
    /// <summary>
    /// Restricts an action method so that the method handles only HTTP DELETE requests that are made via AJAX.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AjaxDeleteAttribute : AjaxMethodSelectorAttribute
    {
        /// <inheritdoc />
        protected override Boolean IsValidMethod(String method)
        {
            return String.Equals(method, "DELETE", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

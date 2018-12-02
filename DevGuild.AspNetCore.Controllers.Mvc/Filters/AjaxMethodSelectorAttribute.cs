using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace DevGuild.AspNetCore.Controllers.Mvc.Filters
{
    public abstract class AjaxMethodSelectorAttribute : ActionMethodSelectorAttribute
    {
        public override Boolean IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return AjaxMethodSelectorAttribute.IsAjaxRequest(routeContext.HttpContext.Request) &&
                   this.IsValidMethod(routeContext.HttpContext.Request.Method);
        }

        /// <summary>
        /// Determines whether the specified method is valid.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>
        ///   <c>true</c> if the specified method is valid; otherwise, <c>false</c>.
        /// </returns>
        protected abstract Boolean IsValidMethod(String method);

        private static Boolean IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} is null", nameof(request));
            }

            if (request.Headers != null && request.Headers.TryGetValue("X-Requested-With", out var requestedWith))
            {
                return requestedWith == "XMLHttpRequest";
            }

            return false;
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DevGuild.AspNetCore.Testing.Mvc
{
    public class MockUrlHelper : IUrlHelper
    {
        public String Action(UrlActionContext actionContext)
        {
            return "";
        }

        public String Content(String contentPath)
        {
            return contentPath;
        }

        public Boolean IsLocalUrl(String url)
        {
            return true;
        }

        public String RouteUrl(UrlRouteContext routeContext)
        {
            return "";
        }

        public String Link(String routeName, Object values)
        {
            return "";
        }

        public ActionContext ActionContext { get; } = null;
    }
}

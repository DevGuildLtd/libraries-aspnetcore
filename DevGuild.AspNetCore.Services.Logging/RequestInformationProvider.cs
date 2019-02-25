using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace DevGuild.AspNetCore.Services.Logging
{
    public class RequestInformationProvider : IRequestInformationProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AsyncLocal<RequestInformation> overridenInformation;

        public RequestInformationProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.overridenInformation = new AsyncLocal<RequestInformation>();
        }

        public RequestInformation GetRequestInformation()
        {
            var overriden = this.overridenInformation.Value;
            if (overriden != null)
            {
                return overriden;
            }

            var httpContext = this.httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                return new RequestInformation(
                    httpContext.Request.Host.ToString(),
                    httpContext.Request.Method,
                    this.GetFullPath(httpContext.Request),
                    httpContext.Connection.RemoteIpAddress.ToString(),
                    httpContext.Request.Headers["User-Agent"],
                    httpContext.User.Identity.Name,
                    httpContext.TraceIdentifier);
            }

            return this.GetDefaultRequestInformation();
        }

        public void OverrideRequestInformation(RequestInformation information)
        {
            this.overridenInformation.Value = information;
        }

        private RequestInformation GetDefaultRequestInformation()
        {
            return new RequestInformation(
                "Default",
                "EXEC",
                "Unknown",
                "::1",
                "N/A",
                null,
                $"Default-{Guid.NewGuid():D}");
        }

        private String GetFullPath(HttpRequest request)
        {
            var sb = new StringBuilder();
            if (request.Path.HasValue)
            {
                sb.Append(request.Path.Value);
            }

            if (request.QueryString.HasValue)
            {
                sb.Append(request.QueryString.Value);
            }

            return sb.ToString();
        }
    }
}

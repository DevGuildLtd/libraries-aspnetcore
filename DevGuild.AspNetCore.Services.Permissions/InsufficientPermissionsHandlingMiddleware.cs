using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DevGuild.AspNetCore.Services.Permissions
{
    public class InsufficientPermissionsHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly InsufficientPermissionsHandlingOptions options;

        public InsufficientPermissionsHandlingMiddleware(RequestDelegate next, InsufficientPermissionsHandlingOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (InsufficientPermissionsException)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                if (context.User.Identity.IsAuthenticated)
                {
                    if (this.options.ReturnNotFoundForAuthenticatedUsers)
                    {
                        context.Response.StatusCode = 404;
                    }
                    else
                    {
                        context.Response.StatusCode = 403;
                    }
                }
                else
                {
                    context.Response.Redirect($"{this.options.LoginUrl}?ReturnUrl={UrlEncoder.Default.Encode(context.Request.Path)}");
                }
            }
        }
    }
}

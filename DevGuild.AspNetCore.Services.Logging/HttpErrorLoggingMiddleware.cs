using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DevGuild.AspNetCore.Services.Logging
{
    public class HttpErrorLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly HttpErrorLoggingMiddlewareOptions options;

        public HttpErrorLoggingMiddleware(RequestDelegate next, HttpErrorLoggingMiddlewareOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public async Task InvokeAsync(HttpContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(this.options.LogCategoryName);
                logger.LogWarning(exception, "Unhandled exception when processing a request");
                throw;
            }
        }
    }
}

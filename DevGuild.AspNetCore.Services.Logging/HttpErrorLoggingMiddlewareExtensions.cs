using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace DevGuild.AspNetCore.Services.Logging
{
    public static class HttpErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseHttpErrorLogging(typeof(HttpErrorLoggingMiddleware).FullName);
        }

        public static IApplicationBuilder UseHttpErrorLogging(this IApplicationBuilder builder, String logCategoryName)
        {
            return builder.UseMiddleware<HttpErrorLoggingMiddleware>(new HttpErrorLoggingMiddlewareOptions
            {
                LogCategoryName = logCategoryName
            });
        }
    }
}

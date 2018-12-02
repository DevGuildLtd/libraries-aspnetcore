using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace DevGuild.AspNetCore.Services.Permissions
{
    public static class PermissionsMiddlewaresExtensions
    {
        public static IApplicationBuilder UsePermissions(this IApplicationBuilder builder)
        {
            return builder.UsePermissions(new PermissionsMiddlewaresOptions());
        }

        public static IApplicationBuilder UsePermissions(this IApplicationBuilder builder, PermissionsMiddlewaresOptions options)
        {
            return builder.UseMiddleware<InsufficientPermissionsHandlingMiddleware>(options.InsufficientPermissionsHandling);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Permissions
{
    public static class PermissionsServiceCollectionExtensions
    {
        public static void AddPermissions(this IServiceCollection services, PermissionsHubConfiguration configuration)
        {
            services.AddSingleton<PermissionsHubConfiguration>(configuration);
            services.AddScoped<IPermissionsHub, PermissionsHub>();
        }
    }
}

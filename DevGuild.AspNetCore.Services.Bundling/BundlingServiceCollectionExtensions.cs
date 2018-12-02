using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Bundling
{
    public static class BundlingServiceCollectionExtensions
    {
        public static void AddBundling(this IServiceCollection services)
        {
            services.Configure<BundlingOptions>(options => { options.Enabled = true; });
            services.AddSingleton<IBundlingConfigurationService, BundlingConfigurationService>();
            services.AddSingleton<IBundlingService, BundlingService>();
        }

        public static void AddBundling(this IServiceCollection services, Boolean enabled)
        {
            services.Configure<BundlingOptions>(options => { options.Enabled = enabled; });
            services.AddSingleton<IBundlingConfigurationService, BundlingConfigurationService>();
            services.AddSingleton<IBundlingService, BundlingService>();
        }

        public static void AddBundling(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BundlingOptions>(configuration);
            services.AddSingleton<IBundlingConfigurationService, BundlingConfigurationService>();
            services.AddSingleton<IBundlingService, BundlingService>();
        }
    }
}

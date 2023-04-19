using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Extensions.Security
{
    public static class HstsConfigurationExtensions
    {
        private const String DefaultHstsSectionName = "Security:Hsts";

        public static IServiceCollection ConfigureHsts(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.TryGetHstsOptions(out var section))
            {
                services.Configure<HstsOptions>(section);
            }

            return services;
        }

        public static IApplicationBuilder TryUseHsts(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (configuration.TryGetHstsOptions(out var section))
            {
                app.UseHsts();
            }

            return app;
        }

        private static Boolean TryGetHstsOptions(this IConfiguration configuration, out IConfigurationSection section)
        {
            if (configuration is IConfigurationSection configurationAsSection)
            {
                var isEnabled = configurationAsSection.GetValue<Boolean>("Enabled");
                if (isEnabled)
                {
                    section = configurationAsSection;
                    return true;
                }
            }
            else
            {
                var childSection = configuration.GetSection(HstsConfigurationExtensions.DefaultHstsSectionName);
                if (childSection.Exists())
                {
                    var isEnabled = childSection.GetValue<Boolean>("Enabled");
                    if (isEnabled)
                    {
                        section = childSection;
                        return true;
                    }
                }
            }

            section = null;
            return false;
        }
    }
}

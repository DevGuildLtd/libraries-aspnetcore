using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Extensions.Security
{
    public static class HttpsRedirectionConfigurationExtensions
    {
        private const String DefaultHttpsRedirectionSectionName = "Security:HttpsRedirection";

        public static IServiceCollection ConfigureHttpsRedirection(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.TryGetHttpsRedirectionOptions(out var section))
            {
                services.Configure<HttpsRedirectionOptions>(section);
            }

            return services;
        }

        public static IApplicationBuilder TryUseHttpsRedirection(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (configuration.TryGetHttpsRedirectionOptions(out var section))
            {
                app.UseHttpsRedirection();
            }

            return app;
        }

        private static Boolean TryGetHttpsRedirectionOptions(this IConfiguration configuration, out IConfigurationSection section)
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
                var childSection = configuration.GetSection(HttpsRedirectionConfigurationExtensions.DefaultHttpsRedirectionSectionName);
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

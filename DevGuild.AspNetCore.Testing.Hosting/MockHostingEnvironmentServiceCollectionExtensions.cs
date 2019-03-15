using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Testing.Hosting
{
    public static class MockHostingEnvironmentServiceCollectionExtensions
    {
        public static void MockHostingEnvironment(this IServiceCollection services, String applicationName, String environmentName, String contentRootPath, String webRootPath)
        {
            var hostedEnvironment = new MockHostingEnvironment(applicationName, environmentName, contentRootPath, webRootPath);
            services.AddSingleton<IHostingEnvironment>(hostedEnvironment);
        }

        public static void MockHostingEnvironment(this IServiceCollection services, String applicationName, String environmentName, String contentRootPath)
        {
            var hostedEnvironment = new MockHostingEnvironment(applicationName, environmentName, contentRootPath);
            services.AddSingleton<IHostingEnvironment>(hostedEnvironment);
        }
    }
}

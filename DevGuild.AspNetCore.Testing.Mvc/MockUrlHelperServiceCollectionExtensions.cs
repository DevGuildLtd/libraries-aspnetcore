using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Testing.Mvc
{
    public static class MockUrlHelperServiceCollectionExtensions
    {
        public static void MockUrlHelper(this IServiceCollection services)
        {
            services.AddScoped<IUrlHelper, MockUrlHelper>();
        }
    }
}

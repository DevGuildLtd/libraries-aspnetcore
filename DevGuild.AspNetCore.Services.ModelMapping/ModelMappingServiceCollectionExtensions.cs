using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    public static class ModelMappingServiceCollectionExtensions
    {
        public static void AddModelMapping(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelMappingManager, ViewModelMappingManager>();
        }
    }
}

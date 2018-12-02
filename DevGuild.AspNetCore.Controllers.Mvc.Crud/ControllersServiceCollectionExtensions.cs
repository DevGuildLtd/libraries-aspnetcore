using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud
{
    public static class ControllersServiceCollectionExtensions
    {
        public static void AddControllerServices(this IServiceCollection services)
        {
            services.AddScoped<IEntityControllerServices, EntityControllerServices>();
        }
    }
}

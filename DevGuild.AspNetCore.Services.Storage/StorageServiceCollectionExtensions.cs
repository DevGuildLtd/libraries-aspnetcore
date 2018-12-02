using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Storage
{
    public static class StorageServiceCollectionExtensions
    {
        public static StorageServiceBuilder AddStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var storageHubConfiguration = new StorageHubConfiguration();
            services.AddSingleton<StorageHubConfiguration>(storageHubConfiguration);
            services.AddScoped<IStorageHub, StorageHub>();

            return new StorageServiceBuilder(services, configuration, storageHubConfiguration);
        }
    }
}

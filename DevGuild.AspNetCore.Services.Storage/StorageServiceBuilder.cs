using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Storage
{
    public class StorageServiceBuilder
    {
        private readonly IDictionary<String, Func<IConfiguration, StorageContainerConstructor>> providers = new Dictionary<String, Func<IConfiguration, StorageContainerConstructor>>();
        private readonly StorageHubConfiguration storageHubConfiguration;

        public StorageServiceBuilder(IServiceCollection services, IConfiguration configuration, StorageHubConfiguration storageHubConfiguration)
        {
            this.Services = services;
            this.Configuration = configuration;
            this.storageHubConfiguration = storageHubConfiguration;
        }

        public IServiceCollection Services { get; }

        public IConfiguration Configuration { get; }

        public StorageServiceBuilder AddContainerProvider(String type, Func<IConfiguration, StorageContainerConstructor> provider)
        {
            this.providers[type] = provider;
            return this;
        }

        public StorageServiceBuilder RegisterContainer(String name)
        {
            IConfiguration configurationSection = this.Configuration.GetSection(name);
            var type = configurationSection.GetValue<String>("Type");
            if (String.IsNullOrEmpty(type))
            {
                throw new InvalidOperationException($"Type for StorageContainer {name} is not configured");
            }

            if (!this.providers.TryGetValue(type, out var provider))
            {
                throw new InvalidOperationException($"Provider for type {type} is not registered");
            }

            var constructor = provider(configurationSection);
            this.storageHubConfiguration.RegisterConstructor(name, constructor);

            return this;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Sms.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Sms
{
    public class SmsServiceBuilder
    {
        private readonly SmsConfigurationCollection configurationCollection;

        private readonly Dictionary<String, Func<String, IConfiguration, SmsConfiguration>> providers = new Dictionary<String, Func<String, IConfiguration, SmsConfiguration>>();

        public SmsServiceBuilder(IServiceCollection services, IConfiguration configuration, SmsConfigurationCollection configurationCollection)
        {
            this.Services = services;
            this.Configuration = configuration;
            this.configurationCollection = configurationCollection;
        }

        public IServiceCollection Services { get; }

        public IConfiguration Configuration { get; }

        public SmsServiceBuilder AddProvider(String name, Func<String, IConfiguration, SmsConfiguration> provider)
        {
            this.providers[name] = provider;
            return this;
        }

        public SmsServiceBuilder RegisterConfiguration(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"{nameof(name)} is null", nameof(name));
            }

            IConfiguration configurationSection = this.Configuration.GetSection(name);
            var type = configurationSection.GetValue<String>("Type");
            if (String.IsNullOrEmpty(type))
            {
                throw new InvalidOperationException($"Type for SmsConfiguration {name} is not configured");
            }

            if (!this.providers.TryGetValue(type, out var provider))
            {
                throw new InvalidOperationException($"Provider for type {type} is not registered");
            }

            var smsConfiguration = provider(name, configurationSection);
            this.configurationCollection.RegisterConfiguration(smsConfiguration);

            return this;
        }
    }
}

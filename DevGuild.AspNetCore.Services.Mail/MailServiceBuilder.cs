using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Mail.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Mail
{
    public class MailServiceBuilder
    {
        private readonly MailConfigurationCollection configurationCollection;
        private readonly Dictionary<String, Func<String, IConfiguration, MailConfiguration>> providers = new Dictionary<String, Func<String, IConfiguration, MailConfiguration>>();

        public MailServiceBuilder(IServiceCollection services, IConfiguration configuration, MailConfigurationCollection configurationCollection)
        {
            this.Services = services;
            this.Configuration = configuration;
            this.configurationCollection = configurationCollection;
        }

        public IServiceCollection Services { get; }

        public IConfiguration Configuration { get; }

        public MailServiceBuilder AddProvider(String name, Func<String, IConfiguration, MailConfiguration> provider)
        {
            this.providers[name] = provider;
            return this;
        }

        public MailServiceBuilder RegisterConfiguration(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"{nameof(name)} is null", nameof(name));
            }

            IConfiguration configurationSection = this.Configuration.GetSection(name);
            var type = configurationSection.GetValue<String>("Type");
            if (String.IsNullOrEmpty(type))
            {
                throw new InvalidOperationException($"Type of EmailConfiguration {name} is not configured");
            }

            if (!this.providers.TryGetValue(type, out var provider))
            {
                throw new InvalidOperationException($"Provider for type {type} is not registered");
            }

            var mailConfiguration = provider(name, configurationSection);
            this.configurationCollection.RegisterConfiguration(mailConfiguration);

            return this;
        }
    }
}

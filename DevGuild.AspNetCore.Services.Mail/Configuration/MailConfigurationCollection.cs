using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail.Configuration
{
    /// <summary>
    /// Represents a collection of mail configurations.
    /// </summary>
    public class MailConfigurationCollection
    {
        private readonly Dictionary<String, MailConfiguration> configurations = new Dictionary<String, MailConfiguration>();

        /// <summary>
        /// Gets the default configuration.
        /// </summary>
        /// <value>
        /// The default configuration.
        /// </value>
        public String DefaultConfiguration { get; internal set; }

        /// <summary>
        /// Gets the configuration by name.
        /// </summary>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <returns>A mail configuration.</returns>
        public MailConfiguration GetConfiguration(String configurationName)
        {
            return this.configurations.TryGetValue(configurationName, out var result) ? result : null;
        }

        internal void RegisterConfiguration(MailConfiguration configuration)
        {
            this.configurations[configuration.ConfigurationName] = configuration;
            if (this.DefaultConfiguration == null)
            {
                this.DefaultConfiguration = configuration.ConfigurationName;
            }
        }
    }
}

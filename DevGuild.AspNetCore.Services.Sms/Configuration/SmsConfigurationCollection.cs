using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.Configuration
{
    /// <summary>
    /// Represents sms configuration collection.
    /// </summary>
    public class SmsConfigurationCollection
    {
        private readonly Dictionary<String, SmsConfiguration> configurations = new Dictionary<String, SmsConfiguration>();

        /// <summary>
        /// Gets the default configuration.
        /// </summary>
        /// <value>
        /// The default configuration.
        /// </value>
        public String DefaultConfiguration { get; internal set; }

        /// <summary>
        /// Gets the configuration by its name.
        /// </summary>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <returns>A sms configuration.</returns>
        public SmsConfiguration GetConfiguration(String configurationName)
        {
            return this.configurations.TryGetValue(configurationName, out var result) ? result : null;
        }

        internal void RegisterConfiguration(SmsConfiguration configuration)
        {
            this.configurations[configuration.ConfigurationName] = configuration;
            if (this.DefaultConfiguration == null)
            {
                this.DefaultConfiguration = configuration.ConfigurationName;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.Configuration
{
    /// <summary>
    /// Represents sms configuration.
    /// </summary>
    public class SmsConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsConfiguration"/> class.
        /// </summary>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <param name="senderName">Name of the sender.</param>
        /// <param name="providerConstructor">The provider constructor.</param>
        public SmsConfiguration(String configurationName, String senderName, SmsProviderConstructor providerConstructor)
        {
            this.ConfigurationName = configurationName;
            this.SenderName = senderName;
            this.ProviderConstructor = providerConstructor;
        }

        /// <summary>
        /// Gets the name of the configuration.
        /// </summary>
        /// <value>
        /// The name of the configuration.
        /// </value>
        public String ConfigurationName { get; }

        /// <summary>
        /// Gets the name of the sender.
        /// </summary>
        /// <value>
        /// The name of the sender.
        /// </value>
        public String SenderName { get; }

        /// <summary>
        /// Gets the provider constructor.
        /// </summary>
        /// <value>
        /// The provider constructor.
        /// </value>
        public SmsProviderConstructor ProviderConstructor { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail.Configuration
{
    /// <summary>
    /// Represents mail configuration.
    /// </summary>
    public class MailConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailConfiguration"/> class.
        /// </summary>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <param name="senderConfiguration">The sender configuration.</param>
        /// <param name="providerConstructor">The provider constructor.</param>
        public MailConfiguration(String configurationName, MailSenderConfiguration senderConfiguration, EmailProviderConstructor providerConstructor)
        {
            this.ConfigurationName = configurationName;
            this.SenderConfiguration = senderConfiguration;
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
        /// Gets the sender configuration.
        /// </summary>
        /// <value>
        /// The sender configuration.
        /// </value>
        public MailSenderConfiguration SenderConfiguration { get; }

        /// <summary>
        /// Gets the provider constructor.
        /// </summary>
        /// <value>
        /// The provider constructor.
        /// </value>
        public EmailProviderConstructor ProviderConstructor { get; }
    }
}

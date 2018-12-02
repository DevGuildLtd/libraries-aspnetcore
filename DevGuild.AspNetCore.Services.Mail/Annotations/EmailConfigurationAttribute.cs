using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DevGuild.AspNetCore.Services.Mail.Models;

namespace DevGuild.AspNetCore.Services.Mail.Annotations
{
    /// <summary>
    /// Specifies the name of the email configuration.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class EmailConfigurationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfigurationAttribute"/> class.
        /// </summary>
        /// <param name="configurationName">Name of the configuration.</param>
        public EmailConfigurationAttribute(String configurationName)
        {
            this.ConfigurationName = configurationName;
        }

        /// <summary>
        /// Gets the name of the configuration.
        /// </summary>
        /// <value>
        /// The name of the configuration.
        /// </value>
        public String ConfigurationName { get; }

        /// <summary>
        /// Gets the configuration from email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The configuration.</returns>
        public static String GetConfigurationFromEmail(IEmail email)
        {
            if (email == null)
            {
                throw new ArgumentNullException($"{nameof(email)} is null", nameof(email));
            }

            var type = email.GetType();
            var attribute = type.GetCustomAttribute<EmailConfigurationAttribute>();
            return attribute?.ConfigurationName;
        }
    }
}

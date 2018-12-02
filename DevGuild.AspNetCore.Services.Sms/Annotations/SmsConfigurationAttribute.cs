using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DevGuild.AspNetCore.Services.Sms.Models;

namespace DevGuild.AspNetCore.Services.Sms.Annotations
{
    /// <summary>
    /// Defines configuration for the sms message class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SmsConfigurationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsConfigurationAttribute"/> class.
        /// </summary>
        /// <param name="configurationName">Name of the configuration.</param>
        public SmsConfigurationAttribute(String configurationName)
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
        /// Gets the configuration from sms message.
        /// </summary>
        /// <param name="message">The sms message.</param>
        /// <returns>Name of the configuration.</returns>
        public static String GetConfigurationFromSms(ISms message)
        {
            if (message == null)
            {
                throw new ArgumentNullException($"{nameof(message)} is null", nameof(message));
            }

            var type = message.GetType();
            var attribute = type.GetCustomAttribute<SmsConfigurationAttribute>();
            return attribute?.ConfigurationName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail.Models
{
    /// <summary>
    /// Provides ability to include configuration name in the class that represents email message.
    /// </summary>
    public interface IExplicitEmailConfiguration
    {
        /// <summary>
        /// Gets the name of the configuration.
        /// </summary>
        /// <value>
        /// The name of the configuration.
        /// </value>
        String GetConfigurationName();
    }
}

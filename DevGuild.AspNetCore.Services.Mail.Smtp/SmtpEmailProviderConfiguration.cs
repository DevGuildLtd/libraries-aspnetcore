using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail.Smtp
{
    /// <summary>
    /// Represents SMTP email provider configuration.
    /// </summary>
    public class SmtpEmailProviderConfiguration
    {
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public String Host { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public Int32 Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SSL is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if SSL is enabled; otherwise, <c>false</c>.
        /// </value>
        public Boolean EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public String Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public SecureString Password { get; set; }
    }
}

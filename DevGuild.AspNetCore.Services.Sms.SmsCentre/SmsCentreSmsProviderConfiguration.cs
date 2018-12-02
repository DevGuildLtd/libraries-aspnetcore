using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.SmsCentre
{
    /// <summary>
    /// Represents sms centre provider configuration.
    /// </summary>
    public class SmsCentreSmsProviderConfiguration
    {
        /// <summary>
        /// Gets or sets the service URL.
        /// </summary>
        /// <value>
        /// The service URL.
        /// </value>
        public String ServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the secondary URL.
        /// </summary>
        /// <value>
        /// The secondary URL.
        /// </value>
        public String SecondaryUrl { get; set; }

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
        public String Password { get; set; }
    }
}

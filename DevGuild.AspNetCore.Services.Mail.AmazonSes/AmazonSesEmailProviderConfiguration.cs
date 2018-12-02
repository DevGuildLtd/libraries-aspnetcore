using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail.AmazonSes
{
    /// <summary>
    /// Represents AmazonSes email provider configuration.
    /// </summary>
    public class AmazonSesEmailProviderConfiguration
    {
        /// <summary>
        /// Gets or sets the access key.
        /// </summary>
        /// <value>
        /// The access key.
        /// </value>
        public String AccessKey { get; set; }

        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        /// <value>
        /// The secret key.
        /// </value>
        public String SecretKey { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public String Region { get; set; }
    }
}

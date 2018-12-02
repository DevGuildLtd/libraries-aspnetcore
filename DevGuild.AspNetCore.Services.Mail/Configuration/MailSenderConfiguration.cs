using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail.Configuration
{
    /// <summary>
    /// Represents implementation of mail sender configuration.
    /// </summary>
    public sealed class MailSenderConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailSenderConfiguration"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="blindCopy">The blind copy.</param>
        /// <param name="debugMode"><c>true</c> if sender is in the debug mode; otherwise, <c>false</c>.</param>
        public MailSenderConfiguration(String sender, String blindCopy, Boolean debugMode)
        {
            this.Sender = sender;
            this.BlindCopy = blindCopy;
            this.DebugMode = debugMode;
        }

        /// <summary>
        /// Gets the message sender.
        /// </summary>
        /// <value>
        /// The message sender.
        /// </value>
        public String Sender { get; }

        /// <summary>
        /// Gets the additional blind copy address.
        /// </summary>
        /// <value>
        /// The additional blind copy address.
        /// </value>
        public String BlindCopy { get; }

        /// <summary>
        /// Gets a value indicating whether sender is in the debug mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sender is in the debug mode; otherwise, <c>false</c>.
        /// </value>
        public Boolean DebugMode { get; }
    }
}

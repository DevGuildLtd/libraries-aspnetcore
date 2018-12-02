using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.SmsCentre.Client
{
    /// <summary>
    /// Represents sms centre send request.
    /// </summary>
    public class SmsCentreSendRequest
    {
        /// <summary>
        /// Gets or sets the recipients phones.
        /// </summary>
        /// <value>
        /// The recipients phones.
        /// </value>
        public String[] Phones { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public String Message { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public String Identifier { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public String Sender { get; set; }
    }
}

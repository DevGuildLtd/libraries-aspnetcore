using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail.Models
{
    /// <summary>
    /// Represents a status of the message sending.
    /// </summary>
    public enum EmailMessageStatus
    {
        /// <summary>
        /// The message is created
        /// </summary>
        Created,

        /// <summary>
        /// The message is sent
        /// </summary>
        Sent,

        /// <summary>
        /// The message sending failed.
        /// </summary>
        Failed
    }
}

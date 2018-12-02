using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.Models
{
    /// <summary>
    /// Represents status of sms message delivery.
    /// </summary>
    public enum SmsMessageStatus
    {
        /// <summary>
        /// Message is created.
        /// </summary>
        Created,

        /// <summary>
        /// Message is sent.
        /// </summary>
        Sent,

        /// <summary>
        /// Message delivery is failed.
        /// </summary>
        Failed
    }
}

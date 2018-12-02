using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.Models
{
    /// <summary>
    /// Represents stored sms message.
    /// </summary>
    public class SmsMessage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Int32 Id { get; set; }

        /// <summary>
        /// Gets or sets the date of creation.
        /// </summary>
        /// <value>
        /// The date of creation.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message.
        /// </value>
        [Required]
        [MaxLength(200)]
        public String MessageType { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        [Required]
        [MaxLength(200)]
        public String Configuration { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public String From { get; set; }

        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        /// <value>
        /// The recipient.
        /// </value>
        public String To { get; set; }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        /// <value>
        /// The message text.
        /// </value>
        public String MessageText { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public String MessageId { get; set; }

        /// <summary>
        /// Gets or sets the status of the delivery.
        /// </summary>
        /// <value>
        /// The status of the delivery.
        /// </value>
        public SmsMessageStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the date of the last attempt.
        /// </summary>
        /// <value>
        /// The date of the last attempt.
        /// </value>
        public DateTime LastAttempt { get; set; }

        /// <summary>
        /// Gets or sets the total number of attempts.
        /// </summary>
        /// <value>
        /// The total number of attempts.
        /// </value>
        public Int32 TotalAttempts { get; set; }

        /// <summary>
        /// Gets or sets the last delivery error.
        /// </summary>
        /// <value>
        /// The last delivery error.
        /// </value>
        public String LastError { get; set; }
    }
}

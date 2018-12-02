using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail.Models
{
    /// <summary>
    /// Represents stored email message.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Gets or sets the identifier of the message.
        /// </summary>
        /// <value>
        /// The identifier of the message.
        /// </value>
        public Int32 Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the creation.
        /// </summary>
        /// <value>
        /// The date of the creation.
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
        /// Gets or sets from address.
        /// </summary>
        /// <value>
        /// From address.
        /// </value>
        public String From { get; set; }

        /// <summary>
        /// Gets or sets recipient address.
        /// </summary>
        /// <value>
        /// To recipient address.
        /// </value>
        public String To { get; set; }

        /// <summary>
        /// Gets or sets the copy address.
        /// </summary>
        /// <value>
        /// The copy address.
        /// </value>
        public String Cc { get; set; }

        /// <summary>
        /// Gets or sets the blind copy address.
        /// </summary>
        /// <value>
        /// The blind copy address.
        /// </value>
        public String Bcc { get; set; }

        /// <summary>
        /// Gets or sets the subject of the email.
        /// </summary>
        /// <value>
        /// The subject of the email.
        /// </value>
        public String Subject { get; set; }

        /// <summary>
        /// Gets or sets the content of the email.
        /// </summary>
        /// <value>
        /// The content of the email.
        /// </value>
        public String Content { get; set; }

        /// <summary>
        /// Gets or sets the status of the email.
        /// </summary>
        /// <value>
        /// The status of the email.
        /// </value>
        public EmailMessageStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public String MessageId { get; set; }

        /// <summary>
        /// Gets or sets the sending error.
        /// </summary>
        /// <value>
        /// The sending error.
        /// </value>
        public String SendingError { get; set; }
    }
}

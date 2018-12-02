using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail
{
    /// <summary>
    /// Represents result of sending email.
    /// </summary>
    public class EmailSendingResult
    {
        private EmailSendingResult(Boolean success, String messageId, String error)
        {
            this.Success = success;
            this.MessageId = messageId;
            this.Error = error;
        }

        /// <summary>
        /// Gets a value indicating whether the email sending was successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sending was successful; otherwise, <c>false</c>.
        /// </value>
        public Boolean Success { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        /// <remarks>Value is <c>null</c> is sending was successful.</remarks>
        public String Error { get; }

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        /// <remarks>Value is <c>null</c> is sending was not successful.</remarks>
        public String MessageId { get; }

        /// <summary>
        /// Returns email sending result that indicates a successful sending.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <returns>Succeeded email sending result.</returns>
        public static EmailSendingResult Succeed(String messageId)
        {
            return new EmailSendingResult(true, messageId, null);
        }

        /// <summary>
        /// Returns email sending result that indicates a failed sending.
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <returns>Failed email sending result.</returns>
        public static EmailSendingResult Fail(String error)
        {
            return new EmailSendingResult(false, null, error);
        }
    }
}

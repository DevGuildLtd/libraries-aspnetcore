using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace DevGuild.AspNetCore.Services.Mail
{
    /// <summary>
    /// Represents implementation of the email provider that does nothing.
    /// </summary>
    /// <seealso cref="DevGuild.AspNetCore.Services.Mail.IEmailProvider" />
    public class NoneEmailProvider : IEmailProvider
    {
        /// <inheritdoc />
        public Task<EmailSendingResult> SendAsync(MimeMessage message)
        {
            return Task.FromResult(EmailSendingResult.Succeed(String.Empty));
        }
    }
}

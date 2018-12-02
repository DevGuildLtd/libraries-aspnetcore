using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace DevGuild.AspNetCore.Services.Mail
{
    public interface IEmailProvider
    {
        Task<EmailSendingResult> SendAsync(MimeMessage message);
    }
}

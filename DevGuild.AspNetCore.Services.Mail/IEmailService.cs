using System;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Mail.Models;

namespace DevGuild.AspNetCore.Services.Mail
{
    public interface IEmailService
    {
        Task<EmailMessage> SendAsync(IEmail email);
    }
}
